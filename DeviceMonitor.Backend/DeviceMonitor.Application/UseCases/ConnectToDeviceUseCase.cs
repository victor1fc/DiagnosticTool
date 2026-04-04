using DeviceMonitor.Application.DTOs;
using DeviceMonitor.Application.Interfaces;
using DeviceMonitor.Application.UseCases.Entities;
using DeviceMonitor.Domain.Entities;
using DeviceMonitor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace DeviceMonitor.Application.UseCases
{
    public class ConnectToDeviceUseCase 
    {
        ISshService sshService;
        public ConnectToDeviceUseCase(ISshService sshService) {
            this.sshService = sshService;
        }

        public ConnectResponse ConnectToDevice(ConnectRequest request)
        {
            string host = request.Host;
            int port = request.Port;
            string username = request.Username;
            string password = request.Password;

            ConnectResponse status = new ConnectResponse();

            ConnectResponse hostValidation = ValidateHost(host);
         
            if (hostValidation.Status == Status.Success)
            {
                ConnectResponse portValidation = ValidatePort(port);

                if (portValidation.Status == Status.Success)
                {
                    if (username != null)
                    {
                        if (password != null)
                        {
                            Device device = new Device(host, port);
                            device.SetConnecting();
                            if (sshService.Connect(host, port, username, password))
                            {
                                device.SetConnected();
                                status.Status = Status.Success;
                                return status;
                            }
                            else
                            {
                                device.SetDisconnected();
                                status.Status = Status.Error;
                                status.StatusMessage = "Connection failed!";
                                return status;
                            }
                        }
                        else
                        {
                            status.Status = Status.Error;
                            status.StatusMessage = "Empty Password!";
                            return status;
                        }
                    }
                    else
                    {
                        status.Status = Status.Error;
                        status.StatusMessage = "Empty Username!";
                        return status;
                    }
                }
                else
                {
                    return portValidation;
                }
            }
            else
            {
                return hostValidation;
            }   
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
                if (int.Parse(Ip) > 255 || int.Parse(Ip) < 0)
                {
                    status.Status = Status.Error;
                    status.StatusMessage = "Host must be between 0 and 255!";
                    return status;
                }
            }

            status.Status = Status.Success;
            return status;
        }
        private ConnectResponse ValidatePort(int port)
        {
            ConnectResponse status = new ConnectResponse();

            if (port < 0)
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
