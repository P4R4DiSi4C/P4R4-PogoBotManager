﻿using System;
using System.Windows.Forms;

namespace P4R4_PokeMob_Creator
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

            //Initialize the class with the form as parameter
            PokeMobUtils mainClass = new PokeMobUtils();

            //Initialize the form
            MainForm mainForm = new MainForm(mainClass);
            
            //Start the app
            Application.Run(mainForm);
        }
    }
}
