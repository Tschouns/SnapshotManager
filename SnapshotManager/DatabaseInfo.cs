//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager
{
    using Base;

    /// <summary>
    /// Represents a database.
    /// </summary>
    public class DatabaseInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseInfo"/> class.
        /// </summary>
        public DatabaseInfo(
            ConnectionInfo connection,
            string name)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));
            ArgumentChecks.AssertNotNull(name, nameof(name));

            this.Connection = connection;
            this.Name = name;
        }

        /// <summary>
        /// Gets the connection associated with this database.
        /// </summary>
        public ConnectionInfo Connection { get; }

        /// <summary>
        /// Gets the database name.
        /// </summary>
        public string Name { get; }
    }
}
