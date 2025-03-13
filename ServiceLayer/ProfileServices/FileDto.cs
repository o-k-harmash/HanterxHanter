using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ServiceLayer.ProfileServices
{
    public class FileDto : IDisposable
    {
        // Приватные поля
        private string _fileName;
        private string _safeFileName;
        private string _extension;
        private byte[] _fileBuffer;
        private Stream _stream;

        // Флаг, который отслеживает, был ли объект уже освобожден
        private bool _disposed = false;

        // Исходный FormFile
        [JsonIgnore]
        public IFormFile FormFile { get; set; }

        // Имя исходного файла (может быть пустым до выполнения BeforeExecute)
        [JsonIgnore]
        [ValidateNever]
        public string FileName
        {
            get => _fileName;
            private set => _fileName = value;
        }

        // Безопасное имя файла (сгенерированное)
        [JsonIgnore]
        [ValidateNever]
        public string SafeFileName
        {
            get => _safeFileName;
            private set => _safeFileName = value;
        }

        // Расширение файла (например, .jpg, .png)
        [JsonIgnore]
        [ValidateNever]
        public string Extension
        {
            get => _extension;
            private set => _extension = value;
        }

        // Буфер данных файла
        [JsonIgnore]
        [ValidateNever]
        public byte[] FileBuffer
        {
            get => _fileBuffer;
            private set => _fileBuffer = value;
        }

        // Поток данных файла
        [JsonIgnore]
        [ValidateNever]
        public Stream Stream
        {
            get => _stream;
            private set => _stream = value;
        }

        public bool IsBufferCopied { get; private set; }

        // Метод для подготовки данных файла: генерирует имя, расширение и читает содержимое файла
        public void BeforeExecute(bool copyBuffer = false)
        {
            // Генерация имени файла и его расширения
            FileName = FormFile.FileName;
            Extension = Path.GetExtension(FileName);

            // Генерация нового безопасного имени файла, использующего GUID
            SafeFileName = $"{Guid.NewGuid()}{Extension}";

            // Открытие потока для чтения файла
            var stream = FormFile.OpenReadStream();

            // Если требуется копирование содержимого в буфер
            if (copyBuffer)
            {
                // Чтение содержимого файла в буфер (массив байт)
                var fileBuffer = new byte[FormFile.Length];
                stream.ReadExactly(fileBuffer); // Прочитаем все данные

                // Сохраняем содержимое в поле FileBuffer
                FileBuffer = fileBuffer;

                IsBufferCopied = true;

                // Закрываем поток
                stream.Close();
            }
            else
            {
                // Если не требуется копирование, сохраняем сам поток
                Stream = stream;

                IsBufferCopied = false;
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

            // Если disposing == true, это означает, что метод вызван явно, и можно освободить управляемые ресурсы
            if (disposing)
            {
                // Закрытие потока, если он открыт
                if (_stream != null)
                {
                    _stream.Dispose();
                    _stream = null;
                }

                // Если был скопирован буфер, нужно также освободить память
                if (_fileBuffer != null)
                {
                    Array.Clear(_fileBuffer, 0, _fileBuffer.Length); // Очистка буфера
                    _fileBuffer = null;
                }

                // Если был передан поток, тоже очищаем
                if (FormFile != null)
                {
                    FormFile = null;
                }
            }

            // Отметим, что объект освобожден
            _disposed = true;
        }

        // Финализатор (сработает только если Dispose не был вызван)
        ~FileDto()
        {
            Dispose(false);
        }
    }
}
