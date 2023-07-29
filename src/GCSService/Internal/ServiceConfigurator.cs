using GSendShared.Abstractions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using PluginManager.Abstractions;

namespace GSendService.Internal
{
    public class ServiceConfigurator : IServiceConfigurator
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.Replace(new ServiceDescriptor(typeof(ILicenseFactory), typeof(LicenseFactory), ServiceLifetime.Singleton));
            services.Replace(new ServiceDescriptor(typeof(ILicenseFactory), typeof(LicenseFactory), ServiceLifetime.Scoped));
            services.Replace(new ServiceDescriptor(typeof(ILicenseFactory), typeof(LicenseFactory), ServiceLifetime.Transient));
        }
    }
}
