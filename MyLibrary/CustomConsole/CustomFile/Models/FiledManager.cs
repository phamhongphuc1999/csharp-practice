using System.IO;

namespace MyLibrary.CustomConsole.CustomFile.Models
{
    public class FiledManager
    {
        public static FileResult CopyFile(string sourcePath, string destPath)
        {
            if (!File.Exists(sourcePath)) return new FileResult
            {
                message = $"The path: {sourcePath} not found",
                result = false
            };
            if (File.Exists(destPath)) return new FileResult
            {
                message = $"The file with path: {destPath} is already exists",
                result = false
            };
            FileStream sourceFile = new FileStream(sourcePath, FileMode.Open);
            FileStream destFile = new FileStream(destPath, FileMode.Create);
            long count = sourceFile.Length;
            byte[] data = new byte[count];
            sourceFile.Read(data, 0, (int)count);
            destFile.Write(data, 0, (int)count);
            sourceFile.Close();
            destFile.Close();
            return new FileResult
            {
                message = null,
                result = true
            };
        }
    }
}
