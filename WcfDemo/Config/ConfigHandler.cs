namespace WcfDemo
{
    public class ConfigHandler
    {
        private readonly string _username = "xyz";
        private readonly string _password = "abc";

        public string GetUserName()
        {
            return _username;
        }

        public string GetPassword()
        {
            return _password;
        }
    }
}
