using DeviceMonitor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceMonitor.Application.DTOs
{
    public class ConnectResponse
    {
        public Status Status { get; set; }
        public string StatusMessage { get; set; }
    }
}
