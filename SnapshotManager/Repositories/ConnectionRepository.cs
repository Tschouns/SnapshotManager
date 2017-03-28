//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Repositories
{
    using System;
    using Base;
    using System.Collections.Generic;
    using System.Configuration;
    using Config;

    /// <summary>
    /// See <see cref="IConnectionRepository"/>.
    /// </summary>
    public class ConnectionRepository : IConnectionRepository
    {
        private readonly IList<ConnectionInfo> _connections = new List<ConnectionInfo>();
        private readonly IConfigHelper _configHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionRepository"/> class.
        /// </summary>
        public ConnectionRepository()
            : this(new ConfigHelper())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionRepository"/> class.
        /// </summary>
        public ConnectionRepository(IConfigHelper configHelper)
        {
            ArgumentChecks.AssertNotNull(configHelper, nameof(configHelper));

            this._configHelper = configHelper;
        }

        /// <summary>
        /// See <see cref="IConnectionRepository.TryLoadConnectionsFromConfig"/>.
        /// </summary>
        public SuccessResult TryLoadConnectionsFromConfig()
        {
            IEnumerable<ConnectionInfo> configuredConnections;

            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuredConnections = this._configHelper.GetConnectionsFromConfiguration(config);
            }
            catch (SnapshotException ex)
            {
                return SuccessResult.CreateFailed(ex.Message);
            }
            catch (ConfigurationErrorsException ex)
            {
                return SuccessResult.CreateFailed(ex.Message);
            }

            foreach (var connection in configuredConnections)
            {
                this._connections.Add(connection);
            }

            return SuccessResult.CreateSuccessful();
        }

        /// <summary>
        /// See <see cref="IConnectionRepository.TrySaveConnectionsToConfig"/>.
        /// </summary>
        public SuccessResult TrySaveConnectionsToConfig()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                this._configHelper.ClearConnectionsFromConfiguration(config);
                foreach (var connection in this._connections)
                {
                    this._configHelper.AddConnectionToConfiguration(config, connection);
                }

                config.Save(ConfigurationSaveMode.Full);
            }
            catch (ConfigurationErrorsException ex)
            {
                return SuccessResult.CreateFailed(ex.Message);
            }

            return SuccessResult.CreateSuccessful();
        }

        /// <summary>
        /// See <see cref="IConnectionRepository.GetConnections"/>.
        /// </summary>
        public IEnumerable<ConnectionInfo> GetConnections()
        {
            return this._connections;
        }

        /// <summary>
        /// See <see cref="IConnectionRepository.AddConnection(ConnectionInfo)"/>.
        /// </summary>
        public void AddConnection(ConnectionInfo connection)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            this._connections.Add(connection);
        }

        /// <summary>
        /// See <see cref="IConnectionRepository.RemoveConnection(ConnectionInfo)"/>.
        /// </summary>
        public void RemoveConnection(ConnectionInfo connection)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            if (this._connections.Contains(connection))
            {
                this._connections.Remove(connection);
            }
        }
    }
}
