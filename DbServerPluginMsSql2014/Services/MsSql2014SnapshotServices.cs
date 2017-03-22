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
    using Base;
    using Helpers;
    using System.Data;
    using System.Linq;
    using System.Globalization;

    /// <summary>
    /// See <see cref="ISnapshotServices"/>.
    /// </summary>
    public class MsSql2014SnapshotServices : ISnapshotServices
    {
        private readonly IConnectionStringHelper _connectionStringHelper;
        private readonly ISqlHelper _sqlHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSql2014SnapshotServices"/> class.
        /// </summary>
        public MsSql2014SnapshotServices()
            : this(
                  new ConnectionStringHelper(),
                  new SqlHelper())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSql2014SnapshotServices"/> class.
        /// </summary>
        public MsSql2014SnapshotServices(
            IConnectionStringHelper connectionStringHelper,
            ISqlHelper sqlHelper)
        {
            ArgumentChecks.AssertNotNull(connectionStringHelper, nameof(connectionStringHelper));
            ArgumentChecks.AssertNotNull(sqlHelper, nameof(sqlHelper));

            this._connectionStringHelper = connectionStringHelper;
            this._sqlHelper = sqlHelper;
        }
        /// <summary>
        /// See <see cref="ISnapshotServices.GetAllSnapshots(string, DbServerConnectionData)"/>.
        /// </summary>
        public IEnumerable<string> GetAllSnapshots(string database, DbServerConnectionData connection)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            var connectionString = this._connectionStringHelper.CreateConnectionString(connection);
            var selectSnapshotsQuery = string.Format(CultureInfo.InvariantCulture, Commands.SelectSnapshots, database);
            var dataTable = this._sqlHelper.ExecuteQuery(connectionString, selectSnapshotsQuery);
            var snapshotNames = dataTable.Rows
                .Cast<DataRow>()
                .Select(r => r[Commands.NameColumn].ToString())
                .ToList();

            return snapshotNames;
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
