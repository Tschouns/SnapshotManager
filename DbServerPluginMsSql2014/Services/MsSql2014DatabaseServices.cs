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
        /// <summary>
        /// See <see cref="IDatabaseServices.GetAllDatabases(string, int, string, string)"/>.
        /// </summary>
        public IEnumerable<string> GetAllDatabases(string host, int portNumber, string userId, string password)
        {
            ArgumentChecks.AssertNotNull(host, nameof(host));
            ArgumentChecks.AssertNotNull(userId, nameof(userId));
            ArgumentChecks.AssertNotNull(password, nameof(password));

            var connectionString = new SqlConnectionStringBuilder();
            connectionString.DataSource = host;
            connectionString.UserID = userId;
            connectionString.Password = password;

            using (var sqlConnection = new SqlConnection(connectionString.ToString()))
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
    }
}
