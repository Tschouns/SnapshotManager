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
        /// Tries to load the databases for the specified connection.
        /// </summary>
        SuccessResult TryLoadDatabases(ConnectionInfo connection);

        /// <summary>
        /// Gets the databases for the specified connection.
        /// </summary>
        IEnumerable<DatabaseInfo> GetLoadedDatabases(ConnectionInfo connection);

        /// <summary>
        /// Clears the databases for the specified connection.
        /// </summary>
        void ClearDatabases(ConnectionInfo connection);

        /// <summary>
        /// Delete the database.
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        SuccessResult DeleteDatabase(DatabaseInfo database);
    }
}
