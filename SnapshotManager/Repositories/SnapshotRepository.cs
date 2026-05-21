//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Repositories
{
    using Base;
    using SnapshotManager.Access;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// See <see cref="ISnapshotRepository"/>.
    /// </summary>
    public class SnapshotRepository : ISnapshotRepository
    {
        private readonly IDatabaseAccess _databaseAccess;
        private readonly IDictionary<DatabaseInfo, IEnumerable<SnapshotInfo>> _snapshotsPerDatabaseDict;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseRepository"/> class.
        /// </summary>
        public SnapshotRepository()
            : this(new DatabaseAccess())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseRepository"/> class.
        /// </summary>
        public SnapshotRepository(IDatabaseAccess databaseAccess)
        {
            ArgumentChecks.AssertNotNull(databaseAccess, nameof(databaseAccess));

            this._databaseAccess = databaseAccess;
            this._snapshotsPerDatabaseDict = new Dictionary<DatabaseInfo, IEnumerable<SnapshotInfo>>();
        }

        /// <summary>
        /// See <see cref="ISnapshotRepository.TryLoadSnapshots(DatabaseInfo)"/>.
        /// </summary>
        public SuccessResult TryLoadSnapshots(DatabaseInfo database)
        {
            ArgumentChecks.AssertNotNull(database, nameof(database));

            this.ClearSnapshots(database);

            try
            {
                var snapshots = this._databaseAccess.GetAllSnapshotsForDatabase(database);
                this._snapshotsPerDatabaseDict.Add(database, snapshots);

                return SuccessResult.CreateSuccessful();
            }
            catch(SnapshotException ex)
            {
                return SuccessResult.CreateFailed($"{ex.Message} ({ex.InnerException.Message})");
            }
        }

        /// <summary>
        /// See <see cref="ISnapshotRepository.GetLoadedSnapshots(DatabaseInfo)"/>.
        /// </summary>
        public IEnumerable<SnapshotInfo> GetLoadedSnapshots(DatabaseInfo database)
        {
            ArgumentChecks.AssertNotNull(database, nameof(database));

            if (!this._snapshotsPerDatabaseDict.ContainsKey(database))
            {
                return new SnapshotInfo[0];
            }

            return this._snapshotsPerDatabaseDict[database];
        }

        /// <summary>
        /// See <see cref="ISnapshotRepository.ClearSnapshots(DatabaseInfo)"/>.
        /// </summary>
        public void ClearSnapshots(DatabaseInfo database)
        {
            ArgumentChecks.AssertNotNull(database, nameof(database));

            if (this._snapshotsPerDatabaseDict.ContainsKey(database))
            {
                this._snapshotsPerDatabaseDict.Remove(database);
            }
        }

        /// <summary>
        /// See <see cref="ISnapshotRepository.ClearSnapshots(ConnectionInfo)"/>.
        /// </summary>
        public void ClearSnapshots(ConnectionInfo connection)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            var databases = this._snapshotsPerDatabaseDict.Keys
                .Where(d => d.Connection == connection)
                .ToList();

            foreach (var database in databases)
            {
                this.ClearSnapshots(database);
            }
        }

        /// <summary>
        /// See <see cref="ISnapshotRepository.TryCreateSnapshot(string, DatabaseInfo)"/>.
        /// </summary>
        public SuccessResult TryCreateSnapshot(string snapshotName, DatabaseInfo database)
        {
            ArgumentChecks.AssertNotNull(snapshotName, nameof(snapshotName));
            ArgumentChecks.AssertNotNull(database, nameof(database));

            try
            {
                this._databaseAccess.CreateSnapshotForDatabase(snapshotName, database);

                return this.TryLoadSnapshots(database);
            }
            catch (SnapshotException ex)
            {
                return SuccessResult.CreateFailed($"{ex.Message} ({ex.InnerException.Message})");
            }
        }

        /// <summary>
        /// See <see cref="ISnapshotRepository.TryRestoreSnapshot(SnapshotInfo)"/>.
        /// </summary>
        public SuccessResult TryRestoreSnapshot(SnapshotInfo snapshot)
        {
            ArgumentChecks.AssertNotNull(snapshot, nameof(snapshot));

            try
            {
                this._databaseAccess.RestoreSnapshot(snapshot);

                // If this snapshot and his friends from the same database were already loaded...
                if (this._snapshotsPerDatabaseDict.ContainsKey(snapshot.Database))
                {
                    // ...we try to reload them.
                    return this.TryLoadSnapshots(snapshot.Database);
                }

                return SuccessResult.CreateSuccessful();
            }
            catch (SnapshotException ex)
            {
                return SuccessResult.CreateFailed($"{ex.Message} ({ex.InnerException.Message})");
            }
        }

        /// <summary>
        /// See <see cref="ISnapshotRepository.TryDeleteSnapshot(SnapshotInfo)"/>.
        /// </summary>
        public SuccessResult TryDeleteSnapshot(SnapshotInfo snapshot)
        {
            ArgumentChecks.AssertNotNull(snapshot, nameof(snapshot));

            try
            {
                this._databaseAccess.DeleteSnapshot(snapshot);

                // If this snapshot and his friends from the same database were already loaded...
                if (this._snapshotsPerDatabaseDict.ContainsKey(snapshot.Database))
                {
                    // ...we try to reload them.
                    return this.TryLoadSnapshots(snapshot.Database);
                }

                return SuccessResult.CreateSuccessful();
            }
            catch (SnapshotException ex)
            {
                return SuccessResult.CreateFailed($"{ex.Message} ({ex.InnerException.Message})");
            }
        }
    }
}
