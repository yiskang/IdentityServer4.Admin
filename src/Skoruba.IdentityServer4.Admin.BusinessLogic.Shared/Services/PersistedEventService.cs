// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4
// Modified by Jan Škoruba

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Configuration;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Events;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Services.Interfaces;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Services
{
    /// <summary>
    /// The default event service
    /// </summary>
    /// <seealso cref="IEventService" />
    public class PersistedEventService : IEventService
    {        
        protected readonly IdentityServerAdminEventOptions Options;

        /// <summary>
        /// The context
        /// </summary>
        protected readonly IHttpContextAccessor Context;

        /// <summary>
        /// The sink
        /// </summary>
        protected readonly IEventSink Sink;

        /// <summary>
        /// The clock
        /// </summary>
        protected readonly ISystemClock Clock;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistedEventService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <param name="sink">The sink.</param>
        /// <param name="clock">The clock.</param>
        public PersistedEventService(IdentityServerAdminEventOptions options, IHttpContextAccessor context, IEventSink sink, ISystemClock clock)
        {
            Options = options;
            Context = context;
            Sink = sink;
            Clock = clock;
        }

        /// <summary>
        /// Raises the specified event.
        /// </summary>
        /// <param name="evt">The event.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">evt</exception>
        public async Task RaiseAsync(Event evt)
        {
            if (evt == null) throw new ArgumentNullException(nameof(evt));

            if (CanRaiseEvent(evt))
            {
                await PrepareEventAsync(evt);
                await Sink.PersistAsync(evt);
            }
        }

        /// <summary>
        /// Indicates if the type of event will be persisted.
        /// </summary>
        /// <param name="evtType"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public bool CanRaiseEventType(EventTypes evtType)
        {
            switch (evtType)
            {
                case EventTypes.Failure:
                    return Options.Events.RaiseFailureEvents;
                case EventTypes.Information:
                    return Options.Events.RaiseInformationEvents;
                case EventTypes.Success:
                    return Options.Events.RaiseSuccessEvents;
                case EventTypes.Error:
                    return Options.Events.RaiseErrorEvents;
                default:
                    throw new ArgumentOutOfRangeException(nameof(evtType));
            }
        }

        /// <summary>
        /// Determines whether this event would be persisted.
        /// </summary>
        /// <param name="evt">The evt.</param>
        /// <returns>
        ///   <c>true</c> if this event would be persisted; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool CanRaiseEvent(Event evt)
        {
            return CanRaiseEventType(evt.EventType);
        }

        /// <summary>
        /// Prepares the event.
        /// </summary>
        /// <param name="evt">The evt.</param>
        /// <returns></returns>
        protected virtual async Task PrepareEventAsync(Event evt)
        {
            evt.ActivityId = Context.HttpContext.TraceIdentifier;
            evt.TimeStamp = Clock.UtcNow.UtcDateTime;
            evt.ProcessId = Process.GetCurrentProcess().Id;

            const string unknown = "unknown";

            evt.LocalIpAddress = Context.HttpContext.Connection.LocalIpAddress != null ? $"{Context.HttpContext.Connection.LocalIpAddress}:{Context.HttpContext.Connection.LocalPort}" : unknown;
            evt.RemoteIpAddress = Context.HttpContext.Connection.RemoteIpAddress != null ? Context.HttpContext.Connection.RemoteIpAddress.ToString() : unknown;

            await evt.PrepareAsync();
        }
    }
}