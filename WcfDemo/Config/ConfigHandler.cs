using System;
using System.IO;

namespace WcfDemo
{
    public class ConfigHandler
    {
        private readonly string fileName = "config.txt";

        public string GetUserName()
        {
            var isFilePresent = CheckOrCreateConfigFile(out string filePath);
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

        public string GetPassword()
        {
            var isFilePresent = CheckOrCreateConfigFile(out string filePath);
            if (!isFilePresent)
            {
                return null;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    streamReader.ReadLine();
                    return streamReader.ReadLine();
                }
            }
        }

        private bool CheckOrCreateConfigFile(out string filePath)
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            filePath = Path.Combine(desktopPath, fileName);

            return File.Exists(filePath);
        }
    }
}
