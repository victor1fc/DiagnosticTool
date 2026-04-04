using DeviceMonitor.Application.Interfaces;
using DeviceMonitor.Domain;
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
            ["device"] = "Device 1.0"
        };
        public bool Connect(string host, int port, string username, string password)
        {
            _isConnected = true;
            return _isConnected;
        }

        public void Disconnect()
        {
            _isConnected = false;
        }

        public CommandResult ExecuteCommand(string command)
        {
            CommandResult result = new CommandResult();

            if (!_isConnected)
            {
                result.Output = "device not connected!";
                result.ExitCode = 1;
                result.IsError = true;
                return result;
            }
            if (_commands.TryGetValue(command, out string output))
            { 
                result.Output = output;
                result.ExitCode = 0;
                result.IsError = false;                
            }
            else
            {
                result.Output = "command not found";
                result.ExitCode = 1;
                result.IsError = true;
            }         
            return result;
        }    
    }
}
