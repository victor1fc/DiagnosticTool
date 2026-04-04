using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceMonitor.Application.Interfaces
{
    public interface ISshService
    {
        public void Connect();
        public void ExecuteCommand();
        public void Disconnect();
    }
}
