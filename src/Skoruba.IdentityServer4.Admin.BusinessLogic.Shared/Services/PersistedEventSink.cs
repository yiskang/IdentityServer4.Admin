// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4
// Modified by Jan Škoruba

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Events;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Services.Interfaces;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Services
{
    /// <summary>
    /// Default implementation of the event service. Write events raised to the log.
    /// </summary>
    public class PersistedEventSink<TUserKey> : IEventSink where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The current user
        /// </summary>
        private readonly IEventApplicationUser<TUserKey> _user;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistedEventSink{TUserKey}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="user">The current user.</param>
        public PersistedEventSink(ILogger<PersistedEventService> logger, IEventApplicationUser<TUserKey> user)
        {
            _logger = logger;
            _user = user;
        }

        /// <summary>
        /// Raises the specified event.
        /// </summary>
        /// <param name="evt">The event.</param>
        /// <exception cref="T:System.ArgumentNullException">evt</exception>
        public virtual Task PersistAsync(Event evt)
        {
            if (evt == null) throw new ArgumentNullException(nameof(evt));

            var persistedEvent = new PersistedEvent<TUserKey>
            {
                UserName = _user.UserName,
                UserSubject = _user.UserSubject,
                Data = evt.ToString()
            };
            
            _logger.LogInformation("{@event}", persistedEvent);

            return Task.CompletedTask;
        }
    }
}