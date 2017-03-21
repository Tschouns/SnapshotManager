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
        LoadResult TryLoadSnapshots(DatabaseInfo database);

        /// <summary>
        /// Gets the snapshots for the specified database.
        /// </summary>
        IEnumerable<SnapshotInfo> GetLoadedSnapshots(DatabaseInfo database);

        /// <summary>
        /// Clears the snapshots for the specified database.
        /// </summary>
        void ClearSnapshots(DatabaseInfo database);
    }
}
