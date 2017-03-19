//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPluginMsSql2014.Services
{
    using DbServerPlugin.Services;
    using Base;

    /// <summary>
    /// Provides services specific to a DB server plug-in.
    /// </summary>
    public class DbServerPluginServiceFacade
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbServerPluginServiceFacade"/> class.
        /// </summary>
        public DbServerPluginServiceFacade(
            IDatabaseServices databases,
            ISnapshotServices snapshots)
        {
            ArgumentChecks.AssertNotNull(databases, nameof(databases));
            ArgumentChecks.AssertNotNull(snapshots, nameof(snapshots));

            this.Databases = databases;
            this.Snapshots = snapshots;
        }

        /// <summary>
        /// Gets the <see cref="IDatabaseServices"/>.
        /// </summary>
        public IDatabaseServices Databases { get; }

        /// <summary>
        /// Gets the <see cref="ISnapshotServices"/>.
        /// </summary>
        public ISnapshotServices Snapshots { get; }
    }
}
