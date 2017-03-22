//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Repositories
{
    using System.Collections.Generic;

    /// <summary>
    /// Stores all the snapshots per database.
    /// </summary>
    public interface ISnapshotRepository
    {
        /// <summary>
        /// Tries to load the snapshots for the specified database.
        /// </summary>
        SuccessResult TryLoadSnapshots(DatabaseInfo database);

        /// <summary>
        /// Gets the snapshots for the specified database.
        /// </summary>
        IEnumerable<SnapshotInfo> GetLoadedSnapshots(DatabaseInfo database);

        /// <summary>
        /// Clears the snapshots for the specified database from the repository (does not remove
        /// them on the database server).
        /// </summary>
        void ClearSnapshots(DatabaseInfo database);

        /// <summary>
        /// Clears the snapshots for the specified connection from the repository (does not remove
        /// them on the database server).
        /// </summary>
        void ClearSnapshots(ConnectionInfo connection);

        /// <summary>
        /// Tries to create a snapshot of a database on the database server.
        /// </summary>
        SuccessResult TryCreateSnapshot(string snapshotName, DatabaseInfo database);

        /// <summary>
        /// Tries to restore the database from a snapshot on the database server.
        /// </summary>
        SuccessResult TryRestoreSnapshot(SnapshotInfo snapshot);

        /// <summary>
        /// Tries to delete a snapshot on the database server.
        /// </summary>
        SuccessResult TryDeleteSnapshot(SnapshotInfo snapshot);
    }
}
