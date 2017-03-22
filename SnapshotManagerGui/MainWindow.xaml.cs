//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui
{
    using System.Windows;
    using DbServerPlugin;
    using DbServerPluginMsSql2014;
    using DbServerPluginMsSql2014.Services;
    using SnapshotManager.Repositories;
    using System.Linq;
    using SnapshotManager;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConnectionRepository _connectionRepository;
        private readonly IDatabaseRepository _databaseRepository;
        private readonly ISnapshotRepository _snapshotRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            // Quick hack...
            DbServerPluginRegistry.RegisterPlugin(
                MsSql2014.Identifier,
                new MsSql2014DbServerInfo(),
                new MsSql2014DatabaseServices(),
                new MsSql2014SnapshotServices());

            this._connectionRepository = new ConnectionRepository();
            this._databaseRepository = new DatabaseRepository();
            this._snapshotRepository = new SnapshotRepository();
        }

        private void UpdateConnectionsListView()
        {
            this.connectionsListView.Items.Clear();
            foreach (var connection in this._connectionRepository.GetConnections())
            {
                this.connectionsListView.Items.Add(connection);
            }
        }

        private void UpdateDatabaseListView()
        {
            this.databasesListView.Items.Clear();

            var selectedConnection = (ConnectionInfo)this.connectionsListView.SelectedItem;
            if (selectedConnection == null)
            {
                return;
            }

            var databases = this._databaseRepository.GetLoadedDatabases(selectedConnection);
            foreach (var database in databases)
            {
                this.databasesListView.Items.Add(database);
            }
        }

        private void UpdateSnapshotListView()
        {
            this.snapshotsListView.Items.Clear();

            var selectedDatabase = (DatabaseInfo)this.databasesListView.SelectedItem;
            if (selectedDatabase == null)
            {
                return;
            }

            var loadResult = this._snapshotRepository.TryLoadSnapshots(selectedDatabase);
            if (!loadResult.Successful)
            {
                MessageBox.Show(loadResult.ErrorMessage);
            }

            var snapshots = this._snapshotRepository.GetLoadedSnapshots(selectedDatabase);
            foreach (var snapshot in snapshots)
            {
                this.snapshotsListView.Items.Add(snapshot);
            }
        }

        #region event handlers

        private void ConnectionsListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.UpdateDatabaseListView();
        }

        private void DatabasesListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.UpdateSnapshotListView();
        }

        private void AddConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            var newConnectionDialog = new NewConnectionDialog();
            var result = newConnectionDialog.Prompt(DbServerPluginRegistry.GetAllPlugins());
            if (result.HasValue)
            {
                // Add to connection repo.
                this._connectionRepository.AddConnection(result.Value);

                // Load databases.
                var loadResult = this._databaseRepository.TryLoadDatabases(result.Value);
                if (!loadResult.Successful)
                {
                    MessageBox.Show(loadResult.ErrorMessage);
                }

                this.UpdateConnectionsListView();
                this.UpdateDatabaseListView();
            }
        }

        private void RemoveConnectionButton_OnClickConnectionButton_Click(object aSender, RoutedEventArgs aE)
        {
            var selectedConnections = this.connectionsListView.SelectedItems.Cast<ConnectionInfo>().ToList();
            foreach (var connection in selectedConnections)
            {
                this._connectionRepository.RemoveConnection(connection);
            }

            this.UpdateConnectionsListView();
            this.UpdateDatabaseListView();
        }

        private void DeleteSnapshotButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSnapshots = this.snapshotsListView.SelectedItems.Cast<SnapshotInfo>().ToList();
            foreach (var snapshot in selectedSnapshots)
            {
                var deleteResult = this._snapshotRepository.TryDeleteSnapshot(snapshot);
                if (!deleteResult.Successful)
                {
                    MessageBox.Show(deleteResult.ErrorMessage);

                    // We won't atempt to delete anymore snapshots...
                    return;
                }
            }
        }

        #endregion
    }
}
