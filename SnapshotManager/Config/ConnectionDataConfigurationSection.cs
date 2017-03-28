//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Config
{
    using System.Configuration;

    /// <summary>
    /// Configuration section containing connection data.
    /// </summary>
    public class ConnectionDataConfigurationSection : ConfigurationSection
    {
        private const string ConnectionDataConnections = "Connections";

        /// <summary>
        /// Gets or sets a <see cref="ConnectionConfigurationElementCollection"/> containing connections.
        /// </summary>
        [ConfigurationProperty(ConnectionDataConnections, IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ConnectionConfigurationElementCollection))]
        public ConnectionConfigurationElementCollection ConnectionCollection
        {
            get
            {
                return (ConnectionConfigurationElementCollection)this[ConnectionDataConnections];
            }
            set
            {
                this[ConnectionDataConnections] = value;
            }
        }
    }
}
