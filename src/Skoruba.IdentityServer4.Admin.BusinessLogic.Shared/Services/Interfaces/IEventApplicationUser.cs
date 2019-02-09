using System;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Services.Interfaces
{
    public interface IEventApplicationUser<TKey> where TKey : IEquatable<TKey>
    {
        TKey UserSubject { get; }

        string UserName { get; }
    }
}