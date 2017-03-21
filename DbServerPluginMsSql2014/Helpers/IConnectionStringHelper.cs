//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPluginMsSql2014.Helpers
{
    using DbServerPlugin;

    /// <summary>
    /// Creates SQL connection strings for different connections.
    /// </summary>
    public interface IConnectionStringHelper
    {
        /// <summary>
        /// Creates an SQL connection string to connect to a specified host, using integrated security.
        /// </summary>
        string CreateConnectionStringIntegratedSecurity(string host);

        /// <summary>
        /// Creates an SQL connection string to connect to a specified host, also specifying user ID
        /// and password.
        /// </summary>
        string CreateConnectionString(string host, string userId, string password);

        /// <summary>
        /// Creates an SQL connection string based on the specified <see cref="DbServerConnectionInfo"/>.
        /// </summary>
        string CreateConnectionString(DbServerConnectionInfo connectionInfo);
    }
}
