namespace WindFormCrud
{
    public static class UserNameLogin
    {
        public static string UserName { get; private set; }
        public static void SetUserName(string userName)
        {
            UserName = userName;
        }
    }
}
