using DeviceMonitor.Application.DTOs;
using DeviceMonitor.Application.Interfaces;
using DeviceMonitor.Application.Services;
using DeviceMonitor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeviceMonitor.Application.UseCases
{
    public class ConnectToDeviceUseCase 
    {
        ISshService SshService;
        DeviceService DeviceService;
        public ConnectToDeviceUseCase(ISshService sshService, DeviceService deviceService) {
            this.SshService = sshService;
            this.DeviceService = deviceService;
        }

        public ConnectResponse ConnectToDevice(ConnectRequest request)
        {
            string host = request.Host;
            int port = request.Port;
            string username = request.Username;
            string password = request.Password;

            ConnectResponse status = new ConnectResponse();

            ConnectResponse hostValidation = ValidateHost(host);

            ConnectResponse portValidation = ValidatePort(port);

            if (hostValidation.Status != Status.Success) return hostValidation;
            if (portValidation.Status != Status.Success) return portValidation;
            if (username == null) 
            {
                status.Status = Status.Error;
                status.StatusMessage = "Empty Username!";
                return status;
            }
            if(password == null)
            {
                status.Status = Status.Error;
                status.StatusMessage = "Empty Password!";
                return status;
            }                              
                   
            this.DeviceService.Initialize(host, port);           
            this.DeviceService.SetConnecting();

            bool connected = SshService.Connect(host, port, username, password);
            if (connected)
            {
                this.DeviceService.SetConnected();
                status.Status = Status.Success;
            }
            else
            {
                this.DeviceService.SetDisconnected();
                status.Status = Status.Error;
                status.StatusMessage = "Connection failed!";
            }
            

            return status;
        }

        private ConnectResponse ValidateHost(string host)
        {
            ConnectResponse status = new ConnectResponse();

            if (string.IsNullOrWhiteSpace(host))
            {
                status.Status = Status.Error;
                status.StatusMessage = "Empty Host!";
                return status;
            }

            string[] IpParser = host.Split(".");
            if (IpParser.Length != 4) {
                status.Status = Status.Error;
                status.StatusMessage = "Invalid Host!";
                return status;
            }
            foreach (string Ip in IpParser)
            {                
                bool isInt = int.TryParse(Ip, out int ipNumber);

                if(isInt)
                {
                    if (ipNumber > 255 || ipNumber < 0)
                    {
                        status.Status = Status.Error;
                        status.StatusMessage = "Host must be between 0 and 255!";
                        return status;
                    }
                }
                else
                {
                    status.Status = Status.Error;
                    status.StatusMessage = "Host must be only numbers!";
                    return status;
                }
            }

            status.Status = Status.Success;
            return status;
        }
        private ConnectResponse ValidatePort(int port)
        {
            ConnectResponse status = new ConnectResponse();

            if (port < 0 || port > 65535)
            {
                status.Status = Status.Error;
                status.StatusMessage = "Invalid Port!";
                return status;
            }
            status.Status = Status.Success;
            return status;
        }


    }
}
