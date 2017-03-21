//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPluginMsSql2014.Helpers
{
    using System.Globalization;
    using Base;
    using DbServerPlugin;

    /// <summary>
    /// See <see cref="IConnectionStringHelper"/>.
    /// </summary>
    public class ConnectionStringHelper : IConnectionStringHelper
    {
        /// <summary>
        /// See <see cref="IConnectionStringHelper.CreateConnectionStringIntegratedSecurity"/>.
        /// </summary>
        public string CreateConnectionStringIntegratedSecurity(string host)
        {
            ArgumentChecks.AssertNotNull(host, nameof(host));

            var connectionString = string.Format(
                CultureInfo.InvariantCulture,
                ConnectionStringTemplates.HostUsingIntegratedSecurity,
                host);

            return connectionString;
        }

        /// <summary>
        /// See <see cref="IConnectionStringHelper.CreateConnectionString(string, string, string)"/>.
        /// </summary>
        public string CreateConnectionString(string host, string userId, string password)
        {
            ArgumentChecks.AssertNotNull(host, nameof(host));
            ArgumentChecks.AssertNotNull(userId, nameof(userId));
            ArgumentChecks.AssertNotNull(password, nameof(password));

            var connectionString = string.Format(
                CultureInfo.InvariantCulture,
                ConnectionStringTemplates.HostUserPassword,
                host,
                userId,
                password);

            return connectionString;
        }

        /// <summary>
        /// See <see cref="IConnectionStringHelper.CreateConnectionString(DbServerConnectionData)"/>.
        /// </summary>
        public string CreateConnectionString(DbServerConnectionData connectionData)
        {
            ArgumentChecks.AssertNotNull(connectionData, nameof(connectionData));

            if (connectionData.UsesIntegratedSecurity)
            {
                return this.CreateConnectionStringIntegratedSecurity(connectionData.Host);
            }

            return this.CreateConnectionString(connectionData.Host, connectionData.UserId, connectionData.Password);
        }
    }
}
