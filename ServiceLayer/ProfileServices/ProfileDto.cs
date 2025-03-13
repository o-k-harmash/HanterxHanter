using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ServiceLayer.ProfileServices
{
    public class ProfileDto : IDisposable
    {
        // Приватные поля
        private List<File> _files;
        private List<FileDto> _filesDto;
        private List<ProfileInterest> _interestLinks;
        private List<ProfileLanguage> _languageLinks;

        // Флаг для отслеживания освобождения ресурсов
        private bool _disposed = false;

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        public string Bio { get; set; }

        /// <summary>
        /// Идентификатор цели отношений.
        /// </summary>
        public string RelationshipGoalId { get; set; }

        /// <summary>
        /// Идентификатор сексуальной ориентации.
        /// </summary>
        public string SexualOrientationId { get; set; }

        /// <summary>
        /// Список идентификаторов интересов.
        /// </summary>
        public List<string> InterestsId { get; set; }

        /// <summary>
        /// Список идентификаторов языков.
        /// </summary>
        public List<string> LanguagesId { get; set; }

        /// <summary>
        /// Список файлов (в формате IFormFile).
        /// </summary>
        [JsonIgnore]
        public List<IFormFile> FormFiles { get; set; }

        /// <summary>
        /// Список файлов (сущности типа File).
        /// </summary>
        [ValidateNever]
        public List<File> Files
        {
            get
            {
                return _files ??= new List<File>(); // Ленивая инициализация
            }
            private set
            {
                _files = value;
            }
        }

        /// <summary>
        /// Список файлов DTO (сущности типа FileDto).
        /// </summary>
        [JsonIgnore]
        [ValidateNever]
        public List<FileDto> FilesDto
        {
            get
            {
                return _filesDto ??= new List<FileDto>(); // Ленивая инициализация
            }
            private set
            {
                _filesDto = value;
            }
        }

        /// <summary>
        /// Список связей между пользователем и интересами (сущности типа ProfileInterest).
        /// </summary>
        [JsonIgnore]
        [ValidateNever]
        public List<ProfileInterest> InterestLinks
        {
            get
            {
                return _interestLinks ??= new List<ProfileInterest>(); // Ленивая инициализация
            }
            private set
            {
                _interestLinks = value;
            }
        }

        /// <summary>
        /// Список связей между пользователем и языками (сущности типа ProfileLanguage).
        /// </summary>
        [JsonIgnore]
        [ValidateNever]
        public List<ProfileLanguage> LanguageLinks
        {
            get
            {
                return _languageLinks ??= new List<ProfileLanguage>(); // Ленивая инициализация
            }
            private set
            {
                _languageLinks = value;
            }
        }

        /// <summary>
        /// Метод для инициализации и преобразования данных, связанных с файлом, интересами и языками.
        /// </summary>
        public void BeforeExecute(bool bufferizeFiles = false)
        {
            UserId = UserId == Guid.Empty
                ? Guid.NewGuid()
                : UserId;

            // Инициализация файлов
            foreach (var formFile in FormFiles)
            {
                // Создание DTO для файла
                var fileDto = new FileDto
                {
                    FormFile = formFile
                };

                // Инициализация данных в DTO
                fileDto.BeforeExecute(bufferizeFiles);

                // Добавление DTO в список
                FilesDto.Add(fileDto);

                // Создание сущности File и добавление в список
                var file = new File
                {
                    FileId = fileDto.SafeFileName
                };

                Files.Add(file);
            }

            // Преобразование списка идентификаторов интересов в список объектов ProfileInterest
            foreach (var interestId in InterestsId)
            {
                var profileInterest = new ProfileInterest
                {
                    ProfileId = UserId,
                    InterestId = interestId
                };

                InterestLinks.Add(profileInterest);
            }

            // Преобразование списка идентификаторов языков в список объектов ProfileLanguage
            foreach (var languageId in LanguagesId)
            {
                var profileLanguage = new ProfileLanguage
                {
                    ProfileId = UserId,
                    LanguageId = languageId
                };

                LanguageLinks.Add(profileLanguage);
            }
        }

        // Реализация IDisposable для освобождения ресурсов
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Основной метод Dispose, который освобождает ресурсы
        protected virtual void Dispose(bool disposing)
        {
            // Проверяем, был ли объект уже освобожден
            if (_disposed)
                return;

            // Если disposing == true, это означает, что метод вызван явно, и можно освободить как управляемые, так и неуправляемые ресурсы
            if (disposing)
            {
                // Освобождение ресурсов файлов DTO
                if (_filesDto != null)
                {
                    foreach (var fileDto in _filesDto)
                    {
                        fileDto.Dispose();
                    }
                    _filesDto.Clear();
                    _filesDto = null;
                }

                // Очистка списка файлов
                if (_files != null)
                {
                    _files.Clear();
                    _files = null;
                }

                // Очистка списка связей интересов
                if (_interestLinks != null)
                {
                    _interestLinks.Clear();
                    _interestLinks = null;
                }

                // Очистка списка связей языков
                if (_languageLinks != null)
                {
                    _languageLinks.Clear();
                    _languageLinks = null;
                }

                // Очистка списка файлов (IFormFile)
                if (FormFiles != null)
                {
                    FormFiles.Clear();
                    FormFiles = null;
                }
            }

            // Отметим, что объект освобожден
            _disposed = true;
        }

        // Финализатор (сработает только если Dispose не был вызван)
        ~ProfileDto()
        {
            Dispose(false);
        }
    }
}
