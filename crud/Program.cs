using WindFormCrud;

namespace crud
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            
            login formlogin = new login();

            formlogin.ShowDialog();

            if (formlogin.DialogResult == DialogResult.OK)
            {
                Application.Run(new MDIformularioMain());
            }
            
            //Application.Run(new MDIformularioMain());

        }



    }
}