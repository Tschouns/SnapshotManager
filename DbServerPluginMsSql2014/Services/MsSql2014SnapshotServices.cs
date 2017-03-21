//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPluginMsSql2014.Services
{
    using System;
    using System.Collections.Generic;
    using DbServerPlugin;
    using DbServerPlugin.Services;

    /// <summary>
    /// See <see cref="ISnapshotServices"/>.
    /// </summary>
    public class MsSql2014SnapshotServices : ISnapshotServices
    {
        /// <summary>
        /// See <see cref="ISnapshotServices.GetAllSnapshots(string, DbServerConnectionData)"/>.
        /// </summary>
        public IEnumerable<string> GetAllSnapshots(string database, DbServerConnectionData connection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="ISnapshotServices.CreateSnapshot(string, string, DbServerConnectionData)"/>.
        /// </summary>
        public void CreateSnapshot(string snapshotName, string database, DbServerConnectionData connection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="ISnapshotServices.RestoreSnapshot(string, string, DbServerConnectionData)"/>.
        /// </summary>
        public void RestoreSnapshot(string snapshotName, string host, DbServerConnectionData connection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="ISnapshotServices.DeleteSnapshot(string, string, DbServerConnectionData)"/>.
        /// </summary>
        public void DeleteSnapshot(string snapshotName, string host, DbServerConnectionData connection)
        {
            throw new NotImplementedException();
        }
    }
}
