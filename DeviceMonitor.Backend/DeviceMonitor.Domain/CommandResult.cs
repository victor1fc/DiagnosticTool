using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceMonitor.Domain
{
    public class CommandResult
    {
        public string Output { get; private set; }
        public int ExitCode {  get; private set; }
        public bool isError {  get; private set; }
        

    }
}
