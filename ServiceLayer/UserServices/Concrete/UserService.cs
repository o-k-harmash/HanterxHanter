using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.UserServices.Concrete
{
    public class UserService
    {
        // Контекст для работы с базой данных
        private readonly EfCoreContext _ctx;

        // Маппер для преобразования DTO в сущности и обратно
        private readonly IMapper _mapper;

        // Конструктор класса, инициализирует сервисы
        public UserService(IMapper mapper, EfCoreContext ctx)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        /// <param name="newUser">DTO нового пользователя для создания</param>
        /// <returns>DTO нового пользователя после создания</returns>
        public Task<User> GetUserByIdAsync(Guid userId)
        {
            // Добавляем нового пользователя в контекст базы данных
            return _ctx.Users.Include(x => x.Address).SingleAsync(x => x.UserId == userId);
        }

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        /// <param name="newUser">DTO нового пользователя для создания</param>
        /// <returns>DTO нового пользователя после создания</returns>
        public async Task<UserDto> CreateUserAsync(UserDto newUser)
        {
            // Подготовка данных перед выполнением (например, валидация, дополнительные вычисления и т.д.)
            newUser.BeforeExecute();

            // Преобразуем DTO в сущность User с помощью AutoMapper
            var user = _mapper.Map<User>(newUser);
            Console.WriteLine(user.UserId);
            // Добавляем нового пользователя в контекст базы данных
            _ctx.Users.Add(user);

            // Сохраняем изменения в базе данных
            await _ctx.SaveChangesAsync();

            // Возвращаем DTO нового пользователя, который был добавлен
            return newUser;
        }

        /// <summary>
        /// Обновляет данные существующего пользователя.
        /// </summary>
        /// <param name="newUser">DTO обновленных данных пользователя</param>
        /// <returns>DTO обновленного пользователя</returns>
        public async Task<UserDto> UpdateUserAsync(UserDto newUser)
        {
            // Ищем существующего пользователя по UserId
            var user = await _ctx.Users
                .Include(u => u.Address)
                .SingleAsync(x => x.UserId == newUser.UserId);

            // Подготавливаем данные перед выполнением (например, валидация или вычисления)
            newUser.BeforeExecute();

            // Маппируем обновленные данные из DTO в сущность пользователя
            var updatedUser = _mapper.Map(newUser, user);

            // Сохраняем изменения в базе данных
            await _ctx.SaveChangesAsync();

            // Возвращаем обновленный DTO пользователя
            return newUser;
        }
    }
}
