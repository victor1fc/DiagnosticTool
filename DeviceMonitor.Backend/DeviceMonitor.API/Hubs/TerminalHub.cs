using Microsoft.AspNetCore.SignalR;

namespace DeviceMonitor.API.Hubs
{
    public class TerminalHub : Hub
    {
        public async Task SendLog(string message)
        {
            await Clients.All.SendAsync("ReceiveLog", message);
        }
    }
}
