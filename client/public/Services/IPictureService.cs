using HxH.App.Models;

namespace HxH.Services
{
    public interface IPictureService
    {
        Result<byte[]> ReadFile(IFormFile formFile, bool validateFileSignature = true);
        Result<string> UploadFile(IFormFile formFile, bool validateFileSignature = true);
        void RemoveFile(string fileName);
        bool IsFileLarge(long fileSize);
        bool IsFileEmpty(long fileSize);
        bool IsFileSignatureEqual(Stream stream, IEnumerable<byte[]> fileSignatures);
    }
}