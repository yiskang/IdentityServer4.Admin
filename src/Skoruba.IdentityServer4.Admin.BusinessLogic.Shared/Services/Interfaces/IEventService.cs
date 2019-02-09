// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4
// Modified by Jan Škoruba

using System.Threading.Tasks;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Events;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Services.Interfaces
{
    /// <summary>
    /// Interface for the event service
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Raises the specified event.
        /// </summary>
        /// <param name="evt">The event.</param>
        Task RaiseAsync(Event evt);

        /// <summary>
        /// Indicates if the type of event will be persisted.
        /// </summary>
        bool CanRaiseEventType(EventTypes evtType);
    }
}