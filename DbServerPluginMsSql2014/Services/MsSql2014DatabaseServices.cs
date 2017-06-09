//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPluginMsSql2014.Services
{
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using DbServerPlugin.Services;
    using Base;
    using DbServerPluginMsSql2014.Helpers;
    using DbServerPlugin;

    /// <summary>
    /// See <see cref="IDatabaseServices"/>.
    /// </summary>
    public class MsSql2014DatabaseServices : IDatabaseServices
    {
        private readonly IConnectionStringHelper _connectionStringHelper;
        private readonly ISqlHelper _sqlHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSql2014DatabaseServices"/> class.
        /// </summary>
        public MsSql2014DatabaseServices()
            : this(
                  new ConnectionStringHelper(),
                  new SqlHelper())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSql2014DatabaseServices"/> class.
        /// </summary>
        public MsSql2014DatabaseServices(
            IConnectionStringHelper connectionStringHelper,
            ISqlHelper sqlHelper)
        {
            ArgumentChecks.AssertNotNull(connectionStringHelper, nameof(connectionStringHelper));
            ArgumentChecks.AssertNotNull(sqlHelper, nameof(sqlHelper));

            this._connectionStringHelper = connectionStringHelper;
            this._sqlHelper = sqlHelper;
        }

        /// <summary>
        /// See <see cref="IDatabaseServices.GetAllDatabasesUsingIntegratedSecurity"/>.
        /// </summary>
        public IEnumerable<DatabaseData> GetAllDatabases(DbServerConnectionData connection)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            var connectionString = this._connectionStringHelper.CreateConnectionString(connection);
            var dataTable = this._sqlHelper.ExecuteQuery(connectionString, Commands.SelectDatabases);
            var databaseNames = dataTable.Rows
                .Cast<DataRow>()
                .Select(r => r[Commands.NameColumn].ToString())
                .ToList();

            IList<DatabaseData> databaseDatas = new List<DatabaseData>();
            foreach (var databaseName in databaseNames)
            {
                var physicalFilePaths = this.GetPhysicalFilePaths(connection, databaseName);

                databaseDatas.Add(new DatabaseData(databaseName, physicalFilePaths));
            }

            return databaseDatas;
        }

        private IEnumerable<string> GetPhysicalFilePaths(DbServerConnectionData connection, string databaseName)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));
            ArgumentChecks.AssertNotNullOrEmpty(databaseName, nameof(databaseName));

            var connectionString = this._connectionStringHelper.CreateConnectionString(connection);
            var selectDatabaseFilesQuery = string.Format(CultureInfo.InvariantCulture, Commands.SelectDatabaseFiles, databaseName);
            var dataTable = this._sqlHelper.ExecuteQuery(connectionString, selectDatabaseFilesQuery);

            var physicalFilePaths = dataTable.Rows
                .Cast<DataRow>()
                .Select(r => r[Commands.PhysicalNameColumn].ToString())
                .ToList();

            return physicalFilePaths;
        }
    }
}
