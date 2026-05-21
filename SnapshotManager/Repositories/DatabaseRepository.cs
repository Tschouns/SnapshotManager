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

    /// <summary>
    /// See <see cref="IDatabaseRepository"/>.
    /// </summary>
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly IDatabaseAccess _databaseServices;
        private readonly IDictionary<ConnectionInfo, IEnumerable<DatabaseInfo>> _databasesPerConnectionDict;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseRepository"/> class.
        /// </summary>
        public DatabaseRepository()
            : this(new DatabaseAccess())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseRepository"/> class.
        /// </summary>
        public DatabaseRepository(IDatabaseAccess databaseServices)
        {
            ArgumentChecks.AssertNotNull(databaseServices, nameof(databaseServices));

            this._databaseServices = databaseServices;
            this._databasesPerConnectionDict = new Dictionary<ConnectionInfo, IEnumerable<DatabaseInfo>>();
        }

        /// <summary>
        /// See <see cref="IDatabaseRepository.TryLoadDatabases(ConnectionInfo)"/>.
        /// </summary>
        public SuccessResult TryLoadDatabases(ConnectionInfo connection)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            this.ClearDatabases(connection);

            try
            {
                var databases = this._databaseServices.GetAllDatabasesForConnection(connection);
                this._databasesPerConnectionDict.Add(connection, databases);

                return SuccessResult.CreateSuccessful();
            }
            catch(SnapshotException ex)
            {
                return SuccessResult.CreateFailed($"{ex.Message} ({ex.InnerException.Message})");
            }
        }

        /// <summary>
        /// See <see cref="IDatabaseRepository.GetLoadedDatabases(ConnectionInfo)"/>.
        /// </summary>
        public IEnumerable<DatabaseInfo> GetLoadedDatabases(ConnectionInfo connection)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            if (!this._databasesPerConnectionDict.ContainsKey(connection))
            {
                return new DatabaseInfo[0];
            }

            return this._databasesPerConnectionDict[connection];
        }

        /// <summary>
        /// See <see cref="IDatabaseRepository.ClearDatabases(ConnectionInfo)"/>.
        /// </summary>
        public void ClearDatabases(ConnectionInfo connection)
        {
            ArgumentChecks.AssertNotNull(connection, nameof(connection));

            if (this._databasesPerConnectionDict.ContainsKey(connection))
            {
                this._databasesPerConnectionDict.Remove(connection);
            }
        }

        /// <summary>
        /// See <see cref="IDatabaseRepository.TryDeleteDatabase(ConnectionInfo)"/>.
        /// </summary>
        public SuccessResult TryDeleteDatabase(DatabaseInfo database)
        {
            ArgumentChecks.AssertNotNull(database, nameof(database));

            try
            {
                this._databaseServices.DeleteDatabase(database);
                // If this database and his friends from the same database were already loaded...
                if (this._databasesPerConnectionDict.ContainsKey(database.Connection))
                {
                    // ...we try to reload them.
                    return this.TryLoadDatabases(database.Connection);
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
