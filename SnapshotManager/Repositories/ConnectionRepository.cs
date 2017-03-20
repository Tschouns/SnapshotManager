//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Repositories
{
    using Base;
    using System.Collections.Generic;

    /// <summary>
    /// See <see cref="IConnectionRepository"/>.
    /// </summary>
    public class ConnectionRepository : IConnectionRepository
    {
        private IList<ConnectionInfo> _connections = new List<ConnectionInfo>();

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
    }
}
