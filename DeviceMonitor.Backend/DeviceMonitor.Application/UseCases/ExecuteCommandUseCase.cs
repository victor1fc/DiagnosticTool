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
        readonly ISshService _sshService;
        readonly DeviceService _deviceService;
        readonly CommandBlocker _commandBlocker;

        public ExecuteCommandUseCase(ISshService sshService, DeviceService deviceService, CommandBlocker commandBlocker)
        {         
            this._sshService = sshService;
            this._deviceService = deviceService;
            this._commandBlocker = commandBlocker;
        }

        public CommandResponse ExecuteCommand(CommandRequest request)
        {
            CommandResponse response = new CommandResponse();

            if(string.IsNullOrWhiteSpace(request.Command))
            {
                response.Status = Status.Error;
                response.StatusMessage = "Command cannot be null or empty";
                return response;
            }
            if(!_commandBlocker.CheckIsBlocked(request.Command))
            {
                response.Status = Status.Error;
                response.StatusMessage = "Command not allowed: " + request.Command;
                return response;
            }

            DeviceStatus deviceStatus = this._deviceService.GetDeviceStatus();

            if (deviceStatus != DeviceStatus.Connected)
            {
                response.Status = Status.Error;
                response.StatusMessage = "ExecuteCommandError: device is not connected";
                return response;
            }

            CommandResult result = _sshService.ExecuteCommand(request.Command);
            if (result.IsError)
            {
                response.Status = Status.Error;
                response.StatusMessage = "Execute command failed.";
                response.StatusMessage += " Exit Code:" + result.ExitCode.ToString();
                response.Output = result.Output;
                return response;
            }

            response.Status = Status.Success;
            response.StatusMessage = "Command was executed successfully!";
            response.Output = result.Output;
            return response;
        }
    }
}
