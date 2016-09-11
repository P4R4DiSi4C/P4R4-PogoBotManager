using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;

namespace P4R4_PokeMob_Creator.Classes
{
    public class Stats
    {
        private MainForm _mainForm;


        public Stats(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Returns a string containing information on running processes
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><returns></returns></span>
        public void ListAllApplications()
        {
            foreach (Process p in Process.GetProcesses("."))
            {
                try
                {
                    if (p.MainWindowTitle.Length > 0 && p.ProcessName.Contains("PokeMobBot"))
                    {

                        TimeSpan runningTime = DateTime.Now - p.StartTime;

                        _mainForm.AddProcessList(p.MainWindowTitle.ToString(), p.ProcessName.ToString(), runningTime.ToString());

                    }
                }
                catch { }
            }
        }
    }
}
