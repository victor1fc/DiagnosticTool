using DeviceMonitor.Application.Interfaces;
using DeviceMonitor.Application.Services;
using DeviceMonitor.Application.UseCases;
using DeviceMonitor.Domain;
using DeviceMonitor.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
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

        public static IServiceCollection AddProjectServices (this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ISshService, SshService>();
            bool useSimulator = configuration.GetValue<bool>("UseSimulator");
            if (useSimulator)
            {
               services.AddSingleton<ISshService, DeviceSimulator>();
            }
            else
            {
               services.AddSingleton<ISshService, SshService>();
            }
            
            services.AddSingleton<DeviceService>();
            services.AddSingleton<ConnectToDeviceUseCase>();
            services.AddSingleton<CommandBlocker>();
            services.AddSingleton<ExecuteCommandUseCase>();
            
            return services;
        }
    }
}
