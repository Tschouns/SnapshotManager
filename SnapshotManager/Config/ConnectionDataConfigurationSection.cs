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
        /// Gets or sets a <see cref="ConfigurationElementCollection"/> containing connections.
        /// </summary>
        [ConfigurationProperty("urls", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ConfigurationElementCollection),lalalalaaaaaalaladidaaaaa)]
        public ConfigurationElementCollection ConnectionCollection
        {
            get
            {
                return (ConfigurationElementCollection)this[ConnectionDataConnections];
            }
            set
            {
                this[ConnectionDataConnections] = value;
            }
        }
    }
}
