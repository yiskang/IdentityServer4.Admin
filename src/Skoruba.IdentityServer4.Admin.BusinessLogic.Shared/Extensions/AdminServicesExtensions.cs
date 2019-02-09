using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Configuration;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Services;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Services.Interfaces;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Extensions
{
    public static class AdminServicesExtensions
    {
        public static IServiceCollection AddAdminEventServices<TUserKey>(this IServiceCollection services,
            Action<IdentityServerAdminEventOptions> setupEvents)
            where TUserKey : IEquatable<TUserKey>
        {
            services.Configure(setupEvents);

            services.AddOptions();
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<IdentityServerAdminEventOptions>>().Value);

            services.AddTransient<IEventService, PersistedEventService>();
            services.AddTransient<IEventSink, PersistedEventSink<TUserKey>>();
            services.AddTransient<IEventApplicationUser<TUserKey>, EventApplicationUser<TUserKey>>();

            return services;
        }
    }
}
