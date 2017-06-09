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
        IEnumerable<string> GetAllSnapshots(string databaseName, DbServerConnectionData connection);

        /// <summary>
        /// Create a new database snapshot by the specified name.
        /// </summary>
        void CreateSnapshot(string snapshotName, string snapshotPhysicalFileLocation, string databaseName, DbServerConnectionData connection);

        /// <summary>
        /// Restores an database from an existing database snapshot.
        /// </summary>
        void RestoreSnapshot(string snapshotName, string databaseName, DbServerConnectionData connection);

        /// <summary>
        /// Deletes an existing database snapshot.
        /// </summary>
        void DeleteSnapshot(string snapshotName, DbServerConnectionData connection);
    }
}
