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
    using System.IO;
    using System.Text;

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
        public IEnumerable<string> GetAllSnapshots(string databaseName, DbServerConnectionData connection)
        {
            ArgumentChecks.AssertNotNull(databaseName, nameof(databaseName));
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            var connectionString = this._connectionStringHelper.CreateConnectionString(connection);
            var selectSnapshotsQuery = string.Format(CultureInfo.InvariantCulture, Commands.SelectSnapshots, databaseName);
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
        public void CreateSnapshot(string snapshotName, string databaseName, DbServerConnectionData connection)
        {
            ArgumentChecks.AssertNotNull(databaseName, nameof(databaseName));
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            // Retrieve the files for the database.
            var connectionString = this._connectionStringHelper.CreateConnectionString(connection);
            var selectDatabaseFilesQuery = string.Format(CultureInfo.InvariantCulture, Commands.SelectDatabaseFiles, databaseName);
            var dataTable = this._sqlHelper.ExecuteQuery(connectionString, selectDatabaseFilesQuery);

            var logicalFileNames = dataTable.Rows
                .Cast<DataRow>()
                .Select(r => r[Commands.NameColumn].ToString())
                .ToList();

            // Determine an adequate file location for our snapshot files.
            var physicalFilePaths = dataTable.Rows
                .Cast<DataRow>()
                .Select(r => r[Commands.PhysicalNameColumn].ToString())
                .ToList();

            if (!physicalFilePaths.Any())
            {
                throw new ApplicationException($"No physical files found for {databaseName}!");
            }
            
            var fileLocation = Path.GetDirectoryName(physicalFilePaths.First());

            // Iterate through all files, and assemble file clause...
            var fileClauseBuilder = new StringBuilder();
            var fileNumber = 0;
            foreach (var logicalFileName in logicalFileNames)
            {
                var physicalSnapshotFileName = Path.Combine(fileLocation, snapshotName + "_" + fileNumber + ".ss");
                var clause = string.Format(
                    CultureInfo.InvariantCulture,
                    Commands.FileClause,
                    logicalFileName,
                    physicalSnapshotFileName);

                if (fileClauseBuilder.Length > 0)
                {
                    fileClauseBuilder.Append(", ");
                }

                fileClauseBuilder.Append(clause);

                fileNumber++;
            }

            // Asseble and execute 'create snapshot' query.
            var createSnapshotQuery = string.Format(CultureInfo.InvariantCulture, Commands.CreateSnapshot, snapshotName, fileClauseBuilder, databaseName);
            ////throw new ApplicationException(createSnapshotQuery);
            this._sqlHelper.ExecuteNonQuery(connectionString, createSnapshotQuery);
        }

        /// <summary>
        /// See <see cref="ISnapshotServices.RestoreSnapshot(string, string, DbServerConnectionData)"/>.
        /// </summary>
        public void RestoreSnapshot(string snapshotName, string databaseName, DbServerConnectionData connection)
        {
            ArgumentChecks.AssertNotNull(snapshotName, nameof(snapshotName));
            ArgumentChecks.AssertNotNull(databaseName, nameof(databaseName));
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            var connectionString = this._connectionStringHelper.CreateConnectionString(connection);
            var dropSnapshotsQuery = string.Format(CultureInfo.InvariantCulture, Commands.RestoreSnapshot, databaseName, snapshotName);
            this._sqlHelper.ExecuteNonQuery(connectionString, dropSnapshotsQuery);
        }

        /// <summary>
        /// See <see cref="ISnapshotServices.DeleteSnapshot(string, DbServerConnectionData)"/>.
        /// </summary>
        public void DeleteSnapshot(string snapshotName, DbServerConnectionData connection)
        {
            ArgumentChecks.AssertNotNull(snapshotName, nameof(snapshotName));
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            var connectionString = this._connectionStringHelper.CreateConnectionString(connection);
            var dropSnapshotsQuery = string.Format(CultureInfo.InvariantCulture, Commands.DropSnapshot, snapshotName);
            this._sqlHelper.ExecuteNonQuery(connectionString, dropSnapshotsQuery);
        }
    }
}
