//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPluginMsSql2014.Services
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Data;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSql2014DatabaseServices"/> class.
        /// </summary>
        public MsSql2014DatabaseServices()
            : this(new ConnectionStringHelper())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSql2014DatabaseServices"/> class.
        /// </summary>
        public MsSql2014DatabaseServices(IConnectionStringHelper connectionStringHelper)
        {
            ArgumentChecks.AssertNotNull(connectionStringHelper, nameof(connectionStringHelper));

            this._connectionStringHelper = connectionStringHelper;
        }

        /// <summary>
        /// See <see cref="IDatabaseServices.GetAllDatabasesUsingIntegratedSecurity"/>.
        /// </summary>
        public IEnumerable<string> GetAllDatabases(DbServerConnectionData connection)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            var connectionString = this._connectionStringHelper.CreateConnectionString(connection);

            // TODO: Refactor this... same code as in MsSql2014SnapshotServices!
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                // Build SQL SELECT query.
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = Commands.SelectDatabases;

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
    }
}
