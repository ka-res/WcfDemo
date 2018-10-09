using System.IO;
using WcfDemo.Common;

namespace WcfDemo
{
    public class ConfigHandler
    {
        private readonly string credentialsFileName = "credentials.txt";
        private readonly string mailBodyFileName = "mailBody.txt";

        public string GetUserName()
        {
            var isFilePresent = ConfigHelper.CheckOrCreateConfigFile(credentialsFileName, out string filePath);
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
            var isFilePresent = ConfigHelper.CheckOrCreateConfigFile(credentialsFileName, out string filePath);
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
            var isFilePresent = ConfigHelper.CheckOrCreateConfigFile(mailBodyFileName, out string filePath);
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
    }
}
