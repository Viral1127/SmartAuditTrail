using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartAuditTrail.Interceptors;
using SmartAuditTrail.Options;
using SmartAuditTrail.Providers;

namespace SmartAuditTrail.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSmartAudit(
            this IServiceCollection services,
            Action<SmartAuditOptions>? configure = null)
        {
            var options = new SmartAuditOptions();
            configure?.Invoke(options);

            services.AddSingleton(options);

            services.AddScoped<SmartAuditInterceptor>();
            services.AddScoped<IUserProvider, DefaultUserProvider>();

            return services;
        }
    }
}
