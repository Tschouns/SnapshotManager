namespace SnapshotManager.Services
{
    using System.Linq;
    using SnapshotManager.Repositories;

    public class DeleteDatabaseService : IDeleteDatabaseService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly ISnapshotRepository _snapshotRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDatabaseService"/> class.
        /// </summary>
        public DeleteDatabaseService(
            IDatabaseRepository databaseRepository,
            ISnapshotRepository snapshotRepository)
        {
            _databaseRepository = databaseRepository;
            _snapshotRepository = snapshotRepository;
        }

        /// <summary>
        /// See <see cref="IDeleteDatabaseService.TryDeleteDatabaseIncludingSnapshots(DatabaseInfo)"/>.
        /// </summary>
        public SuccessResult TryDeleteDatabaseIncludingSnapshots(DatabaseInfo database)
        {
            // Reload the snapshots.
            var lSuccessResult = _snapshotRepository.TryLoadSnapshots(database);

            if (!lSuccessResult.Successful)
            {
                return lSuccessResult;
            }

            // Delete the snapshots.
            var snapshots = _snapshotRepository.GetLoadedSnapshots(database).ToList();
            foreach (var snapshot in snapshots)
            {
                lSuccessResult = _snapshotRepository.TryDeleteSnapshot(snapshot);

                if (!lSuccessResult.Successful)
                {
                    return lSuccessResult;
                }
            }

            // Delete the database.
            lSuccessResult = _databaseRepository.TryDeleteDatabase(database);

            if (!lSuccessResult.Successful)
            {
                return lSuccessResult;
            }

            return lSuccessResult;
        }
    }
}
