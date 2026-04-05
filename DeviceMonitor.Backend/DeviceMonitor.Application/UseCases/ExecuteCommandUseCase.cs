using DeviceMonitor.Application.DTOs;
using DeviceMonitor.Application.Interfaces;
using DeviceMonitor.Application.Services;
using DeviceMonitor.Domain;
using DeviceMonitor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceMonitor.Application.UseCases
{
    public class ExecuteCommandUseCase
    {
        ISshService SshService;
        DeviceService DeviceService;
        public ExecuteCommandUseCase(ISshService sshService, DeviceService deviceService)
        {         
            this.SshService = sshService;
            this.DeviceService = deviceService;
        }

        public CommandResponse ExecuteCommand(CommandRequest request)
        {
            CommandResponse response = new CommandResponse();
            DeviceStatus deviceStatus = this.DeviceService.GetDeviceStatus();

            if (deviceStatus != DeviceStatus.Connected)
            {
                response.Status = Status.Error;
                response.StatusMessage = "ExecuteCommandError: device is not connected";
                return response;
            }

            CommandResult result = SshService.ExecuteCommand(request.Command);
            if (result.IsError)
            {
                response.Status = Status.Error;
                response.StatusMessage = "Execute command failed.";
                response.StatusMessage += " Exit Code:" + result.ExitCode.ToString();
                return response;
            }

            response.Status = Status.Success;
            response.StatusMessage = "Command was executed successfully!";
            response.Output = result.Output;
            return response;
        }
    }
}
