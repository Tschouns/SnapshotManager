//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Repositories
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// See <see cref="ISnapshotRepository"/>.
    /// </summary>
    public class SnapshotRepository : ISnapshotRepository
    {
        /// <summary>
        /// See <see cref="ISnapshotRepository.TryLoadSnapshots(DatabaseInfo)"/>.
        /// </summary>
        public LoadResult TryLoadSnapshots(DatabaseInfo database)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="ISnapshotRepository.GetLoadedSnapshots(DatabaseInfo)"/>.
        /// </summary>
        public IEnumerable<DatabaseInfo> GetLoadedSnapshots(DatabaseInfo database)
        {
            throw new NotImplementedException();
        }
    }
}
