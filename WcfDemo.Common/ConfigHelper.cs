using System;
using System.IO;

namespace WcfDemo.Common
{
    public static class ConfigHelper
    {
        public static string GetHostAddress(string fileName)
        {
            var isFilePresent = CheckOrCreateConfigFile(fileName, out string filePath);
            if (!isFilePresent)
            {
                return null;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    return streamReader.ReadLine();
                }
            }
        }

        public static bool CheckOrCreateConfigFile(string filePath, out string fullFilePath)
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fullFilePath = Path.Combine(desktopPath, filePath);

            return File.Exists(fullFilePath);
        }

    }
}
