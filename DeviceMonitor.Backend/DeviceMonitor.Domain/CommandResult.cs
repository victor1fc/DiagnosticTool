using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceMonitor.Domain
{
    public class CommandResult
    {
        public string Output { get;  set; }
        public int ExitCode {  get;  set; }
        public bool IsError {  get;  set; }
        

    }
}
