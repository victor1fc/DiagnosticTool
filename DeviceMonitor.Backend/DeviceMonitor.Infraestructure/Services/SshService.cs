using DeviceMonitor.Application.Interfaces;
using DeviceMonitor.Domain;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeviceMonitor.Infrastructure.Services
{
    public class SshService : ISshService
    {
        private SshClient? _client;
        public bool Connect(string host, int port, string username, string password)
        {
            try
            {
                _client = new SshClient(host, port, username, password);
                _client.Connect();
                if (_client.IsConnected)
                {
                    return true;
                }
            }
            catch
            {
                return false;                
            }
           
            return false;
        }

        public void Disconnect()
        {
            if (_client != null)
            {
                if (_client.IsConnected) 
                {
                    _client.Disconnect();
                    _client.Dispose();
                    _client = null;
                }                
            }       
        }

        public CommandResult ExecuteCommand(string command)
        {
            CommandResult result = new CommandResult(); 
            
            if(_client == null)
            { 
                result.IsError = true;
                result.ExitCode = 1;
                result.Output = "ExecuteCommand is failed, client is null";
                return result;
            }
                
            SshCommand response = _client.RunCommand(command);
           
            result.ExitCode = (int)response.ExitStatus;
            if (response.ExitStatus != 0) 
            {
                result.Output = response.Error;
            }
            else
            {
                result.Output = response.Result;
            }
            
            result.IsError = response.ExitStatus != 0;
            return result;
    
        }
    }
}
