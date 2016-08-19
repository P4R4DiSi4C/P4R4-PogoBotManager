using System;
using System.Windows.Forms;

namespace P4R4_PogoBotsManager
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Initialize the form
            mainForm mainForm = new mainForm();

            //Initialize the class with the form as parameter
            mainClass mainClass = new mainClass(mainForm);
            
            //Start the app
            Application.Run(mainForm);
        }
    }
}
