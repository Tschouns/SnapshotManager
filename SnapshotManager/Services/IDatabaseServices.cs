//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides methods to interact with databases.
    /// </summary>
    public interface IDatabaseServices
    {
        /// <summary>
        /// Gets all the databases for the specified connection.
        /// </summary>
        IEnumerable<DatabaseInfo> GetAllDatabasesForConnection(ConnectionInfo connection);
    }
}
