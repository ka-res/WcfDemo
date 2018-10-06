using System;
using System.IO;

namespace WcfDemo
{
    public class ConfigHandler
    {
        private readonly string credentialsFileName = "config.txt";
        private readonly string mailBodyFileName = "mailBody.txt";

        public string GetUserName()
        {
            var isFilePresent = CheckOrCreateConfigFile(credentialsFileName, out string filePath);
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
            var isFilePresent = CheckOrCreateConfigFile(credentialsFileName, out string filePath);
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

        public string GetMailBody()
        {
            var isFilePresent = CheckOrCreateConfigFile(mailBodyFileName , out string filePath);
            if (!isFilePresent)
            {
                return null;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    return streamReader.ReadToEnd();
                }
            }

        }

        private bool CheckOrCreateConfigFile(string filePath, out string fullFilePath)
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fullFilePath = Path.Combine(desktopPath, filePath);

            return File.Exists(fullFilePath);
        }
    }
}
