//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Services
{
    using Base;
    using DbServerPlugin;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
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
                var databases = connection.DbServer.Services.Databases.GetAllDatabases(new DbServerConnectionData(
                    connection.Host,
                    connection.UsesIntegratedSecurity,
                    connection.UserId,
                    connection.Password));

                var databaseInfos = databases
                    .Select(database => new DatabaseInfo(connection, database.Name, database.PhysicalFilePaths))
                    .ToList();

                return databaseInfos;
            }
            catch(Exception ex)
            {
                var message = string.Format(CultureInfo.CurrentCulture, Messages.GetAllDatabasesForConnectionFailed, connection);

                throw new SnapshotException(message, ex);
            }
        }

        /// <summary>
        /// See <see cref="IDatabaseServices.GetAllSnapshotsForDatabase(DatabaseInfo)"/>.
        /// </summary>
        public IEnumerable<SnapshotInfo> GetAllSnapshotsForDatabase(DatabaseInfo database)
        {
            ArgumentChecks.AssertNotNull(database, nameof(database));

            try
            {
                var snapshotNames = database.Connection.DbServer.Services.Snapshots.GetAllSnapshots(
                    database.Name, 
                    new DbServerConnectionData(
                        database.Connection.Host,
                        database.Connection.UsesIntegratedSecurity,
                        database.Connection.UserId,
                        database.Connection.Password));

                var snapshotInfos = snapshotNames.Select(name => new SnapshotInfo(database, name)).ToList();

                return snapshotInfos;
            }
            catch (Exception ex)
            {
                var message = string.Format(CultureInfo.CurrentCulture, Messages.GetAllSnapshotsForDatabaseFailed, database);

                throw new SnapshotException(message, ex);
            }
        }

        /// <summary>
        /// See <see cref="IDatabaseServices.CreateSnapshotForDatabase"/>.
        /// </summary>
        public void CreateSnapshotForDatabase(string snapshotName, DatabaseInfo database)
        {
            ArgumentChecks.AssertNotNull(snapshotName, nameof(snapshotName));
            ArgumentChecks.AssertNotNull(database, nameof(database));

            try
            {
                // TODO: pass this in as argument.
                var snapshotPhysicalFileLocation = Path.GetDirectoryName(database.PhysicalFilePaths.First());

                database.Connection.DbServer.Services.Snapshots.CreateSnapshot(
                    snapshotName,
                    snapshotPhysicalFileLocation,
                    database.Name,
                    new DbServerConnectionData(
                        database.Connection.Host,
                        database.Connection.UsesIntegratedSecurity,
                        database.Connection.UserId,
                        database.Connection.Password));
            }
            catch (Exception ex)
            {
                var message = string.Format(CultureInfo.CurrentCulture, Messages.CreateSnapshotFailed, database);

                throw new SnapshotException(message, ex);
            }
        }

        /// <summary>
        /// See <see cref="IDatabaseServices.RestoreSnapshot(SnapshotInfo)"/>.
        /// </summary>
        public void RestoreSnapshot(SnapshotInfo snapshot)
        {
            ArgumentChecks.AssertNotNull(snapshot, nameof(snapshot));

            try
            {
                snapshot.Database.Connection.DbServer.Services.Snapshots.RestoreSnapshot(
                    snapshot.Name,
                    snapshot.Database.Name,
                    new DbServerConnectionData(
                        snapshot.Database.Connection.Host,
                        snapshot.Database.Connection.UsesIntegratedSecurity,
                        snapshot.Database.Connection.UserId,
                        snapshot.Database.Connection.Password));
            }
            catch (Exception ex)
            {
                var message = string.Format(CultureInfo.CurrentCulture, Messages.RestoreSnapshotFailed, snapshot);

                throw new SnapshotException(message, ex);
            }
        }

        /// <summary>
        /// See <see cref="IDatabaseServices.DeleteSnapshot(SnapshotInfo)"/>.
        /// </summary>
        public void DeleteSnapshot(SnapshotInfo snapshot)
        {
            ArgumentChecks.AssertNotNull(snapshot, nameof(snapshot));

            try
            {
                snapshot.Database.Connection.DbServer.Services.Snapshots.DeleteSnapshot(
                    snapshot.Name,
                    new DbServerConnectionData(
                        snapshot.Database.Connection.Host,
                        snapshot.Database.Connection.UsesIntegratedSecurity,
                        snapshot.Database.Connection.UserId,
                        snapshot.Database.Connection.Password));
            }
            catch (Exception ex)
            {
                var message = string.Format(CultureInfo.CurrentCulture, Messages.DeleteSnapshotFailed, snapshot);

                throw new SnapshotException(message, ex);
            }
        }

        /// <summary>
        /// See <see cref="IDatabaseServices.DeleteSnapshot(SnapshotInfo)"/>.
        /// </summary>
        public void DeleteDatabase(DatabaseInfo database)
        {
            ArgumentChecks.AssertNotNull(database, nameof(database));

            try
            {
                database.Connection.DbServer.Services.Databases.DeleteDatabase(
                    database.Name,
                    new DbServerConnectionData(
                        database.Connection.Host,
                        database.Connection.UsesIntegratedSecurity,
                        database.Connection.UserId,
                        database.Connection.Password));
            }
            catch (Exception ex)
            {
                var message = string.Format(CultureInfo.CurrentCulture, Messages.DeleteSnapshotFailed, database);
                throw new SnapshotException(message, ex);
            }
        }
    }
}
