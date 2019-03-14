//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPluginMsSql2014.Helpers
{
    using Base;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// See <see cref="ISqlHelper"/>.
    /// </summary>
    public class SqlHelper : ISqlHelper
    {
        /// <summary>
        /// See <see cref="ISqlHelper.ExecuteQuery(string, string)"/>.
        /// </summary>
        public DataTable ExecuteQuery(string connectionString, string queryStatement)
        {
            ArgumentChecks.AssertNotNull(connectionString, nameof(connectionString));
            ArgumentChecks.AssertNotNull(queryStatement, nameof(queryStatement));

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                // Create SQL query.
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = queryStatement;

                // Execute query.
                var adapter = new SqlDataAdapter(sqlCommand);
                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                return dataTable;
            }
        }

        /// <summary>
        /// See <see cref="ISqlHelper.ExecuteNonQuery(string, string)"/>.
        /// </summary>
        public int ExecuteNonQuery(string connectionString, string nonQueryStatement)
        {
            ArgumentChecks.AssertNotNull(connectionString, nameof(connectionString));
            ArgumentChecks.AssertNotNull(nonQueryStatement, nameof(nonQueryStatement));

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                // Create SQL query.
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = nonQueryStatement;
                sqlCommand.CommandTimeout = 600;

                // Execute query.
                var rowsAffected = sqlCommand.ExecuteNonQuery();

                return rowsAffected;
            }
        }
    }
}
