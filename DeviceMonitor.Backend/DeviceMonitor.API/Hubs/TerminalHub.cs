using DeviceMonitor.Application.DTOs;
using DeviceMonitor.Application.UseCases;
using Microsoft.AspNetCore.SignalR;

namespace DeviceMonitor.API.Hubs
{
    public class TerminalHub : Hub
    {
        private readonly ConnectToDeviceUseCase _connectToDeviceUseCase;
        private readonly ExecuteCommandUseCase _executeCommandUseCase;
        public TerminalHub(ConnectToDeviceUseCase connectToDeviceUseCase, ExecuteCommandUseCase executeCommandUseCase) 
        {
            this._connectToDeviceUseCase = connectToDeviceUseCase;
            this._executeCommandUseCase = executeCommandUseCase;
        }

        public async Task ConnectToDevice(ConnectRequest request)
        {
            ConnectResponse response =  this._connectToDeviceUseCase.ConnectToDevice(request);
            await Clients.Caller.SendAsync("ReceiveConnection", response);
        }
        public async Task SendCommand(CommandRequest request)
        {
            CommandResponse response = this._executeCommandUseCase.ExecuteCommand(request);
            await Clients.Caller.SendAsync("ReceiveCommandOutput", response);
        }
    }
}
