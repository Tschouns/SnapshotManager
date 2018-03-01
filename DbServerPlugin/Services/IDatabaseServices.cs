//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPlugin.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Prvides service methods to interact with the databases.
    /// </summary>
    public interface IDatabaseServices
    {
        /// <summary>
        /// Gets all databases for the specified connection.
        /// </summary>
        IEnumerable<DatabaseData> GetAllDatabases(DbServerConnectionData connection);

        /// <summary>
        /// Delete a Database but no systemdatabase
        /// </summary>
        void DeleteDatabase(string databaseName, DbServerConnectionData connection);
    }
}
