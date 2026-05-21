namespace SnapshotManager.Services
{
    using SnapshotManager.Repositories;

    public interface IDeleteDatabaseService
    {
        /// <summary>
        /// Deletes a database, including its snapshots.
        /// </summary>
        SuccessResult TryDeleteDatabaseIncludingSnapshots(DatabaseInfo database);
    }
}
