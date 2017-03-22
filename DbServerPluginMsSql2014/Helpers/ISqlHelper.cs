//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPluginMsSql2014.Helpers
{
    using System.Data;

    /// <summary>
    /// Provides a simple interface to execute SQL statements against a database.
    /// </summary>
    public interface ISqlHelper
    {
        /// <summary>
        /// Executes a query statement against the database specified by the
        /// connection string. Returns the query result.
        /// </summary>
        DataTable ExecuteQuery(string connectionString, string queryStatement);

        /// <summary>
        /// Executes a non-query statement against the database specified by the
        /// connection string. Returns the number of rows affected.
        /// </summary>
        int ExecuteNonQuery(string connectionString, string nonQueryStatement);
    }
}
