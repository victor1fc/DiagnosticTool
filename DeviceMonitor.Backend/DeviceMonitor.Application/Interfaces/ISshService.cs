using DeviceMonitor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceMonitor.Application.Interfaces
{
    public interface ISshService
    {
        public bool Connect(string host, int port, string username, string password);
        public CommandResult ExecuteCommand(string command);
        public void Disconnect();
    }
}
