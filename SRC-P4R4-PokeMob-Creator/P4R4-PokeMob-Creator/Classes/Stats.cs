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
        /// <span class="code-SummaryComment"><summary></span>
        /// Returns a string containing information on running processes
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><returns></returns></span>
        public string ListAllApplications()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Process p in Process.GetProcesses("."))
            {
                try
                {
                    if (p.MainWindowTitle.Length > 0 && p.ProcessName.Contains("PokeMobBot"))
                    {
                        sb.Append("Window Title:\t" +
                            p.MainWindowTitle.ToString()
                            + Environment.NewLine);

                        sb.Append("Process Name:\t" +
                            p.ProcessName.ToString()
                            + Environment.NewLine);

                        sb.Append("Window Handle:\t" +
                            p.MainWindowHandle.ToString()
                            + Environment.NewLine);

                        sb.Append("Memory Allocation:\t" +
                            p.PrivateMemorySize64.ToString()
                            + Environment.NewLine);

                        sb.Append(Environment.NewLine);
                    }
                }
                catch { }
            }

            return sb.ToString();
        }
    }
}
