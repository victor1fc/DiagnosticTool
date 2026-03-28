using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceMonitor.CrossCutting.IoC
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddProjectServices (this IServiceCollection services)
        {
            //services.AddScoped<ISshService, SshService>();
            return services;
        }
    }
}
