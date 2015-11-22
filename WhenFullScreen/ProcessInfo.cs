using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenFullScreen
{
    public class ProcessInfo
    {
        public ProcessInfo()
        {
            this.fileName = "";
            this.processName = "";
        }

        public ProcessInfo(string file, string process)
        {
            this.fileName = file;
            this.processName = process;
        }

        public string fileName
        { get; set; }
        public string processName
        { get; set; }
    }
}
