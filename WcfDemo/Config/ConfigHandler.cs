namespace WcfDemo
{
    public class ConfigHandler
    {
        private readonly string _username = "foo@bar";
        private readonly string _password = "***";

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
