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
            bool usesIntegratedSecurity,
            string userId,
            string password)
        {
            ArgumentChecks.AssertNotNull(dbServer, nameof(dbServer));
            ArgumentChecks.AssertNotNull(host, nameof(host));
            ArgumentChecks.AssertNotNull(userId, nameof(userId));
            ArgumentChecks.AssertNotNull(password, nameof(password));

            this.DbServer = dbServer;
            this.Host = host;
            this.UsesIntegratedSecurity = usesIntegratedSecurity;
            this.UserId = userId;
            this.Password = password;
        }

        /// <summary>
        /// Gets the <see cref="DbServerPluginInfo"/>.
        /// </summary>
        public DbServerPluginInfo DbServer { get; }

        /// <summary>
        /// Gets the host for the connection.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Gets a value indicating whether the connection uses integrated security (in which case
        /// <see cref="UserId"/> and <see cref="Password"/> are not relevant).
        /// </summary>
        public bool UsesIntegratedSecurity { get; }

        /// <summary>
        /// Gets the user ID used for the connection.
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Gets the password used for the connection.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// See <see cref="object.ToString"/>.
        /// </summary>
        public override string ToString()
        {
            return $"{this.Host} (user: {this.UserId})";
        }
    }
}
