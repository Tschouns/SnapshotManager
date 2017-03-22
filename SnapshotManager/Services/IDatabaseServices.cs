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
        /// <exception cref="SnapshotException">
        /// Thrown if the databases for the specified connection could not be retrieved
        /// </exception>
        IEnumerable<DatabaseInfo> GetAllDatabasesForConnection(ConnectionInfo connection);

        /// <summary>
        /// Gets all the snapshots for the specified database.
        /// </summary>
        /// <exception cref="SnapshotException">
        /// Thrown if the snapshots for the specified database could not be retrieved
        /// </exception>
        IEnumerable<SnapshotInfo> GetAllSnapshotsForDatabase(DatabaseInfo database);

        /// <summary>
        /// Creates a snapshot for the specified database
        /// </summary>
        /// <exception cref="SnapshotException">
        /// Thrown if the snapshot could not be created
        /// </exception>
        void CreateSnapshotForDatabase(string snapshotName, DatabaseInfo database);

        /// <summary>
        /// Restores the database from the specified snapshot.
        /// </summary>
        /// <exception cref="SnapshotException">
        /// Thrown if the specified snapshot could not be restored
        /// </exception>
        void RestoreSnapshot(SnapshotInfo snapshot);

        /// <summary>
        /// Deletes the specified snapshot.
        /// </summary>
        /// <exception cref="SnapshotException">
        /// Thrown if the specified snapshot could not be deleted
        /// </exception>
        void DeleteSnapshot(SnapshotInfo snapshot);
    }
}
