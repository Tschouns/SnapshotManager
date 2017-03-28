//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Config
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using Base;
    using DbServerPlugin;

    /// <summary>
    /// See <see cref="IConfigHelper"/> .
    /// </summary>
    public class ConfigHelper : IConfigHelper
    {
        private const string ConnectionDataSectionName = "ConnectionData";

        /// <summary>
        /// See <see cref="IConfigHelper.GetConnectionsFromConfiguration"/> .
        /// </summary>
        public IEnumerable<ConnectionInfo> GetConnectionsFromConfiguration(Configuration config)
        {
            ArgumentChecks.AssertNotNull(config, nameof(config));

            var section = GetOrCreateConnectionDataSection(config);
            var elements = section.ConnectionCollection
                .Cast<ConnectionConfigurationElement>()
                .ToList();

            var connections = elements
                .Select(ConvertToConnectionInfo)
                .ToList();

            return connections;
        }

        /// <summary>
        /// See <see cref="IConfigHelper.AddConnectionToConfiguration"/> .
        /// </summary>
        public void AddConnectionToConfiguration(Configuration config, ConnectionInfo connection)
        {
            ArgumentChecks.AssertNotNull(config, nameof(config));
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            var element = ConvertToConfigElement(connection);
            var section = GetOrCreateConnectionDataSection(config);
            if (!section.ConnectionCollection.Contains(element))
            {
                section.ConnectionCollection.Add(element);
            }
        }

        /// <summary>
        /// See <see cref="IConfigHelper.RemoveConnectionFromConfiguration"/> .
        /// </summary>
        public void RemoveConnectionFromConfiguration(Configuration config, ConnectionInfo connection)
        {
            ArgumentChecks.AssertNotNull(config, nameof(config));
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            var element = ConvertToConfigElement(connection);
            var section = GetOrCreateConnectionDataSection(config);
            if (section.ConnectionCollection.Contains(element))
            {
                section.ConnectionCollection.Remove(element);
            }
        }

        private static ConnectionDataConfigurationSection GetOrCreateConnectionDataSection(Configuration config)
        {
            ArgumentChecks.AssertNotNull(config, nameof(config));

            if (config.Sections[ConnectionDataSectionName] == null)
            {
                var section = new ConnectionDataConfigurationSection();
                config.Sections.Add(ConnectionDataSectionName, section);
            }

            return (ConnectionDataConfigurationSection)config.Sections[ConnectionDataSectionName];
        }

        private static ConnectionInfo ConvertToConnectionInfo(ConnectionConfigurationElement configurationElement)
        {
            ArgumentChecks.AssertNotNull(configurationElement, nameof(configurationElement));

            var pluginIdentifier = configurationElement.DbServerPluginInfo;
            var pluginResult = DbServerPluginRegistry.GetPluginByIdentifier(pluginIdentifier);
            if (!pluginResult.HasValue)
            {
                var message = string.Format(
                    CultureInfo.CurrentCulture,
                    Messages.PluginNotFound,
                    pluginIdentifier);

                throw new SnapshotException(message);
            }

            var connection = new ConnectionInfo(
                pluginResult.Value,
                configurationElement.Host,
                configurationElement.UsesIntegratedSecurity,
                configurationElement.UserId,
                configurationElement.Password);

            return connection;
        }

        private static ConnectionConfigurationElement ConvertToConfigElement(ConnectionInfo connection)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            var element = new ConnectionConfigurationElement
            {
                DbServerPluginInfo = connection.DbServer.PluginIdentifier,
                Host = connection.Host,
                UsesIntegratedSecurity = connection.UsesIntegratedSecurity,
                UserId = connection.UserId,
                Password = connection.Password
            };

            return element;
        }
    }
}
