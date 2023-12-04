using WindFormCrud;

namespace crud
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            
            login formlogin = new login();

            formlogin.ShowDialog();

            if (formlogin.DialogResult == DialogResult.OK)
            {
                Application.Run(new MDIformularioMain());
            }   
        }
    }
}