//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Repositories
{
    using System.Collections.Generic;

    /// <summary>
    /// Stores all the connections.
    /// </summary>
    public interface IConnectionRepository
    {
        /// <summary>
        /// Tries to load the connections from the config file.
        /// </summary>
        SuccessResult TryLoadConnectionsFromConfig();

        /// <summary>
        /// Tries to save the connections to the config file.
        /// </summary>
        SuccessResult TrySaveConnectionsToConfig();

        /// <summary>
        /// Gets the connections.
        /// </summary>
        IEnumerable<ConnectionInfo> GetConnections();

        /// <summary>
        /// Adds a connection to the repository.
        /// </summary>
        void AddConnection(ConnectionInfo connection);

        /// <summary>
        /// Removes a connection from the repository.
        /// </summary>
        void RemoveConnection(ConnectionInfo connection);
    }
}
