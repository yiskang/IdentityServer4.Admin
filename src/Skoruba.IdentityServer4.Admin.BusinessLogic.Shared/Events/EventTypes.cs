// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4
// Modified by Jan Škoruba

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Events
{
    /// <summary>
    /// Indicates if the event is a success or fail event.
    /// </summary>
    public enum EventTypes
    {
        /// <summary>
        /// Success event
        /// </summary>
        Success = 1,

        /// <summary>
        /// Failure event
        /// </summary>
        Failure = 2,

        /// <summary>
        /// Information event
        /// </summary>
        Information = 3,

        /// <summary>
        /// Error event
        /// </summary>
        Error = 4
    }
}