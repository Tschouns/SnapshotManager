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
        IEnumerable<string> GetAllDatabases(string host, int portNumber, string userId, string password);
    }
}
