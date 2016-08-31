
using System.Windows.Forms;

namespace P4R4_PokeMob_Creator
{
    public class Logger
    {
        //Reference the mainForm
        private MainForm _mainForm;

        /// <summary>
        /// Default constructore of this class
        /// </summary>
        public Logger(MainForm mainForm)
        {
            //Link the form to the class
            _mainForm = mainForm;
        }

        /// <summary>
        /// Method to append a text to the logs richtxtbox
        /// </summary>
        /// <param name="logText"></param>
        public void AppendLog(string logText)
        {
            _mainForm.CreationLogsActions(logText + "\n", false);
        }

        /// <summary>
        /// Method to clear the logs
        /// </summary>
        public void ClearLogs()
        {
            _mainForm.CreationLogsActions("", true);
        }
    }
}
