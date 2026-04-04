using DeviceMonitor.Application.Interfaces;
using DeviceMonitor.Application.Services;
using DeviceMonitor.Application.UseCases;
using DeviceMonitor.Infrastructure.Services;
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
            services.AddSingleton<ISshService, DeviceSimulator>();
            services.AddSingleton<DeviceService>();
            services.AddSingleton<ConnectToDeviceUseCase>();
            return services;
        }
    }
}
