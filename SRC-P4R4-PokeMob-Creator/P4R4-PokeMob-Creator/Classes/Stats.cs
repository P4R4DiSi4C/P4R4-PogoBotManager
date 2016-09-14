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
            //Loop through each process
            foreach (Process p in Process.GetProcesses("."))
            {
                try
                {
                    //Check if the process name contains PokeMobBot and it's bigger than 0 length.
                    if (p.MainWindowTitle.Length > 0 && p.ProcessName.Contains("PokeMobBot"))
                    {
                        //Create a timespan var to time that the process is runnig
                        //Substract current Time now to the start time
                        TimeSpan runningTime = DateTime.Now - p.StartTime;

                        int processID = p.Id;

                        //Call the method to add a process to the list of processes
                        _mainForm.AddProcessList(processID, p.MainWindowTitle.ToString(), p.ProcessName.ToString(), runningTime.ToString());
                    }
                }
                catch { }
            }
        }
    }
}
