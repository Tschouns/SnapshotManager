//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager
{
    using Base;
    using DbServerPlugin;

    /// <summary>
    /// Represents a database server connection.
    /// </summary>
    public class ConnectionInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionInfo"/> class.
        /// </summary>
        public ConnectionInfo(
            DbServerPluginInfo dbServer,
            string host,
            int portNumber,
            string userId,
            string password)
        {
            ArgumentChecks.AssertNotNull(dbServer, nameof(dbServer));
            ArgumentChecks.AssertNotNull(host, nameof(host));
            ArgumentChecks.AssertNotNull(userId, nameof(userId));
            ArgumentChecks.AssertNotNull(password, nameof(password));

            this.dbServer = dbServer;
            this.Host = host;
            this.PortNumber = portNumber;
            this.UserId = userId;
            this.Password = password;
        }

        /// <summary>
        /// Gets the <see cref="DbServerPluginInfo"/>.
        /// </summary>
        DbServerPluginInfo dbServer { get; }

        /// <summary>
        /// Gets the host for the connection.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Gets the port number for the connection.
        /// </summary>
        public int PortNumber { get; }

        /// <summary>
        /// Gets the user ID used for the connection.
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Gets the password used for the connection.
        /// </summary>
        public string Password { get; }
    }
}
