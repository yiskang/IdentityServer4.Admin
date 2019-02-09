using System;
using System.ComponentModel;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Services.Interfaces;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Services
{
    public class EventApplicationUser<TKey> : IEventApplicationUser<TKey> where TKey : IEquatable<TKey>
    {
        private readonly IHttpContextAccessor _accessor;

        public EventApplicationUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public TKey UserSubject => ConvertUserKeyFromString(_accessor.HttpContext.User.FindFirst(JwtClaimTypes.Subject)?.Value);

        public string UserName => _accessor.HttpContext.User.FindFirst(JwtClaimTypes.Name)?.Value;

        public virtual TKey ConvertUserKeyFromString(string subject)
        {
            if (subject == null)
            {
                return default(TKey);
            }

            return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(subject);
        }
    }
}