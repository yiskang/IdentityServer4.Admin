using System;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.Events
{
    public class PersistedEvent<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets or sets the data of the current request.
        /// </summary>
        /// <value>
        /// The data in json format.
        /// </value>
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the user name of the current request.
        /// </summary>
        /// <value>
        /// The current user name
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user subject of the current request.
        /// </summary>
        /// <value>
        /// The current user subject
        /// </value>
        public TKey UserSubject { get; set; }
    }
}
