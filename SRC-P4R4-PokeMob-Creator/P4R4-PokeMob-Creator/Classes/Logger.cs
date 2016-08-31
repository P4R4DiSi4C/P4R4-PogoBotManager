
<<<<<<< HEAD
using System;
=======
using System.Windows.Forms;
>>>>>>> refs/remotes/origin/master

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
<<<<<<< HEAD
            if (_mainForm._creationLogsRichTxtBox.InvokeRequired)
            {
                _mainForm._creationLogsRichTxtBox.BeginInvoke(new Action(delegate {
                    AppendLog(logText);
                }));
                return;
            }

            _mainForm._creationLogsRichTxtBox.AppendText(logText + "\n");
=======
            _mainForm.CreationLogsActions(logText + "\n", false);
>>>>>>> refs/remotes/origin/master
        }

        /// <summary>
        /// Method to clear the logs
        /// </summary>
        public void ClearLogs()
        {
            _mainForm._creationLogsRichTxtBox.Clear();
        }
    }
}
