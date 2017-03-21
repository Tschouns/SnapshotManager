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
        /// See <see cref="ISnapshotServices.GetAllSnapshots(string, DbServerConnectionInfo)"/>.
        /// </summary>
        public IEnumerable<string> GetAllSnapshots(string database, DbServerConnectionInfo connection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="ISnapshotServices.CreateSnapshot(string, string, DbServerConnectionInfo)"/>.
        /// </summary>
        public void CreateSnapshot(string snapshotName, string database, DbServerConnectionInfo connection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="ISnapshotServices.RestoreSnapshot(string, string, DbServerConnectionInfo)"/>.
        /// </summary>
        public void RestoreSnapshot(string snapshotName, string host, DbServerConnectionInfo connection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="ISnapshotServices.DeleteSnapshot(string, string, DbServerConnectionInfo)"/>.
        /// </summary>
        public void DeleteSnapshot(string snapshotName, string host, DbServerConnectionInfo connection)
        {
            throw new NotImplementedException();
        }
    }
}
