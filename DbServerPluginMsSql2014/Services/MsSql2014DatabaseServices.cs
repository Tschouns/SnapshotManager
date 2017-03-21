//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPluginMsSql2014.Services
{
    using System.Collections.Generic;
    using DbServerPlugin.Services;
    using Base;
    using System.Data.SqlClient;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// See <see cref="IDatabaseServices"/>.
    /// </summary>
    public class MsSql2014DatabaseServices : IDatabaseServices
    {
        public IEnumerable<string> GetAllDatabasesUsingIntegratedSecurity(string host)
        {
            ArgumentChecks.AssertNotNull(host, nameof(host));

            // TODO: create own connection string builder....
            var connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = host;
            ////connectionStringBuilder.UserID = userId;
            ////connectionStringBuilder.Password = password;
            connectionStringBuilder.IntegratedSecurity = true;

            var connectionString = "Server=WR-SQL14\\SQL2014_02;Integrated Security=SSPI";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                // Build SQL SELECT query.
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = DatabaseServicesCommands.SelectDatabases;

                // Execute query.
                var adapter = new SqlDataAdapter(sqlCommand);
                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                var databaseNames = dataTable.Rows
                    .Cast<DataRow>()
                    .Select(r => r[DatabaseServicesCommands.NameColumn].ToString())
                    .ToList();

                return databaseNames;
            }
        }

        public IEnumerable<string> GetAllDatabases(string host, string userId, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
