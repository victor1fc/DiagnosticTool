using DeviceMonitor.Domain.Entities;
using DeviceMonitor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceMonitor.Application.Services
{    
    public class DeviceService
    {
        private Device Device { get;  set; }
        public void Initialize(string host, int port)
        {   
           Device = new Device(host, port);       
        }
        public void SetConnecting()
        {
            if (this.Device != null)
            {
                if (Device.Status != DeviceStatus.Connected && Device.Status != DeviceStatus.Connecting)
                {
                    Device.SetConnecting();
                }               
            }
        }
        public void SetConnected()
        {
            if (this.Device != null)
            { 
                Device.SetConnected();
            }
        }
        public void SetDisconnecting()
        {
            if (this.Device != null)
            {
                if (Device.Status != DeviceStatus.Disconnecting && Device.Status != DeviceStatus.Disconnected)
                {
                    Device.SetDisconnecting();
                }
            }
        }
        public void SetDisconnected()
        {
            if (this.Device != null)
            {
                Device.SetDisconnected();
            }
        }

        public DeviceStatus GetDeviceStatus()
        {   if (this.Device != null)
            {
                return Device.Status;
            }
            else 
            { 
                return DeviceStatus.Unknown; 
            }
            
        }

    }
}
