//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager
{
    using Base;

    /// <summary>
    /// Represents a database snapshot.
    /// </summary>
    public class SnapshotInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotInfo"/> class.
        /// </summary>
        public SnapshotInfo(
            DatabaseInfo database,
            string name)
        {
            ArgumentChecks.AssertNotNull(database, nameof(database));
            ArgumentChecks.AssertNotNull(name, nameof(name));

            this.Database = database;
            this.Name = name;
        }

        /// <summary>
        /// Gets the database which this snapshot belongs to.
        /// </summary>
        DatabaseInfo Database { get; }

        /// <summary>
        /// Gets the snapshot name.
        /// </summary>
        public string Name { get; }
    }
}
