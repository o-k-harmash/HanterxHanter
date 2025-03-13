using HxH.App.Models;
using Microsoft.Extensions.Options;

namespace HxH.Services
{
    public class PictureService : IPictureService
    {
        private readonly PictureServiceOptions _options;
        private readonly ILogger<PictureService> _logger;

        public PictureService(ILogger<PictureService> logger, IOptions<PictureServiceOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public bool IsFileLarge(long fileSize)
        {
            return fileSize > _options.maxFileSize;
        }

        public bool IsFileEmpty(long fileSize)
        {
            return fileSize == 0;
        }

        public bool IsFileExtensionPermitted(string fileExtension)
        {
            return _options.permittedFileExtensions.Contains(fileExtension);
        }

        public bool IsFileSignatureEqual(Stream stream, IEnumerable<byte[]> fileSignatures)
        {
            using (var reader = new BinaryReader(stream))
            {
                var headerBytes = reader.ReadBytes(fileSignatures.Max(m => m.Length));

                return fileSignatures.Any(signature =>
                    headerBytes.Take(signature.Length).SequenceEqual(signature));
            }
        }

        private Result<T> ProcessFile<T>(IFormFile formFile, bool validateFileSignature, Func<Stream, T> processFile)
        {
            var result = new Result<T>();

            // Проверка на пустоту
            if (IsFileEmpty(formFile.Length))
            {
                return result += new ArgumentException("File cannot be empty.", nameof(formFile));
            }

            // Проверка на размер
            if (IsFileLarge(formFile.Length))
            {
                return result += new ArgumentOutOfRangeException(nameof(formFile), "File size exceeds the limit.");
            }

            var fileExtension = Path.GetExtension(formFile.FileName);

            // Проверка на разрешенные расширения
            if (string.IsNullOrEmpty(fileExtension) || !IsFileExtensionPermitted(fileExtension))
            {
                return result += new NotSupportedException($"File extension '{fileExtension}' is not allowed.");
            }

            try
            {
                using (var stream = formFile.OpenReadStream())
                {
                    // Валидация подписи файла
                    if (validateFileSignature)
                    {
                        if (!IsFileSignatureEqual(stream, _options.fileSignatures[fileExtension]))
                        {
                            return result += new InvalidOperationException("File signature validation failed.");
                        }
                    }

                    // Обработка файла с помощью переданной делегированной функции
                    var processedData = processFile(stream);

                    return result += processedData;
                }
            }
            catch (Exception fileEx)
            {
                _logger.LogCritical(fileEx.Message);
                return result += new InvalidOperationException("An error occurred while processing the file.", fileEx);
            }
        }

        public Result<string> UploadFile(IFormFile formFile, bool validateFileSignature = true)
        {
            return ProcessFile(formFile, validateFileSignature, stream =>
            {
                var fileName = string.Concat(
                    Path.GetRandomFileName(),
                    Path.GetExtension(formFile.FileName));
                var filePath = Path.Combine(_options.rootFilePath, fileName);

                using (var file = System.IO.File.Create(filePath))
                {
                    stream.CopyTo(file);
                }

                return fileName; // Возвращаем имя файла
            });
        }


        public Result<byte[]> ReadFile(IFormFile formFile, bool validateFileSignature = true)
        {
            return ProcessFile(formFile, validateFileSignature, stream =>
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray(); // Возвращаем содержимое файла в виде массива байтов
                }
            });
        }

        public void RemoveFile(string fileName)
        {
            System.IO.File.Delete(Path.Combine(_options.rootFilePath, fileName));
        }
    }
}


