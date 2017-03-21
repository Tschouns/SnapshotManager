//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPlugin.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Prvides service methods to interact with the database snapshots.
    /// </summary>
    public interface ISnapshotServices
    {
        /// <summary>
        /// Gets all databases for the specified database.
        /// </summary>
        IEnumerable<string> GetAllSnapshots(string database, DbServerConnectionInfo connection);

        /// <summary>
        /// Create a new database snapshot by the specified name.
        /// </summary>
        void CreateSnapshot(string snapshotName, string database, DbServerConnectionInfo connection);

        /// <summary>
        /// Restores an existing database snapshot by the specified name.
        /// </summary>
        void RestoreSnapshot(string snapshotName, string host, DbServerConnectionInfo connection);

        /// <summary>
        /// Deletes an existing database snapshot by the specified name.
        /// </summary>
        void DeleteSnapshot(string snapshotName, string host, DbServerConnectionInfo connection);
    }
}
