namespace WindFormCrud
{
    public static class UserNameLogin
    {
        public static string UserName { get; set; }

        public static string TipoPerfil { get; set; }

        public static void setTipoPerfil(string tipoperfil)
        {
           TipoPerfil  = tipoperfil;
        }

        public static void setUserName(string userName)
        {
            UserName = userName;
        }

        
    }
}
