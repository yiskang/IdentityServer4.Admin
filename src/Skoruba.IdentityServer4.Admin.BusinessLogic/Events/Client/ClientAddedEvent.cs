using Skoruba.IdentityServer4.Admin.BusinessLogic.Dtos.Configuration;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Events.Infrastructure;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Events;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Events.Client
{
    public class ClientAddedEvent : Event
    {
        public ClientAddedEvent(ClientDto clientDto)
            : base(EventCategories.Client, "Client Added", EventTypes.Success, EventIds.ClientAdded)
        {
            ClientDto = clientDto;
        }

        public ClientDto ClientDto { get; set; }
    }
}
