using AutoMapper;
using DataLayer.QueryObjects;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ProfileServices.QueryObjects;

namespace ServiceLayer.ProfileServices.Concrete
{
    public class ProfileService
    {
        // Путь к корневой директории для сохранения файлов используется в демо версии
        private readonly string _rootPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        private readonly IMapper _mapper;
        private readonly UnitOfWork _uow;

        // Конструктор сервиса, принимает контекст базы данных
        public ProfileService(IMapper mapper, UnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        /// <summary>
        /// Метод для получения списка профилей с фильтрацией и постраничной навигацией.
        /// </summary>
        /// <param name="options">Параметры фильтрации и пагинации.</param>
        /// <returns>Возвращает DTO с фильтрованными профилями и дополнительной информацией.</returns>
        public async Task<ProfileListCombinedDto> FilterPageProfileListAsync(FilterPageOptions options)
        {
            // Начинаем формировать запрос для получения профилей из базы данных с фильтрацией.
            var profileQuery = _uow.Ctx.Profiles
                // Применяем фильтрацию по городу, полу, возрасту (максимальный и минимальный возраст).
                .FilterProfilesBy(options.CityId, options.GenderId, options.MaxAge, options.MinAge)
                // Преобразуем результаты профилей в DTO-объекты (ProfileListDto).
                .MapProfileToDto();

            // Настроим дополнительные параметры для DTO, такие как сортировка или дополнительные фильтры.
            options.SetupRestOfDto(profileQuery);

            // Получаем отсортированные и отфильтрованные профили с применением постраничной навигации.
            var profileList = await profileQuery
                // Применяем пагинацию: страница начинается с 0, поэтому уменьшаем на 1.
                .Page(options.PageNum - 1, options.PageSize)
                // Выполняем запрос в базу данных и возвращаем результаты.
                .ToListAsync();

            options.Size = profileList.Count;

            // Возвращаем DTO, которое содержит отфильтрованные профили и информацию о параметрах страницы.
            return new ProfileListCombinedDto
            {
                FilterPageData = options, // Дополнительные параметры фильтрации и пагинации.
                ProfileList = profileList // Список профилей, соответствующих запросу.
            };
        }


        /// <summary>
        /// Создает нового пользователя на основе данных из ProfileDto.
        /// </summary>
        /// <param name="newProfile">DTO объект с данными для нового пользователя.</param>
        /// <returns>Созданный пользователь.</returns>
        public async Task<ProfileDto> CreateProfileAsync(ProfileDto newProfile)
        {
            try
            {
                var user = await _uow.Ctx.Users
                    .Include(x => x.Address)
                    .SingleAsync(x => x.UserId == newProfile.UserId);

                // Инициализируем вычисляемые свойства InterestLinks перед сохранением пользователя
                newProfile.BeforeExecute();

                // Маппируем данные из ProfileDto в сущность Profile
                // Добавляем поля из пользователя дублирование даных для оптимизации поиска профилей по полям пользователя
                var profile = _mapper.Map(user, _mapper.Map<Profile>(newProfile));

                _uow.Ctx.Profiles.Add(profile); // Добавляем нового пользователя в контекст

                // Обрабатываем и добавляем файлы пользователя
                foreach (var fileDto in newProfile.FilesDto)
                {
                    // Сохраняем файл в файловой системе
                    var filePath = Path.Combine(_rootPath, fileDto.SafeFileName);

                    // Внимание: Слепки файлов не проверяются, просто перезаписываем файл. 
                    // Файловый контекст гарантирует атомарность операций.
                    _uow.FileContext.CreateFile(filePath, fileDto.Stream);
                }

                // Сохраняем изменения в базе данных в рамках одной транзакции
                await _uow.SaveChangesAsync();

                return newProfile;
            }
            catch
            {
                // В случае ошибки откатываем изменения
                _uow.Rollback();

                throw;
            }
        }

        /// <summary>
        /// Обновляет существующего пользователя на основе данных из ProfileDto.
        /// </summary>
        /// <param name="newProfile">DTO объект с новыми данными для пользователя.</param>
        /// <returns>Обновленный пользователь.</returns>
        public async Task<ProfileDto> UpdateProfileAsync(ProfileDto newProfile)
        {
            try
            {
                // Получаем пользователя из базы данных с соответствующими связями
                var profile = await _uow.Ctx.Profiles
                        .Include(x => x.InterestLinks) // Включаем связи с интересами
                            .ThenInclude(x => x.Interest)
                        .Include(x => x.LanguageLinks) // Включаем связи с языками
                            .ThenInclude(x => x.Language)
                        .Include(x => x.Files)
                    .SingleAsync(x => x.UserId == newProfile.UserId); // Находим пользователя по ProfileId

                // Обрабатываем изменения файлов пользователя
                // Игнорируется проверка слепка файлов, т.к. файловый контекст гарантирует атомарность операций.
                // Это позволяет перезаписать файлы без дополнительных проверок на идентичность.
                foreach (var file in profile.Files)
                {
                    // Пытаемся удалить файл, если он уже был создан ранее
                    // Это позволяет избежать проблем с оставшимися старыми версиями файлов.
                    _uow.FileContext.DeleteFile(Path.Combine(_rootPath, file.FileId));
                }

                // Инициализируем вычисляемые свойства InterestLinks перед сохранением обновлений
                newProfile.BeforeExecute();

                // Копируем данные из ProfileDto в сущность пользователя
                var updatedProfile = _mapper.Map(newProfile, profile);

                foreach (var fileDto in newProfile.FilesDto)
                {
                    // Сохраняем новый файл или пересоздаем (для простоты логики) существующий в файловой системе
                    _uow.FileContext.CreateFile(Path.Combine(_rootPath, fileDto.SafeFileName), fileDto.Stream);
                }

                // Сохраняем изменения в базе данных и файловой системе в рамках одной транзакции
                await _uow.SaveChangesAsync();

                // Возвращаем обновленного пользователя
                return newProfile;
            }
            catch
            {
                // В случае ошибки откатываем изменения
                _uow.Rollback();

                throw;
            }
        }
    }
}
