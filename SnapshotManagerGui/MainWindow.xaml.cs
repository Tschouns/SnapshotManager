//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using Base;
    using DbServerPlugin;
    using DbServerPluginMsSql2014;
    using DbServerPluginMsSql2014.Services;
    using SnapshotManager.Repositories;
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

            this.UpdateButtonStatus();
        }

        private static void HandleResult(SuccessResult result)
        {
            ArgumentChecks.AssertNotNull(result, nameof(result));

            if (!result.Successful)
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }

        private void UpdateButtonStatus()
        {
            var isConnectionSelected = this.connectionsListView.SelectedItem != null;
            var isDatabaseSelected = this.databasesListView.SelectedItem != null;
            var isSnapshotSelected = this.snapshotsListView.SelectedItem != null;

            this.removeConnectionButton.IsEnabled = isConnectionSelected;
            this.refreshDatabasesButton.IsEnabled = isConnectionSelected;
            this.refreshSnapshotsButton.IsEnabled = isDatabaseSelected;
            this.createSnapshotButton.IsEnabled = isDatabaseSelected;
            this.restoreSnapshotButton.IsEnabled = isSnapshotSelected;
            this.deleteSnapshotButton.IsEnabled = isSnapshotSelected;
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

            HandleResult(this._snapshotRepository.TryLoadSnapshots(selectedDatabase));
            var snapshots = this._snapshotRepository.GetLoadedSnapshots(selectedDatabase);
            foreach (var snapshot in snapshots)
            {
                this.snapshotsListView.Items.Add(snapshot);
            }
        }

        #region event handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load connections.
            HandleResult(this._connectionRepository.TryLoadConnectionsFromConfig());

            // Load databases foreach connection.
            var connections = this._connectionRepository.GetConnections();
            foreach (var connection in connections)
            {
                HandleResult(this._databaseRepository.TryLoadDatabases(connection));
            }

            this.UpdateConnectionsListView();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var saveResult = this._connectionRepository.TrySaveConnectionsToConfig();
            if (!saveResult.Successful)
            {
                var message = string.Format(CultureInfo.CurrentCulture, Messages.SaveConnectionsFailedYesNoQuestion, saveResult.ErrorMessage);
                var messageBoxResult = MessageBox.Show(message, Messages.SaveConnectionsFailedCaption, MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void ConnectionsListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.UpdateButtonStatus();
            this.UpdateDatabaseListView();
        }

        private void DatabasesListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.UpdateButtonStatus();
            this.UpdateSnapshotListView();
        }

        private void SnapshotsListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.UpdateButtonStatus();
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
                HandleResult(this._databaseRepository.TryLoadDatabases(result.Value));
                this.UpdateConnectionsListView();
                this.UpdateDatabaseListView();
            }
        }

        private void RemoveConnectionButton_OnClickConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedConnections = this.connectionsListView.SelectedItems.Cast<ConnectionInfo>().ToList();
            foreach (var connection in selectedConnections)
            {
                this._connectionRepository.RemoveConnection(connection);
                this._databaseRepository.ClearDatabases(connection);
                this._snapshotRepository.ClearSnapshots(connection);
            }

            this.UpdateButtonStatus();
            this.UpdateConnectionsListView();
            this.UpdateDatabaseListView();
        }

        private void CreateSnapshotButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDatabase = (DatabaseInfo)this.databasesListView.SelectedItem;
            if (selectedDatabase == null)
            {
                return;
            }

            var newSnapshotDialo = new NewSnapshotDialog();
            var result = newSnapshotDialo.Prompt("MySnapshot_" + DateTime.Now.ToFileTime());
            if (result.HasValue)
            {
                HandleResult(this._snapshotRepository.TryCreateSnapshot(result.Value, selectedDatabase));

                this.UpdateButtonStatus();
                this.UpdateSnapshotListView();
            }
        }

        private void RestoreSnapshotButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSnapshot = (SnapshotInfo)this.snapshotsListView.SelectedItem;
            if (selectedSnapshot == null)
            {
                return;
            }

            var restoreResult = this._snapshotRepository.TryRestoreSnapshot(selectedSnapshot);
            HandleResult(restoreResult);
            if (restoreResult.Successful)
            {
                MessageBox.Show(Messages.SnapshotRestored);
            }

            this.UpdateButtonStatus();
            this.UpdateSnapshotListView();
        }

        private void DeleteSnapshotButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSnapshots = this.snapshotsListView.SelectedItems.Cast<SnapshotInfo>().ToList();
            foreach (var snapshot in selectedSnapshots)
            {
                HandleResult(this._snapshotRepository.TryDeleteSnapshot(snapshot));
            }

            this.UpdateButtonStatus();
            this.UpdateSnapshotListView();
        }

        private void RefreshDatabasesButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedConnection = (ConnectionInfo)this.connectionsListView.SelectedItem;
            if (selectedConnection != null)
            {
                HandleResult(this._databaseRepository.TryLoadDatabases(selectedConnection));
            }

            this.UpdateButtonStatus();
            this.UpdateDatabaseListView();
            this.UpdateSnapshotListView();
        }

        private void RefreshSnapshotsButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDatabase = (DatabaseInfo)this.databasesListView.SelectedItem;
            if (selectedDatabase != null)
            {
                HandleResult(this._snapshotRepository.TryLoadSnapshots(selectedDatabase));
            }

            this.UpdateButtonStatus();
            this.UpdateSnapshotListView();
        }

        #endregion
    }
}
