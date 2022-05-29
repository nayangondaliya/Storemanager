using Raditap.BusinessLogic.Interfaces.Helpers;
using Raditap.DataObjects.AppSettings;
using System.IO;

namespace Raditap.BusinessLogic.Helpers
{
    public class FileOperationHelper : IFileOperationHelper
    {
        private readonly AppSettings _appSettings;

        public FileOperationHelper(AppSettings appSettings)
        {
            _appSettings = appSettings;
            if (!Directory.Exists(_appSettings.FileWritePath))
                Directory.CreateDirectory(_appSettings.FileWritePath);
        }

        public byte[] GetFile(string fileName) => File.ReadAllBytes($"{_appSettings.FileWritePath}{fileName}");

        public string GetFullPath(string fileName) => $"{_appSettings.FileWritePath}{fileName}";

        public void RemoveFile(string fileName)
        {
            if(File.Exists($"{_appSettings.FileWritePath}{fileName}"))
                File.Delete($"{_appSettings.FileWritePath}{fileName}");
        }

        public void ReplaceFile(string oldFileName, string newFileName, byte[] file)
        {
            this.RemoveFile(oldFileName);
            this.WriteFile(newFileName, file);
        }

        public void WriteFile(string fileName, byte[] file) => File.WriteAllBytes($"{_appSettings.FileWritePath}{fileName}", file);
    }
}
