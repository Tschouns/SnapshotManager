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
