// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4
// Modified by Jan Škoruba

using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Events;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Configuration
{
    public class IdentityServerAdminEventOptions
    {
        public EventsOptions Events { get; set; } = new EventsOptions();
    }
}