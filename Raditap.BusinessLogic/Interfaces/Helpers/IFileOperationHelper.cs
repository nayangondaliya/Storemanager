namespace Raditap.BusinessLogic.Interfaces.Helpers
{
    public interface IFileOperationHelper
    {
        void WriteFile(string fileName, byte[] file);
        void RemoveFile(string fileName);
        void ReplaceFile(string oldFileName, string newFileName, byte[] file);
        string GetFullPath(string fileName);
        byte[] GetFile(string fileName);
    }
}
