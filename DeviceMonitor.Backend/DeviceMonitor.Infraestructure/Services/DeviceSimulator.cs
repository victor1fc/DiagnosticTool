using DeviceMonitor.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceMonitor.Infrastructure.Services
{
    public class DeviceSimulator : ISshService
    {
        private bool _isConnected;
        private Dictionary<string,string> _commands = new Dictionary<string, string>() {
            ["ls"] = "bin app var home usr",
            ["uname -a"] = "Device 1.0"
        };
        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void ExecuteCommand()
        {
            throw new NotImplementedException();
        }
    }
}
