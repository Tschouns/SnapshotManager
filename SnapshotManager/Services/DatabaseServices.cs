//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Services
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// See <see cref="IDatabaseServices"/>.
    /// </summary>
    public class DatabaseServices : IDatabaseServices
    {
        /// <summary>
        /// See <see cref="IDatabaseServices.GetAllDatabasesForConnection(ConnectionInfo)"/>.
        /// </summary>
        public IEnumerable<DatabaseInfo> GetAllDatabasesForConnection(ConnectionInfo connection)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            try
            {
                IEnumerable<string> databaseNames = new string[0];

                if (connection.UsesIntegratedSecurity)
                {
                    databaseNames = connection.DbServer.Services.Databases.GetAllDatabasesUsingIntegratedSecurity(connection.Host);
                }
                else
                {
                    databaseNames = connection.DbServer.Services.Databases.GetAllDatabases(connection.Host, connection.UserId, connection.Password);
                }

                var databaseInfos = databaseNames.Select(name => new DatabaseInfo(connection, name)).ToList();

                return databaseInfos;
            }
            catch(Exception ex)
            {
                var message = string.Format(CultureInfo.CurrentCulture, Messages.GetAllDatabasesForConnectionFailed, connection);

                throw new SnapshotException(message, ex);
            }
        }
    }
}
