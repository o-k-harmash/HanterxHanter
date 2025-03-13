namespace HxH.Services
{
    public class PictureServiceOptions
    {
        public static readonly string optionString = "PictureServiceOptions";

        public readonly Dictionary<string, List<byte[]>> fileSignatures = new Dictionary<string, List<byte[]>>
        {
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                }
            },
        };

        public string rootFilePath = "C://";
        public long maxFileSize = 2097152;
        public string[] permittedFileExtensions = { ".jpeg" };
    }
}