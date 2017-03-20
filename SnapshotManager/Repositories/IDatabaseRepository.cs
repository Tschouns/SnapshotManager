//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Repositories
{
    using System.Collections.Generic;

    /// <summary>
    /// Stores all the databases per connection.
    /// </summary>
    public interface IDatabaseRepository
    {
        /// <summary>
        /// Gets the connections.
        /// </summary>
        IEnumerable<DatabaseInfo> GetDatabasesForConnection(ConnectionInfo connection);
    }
}
