using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            //Initialize the class
            mainClass mainClass = new mainClass();
            //Initialize the form with the class as a parameter
            mainForm mainForm = new mainForm(mainClass);
            //Start the app
            Application.Run(mainForm);
        }
    }
}
