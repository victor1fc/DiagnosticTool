using DeviceMonitor.Domain.Enums;

namespace DeviceMonitor.Domain.Entities
{
    public class Device
    {
        public int Id { get; private set; }
        public string Ip { get; private set; }
        public int Port { get; private set; }
        public DeviceStatus Status { get; private set; }

        public Device (string ip, int port)
        {
            Ip = ip;
            Port = port;
            Status = DeviceStatus.Unknown;
        }

        public void SetConnecting() { 
            Status = DeviceStatus.Connecting;
        }
        public void SetConnected() {
            Status = DeviceStatus.Connected;
        }
        public void SetDisconnecting() { 
            Status = DeviceStatus.Disconnecting;
        }
        public void SetDisconnected() {
            Status = DeviceStatus.Disconnected;
        }


    }
}