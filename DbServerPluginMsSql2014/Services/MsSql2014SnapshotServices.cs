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
    using System.Data.SqlClient;
    using System.Data;
    using System.Linq;
    using System.Globalization;

    /// <summary>
    /// See <see cref="ISnapshotServices"/>.
    /// </summary>
    public class MsSql2014SnapshotServices : ISnapshotServices
    {
        private readonly IConnectionStringHelper _connectionStringHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSql2014SnapshotServices"/> class.
        /// </summary>
        public MsSql2014SnapshotServices()
            : this(new ConnectionStringHelper())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSql2014SnapshotServices"/> class.
        /// </summary>
        public MsSql2014SnapshotServices(IConnectionStringHelper connectionStringHelper)
        {
            ArgumentChecks.AssertNotNull(connectionStringHelper, nameof(connectionStringHelper));

            this._connectionStringHelper = connectionStringHelper;
        }
        /// <summary>
        /// See <see cref="ISnapshotServices.GetAllSnapshots(string, DbServerConnectionData)"/>.
        /// </summary>
        public IEnumerable<string> GetAllSnapshots(string database, DbServerConnectionData connection)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            var connectionString = this._connectionStringHelper.CreateConnectionString(connection);

            // TODO: Refactor this... same code as in MsSql2014DatabaseServices!
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                // Build SQL SELECT query.
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = string.Format(CultureInfo.InvariantCulture, Commands.SelectSnapshots, database);

                // Execute query.
                var adapter = new SqlDataAdapter(sqlCommand);
                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                var databaseNames = dataTable.Rows
                    .Cast<DataRow>()
                    .Select(r => r[Commands.NameColumn].ToString())
                    .ToList();

                return databaseNames;
            }
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
