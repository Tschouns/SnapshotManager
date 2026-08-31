//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Base;
    using DbServerPlugin;
    using DbServerPluginMsSql2014;
    using DbServerPluginMsSql2014.Services;
    using SnapshotManager;
    using SnapshotManager.Repositories;
    using SnapshotManager.Services;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConnectionRepository _connectionRepository;
        private readonly IDatabaseRepository _databaseRepository;
        private readonly ISnapshotRepository _snapshotRepository;

        private readonly DeleteDatabaseService _deleteDatabaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Quick hack...
            DbServerPluginRegistry.RegisterPlugin(
                MsSql2014.Identifier,
                new MsSql2014DbServerInfo(),
                new MsSql2014DatabaseServices(),
                new MsSql2014SnapshotServices());

            _connectionRepository = new ConnectionRepository();
            _databaseRepository = new DatabaseRepository();
            _snapshotRepository = new SnapshotRepository();

            _deleteDatabaseService = new DeleteDatabaseService(
                _databaseRepository,
                _snapshotRepository);

            UpdateButtonStatus();
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
            this.deleteDatabaseButton.IsEnabled = isDatabaseSelected;
            this.restoreSnapshotButton.IsEnabled = isSnapshotSelected;
            this.deleteSnapshotButton.IsEnabled = isSnapshotSelected;
        }

        private void UpdateConnectionsListView()
        {
            this.connectionsListView.Items.Clear();

            var connections = _connectionRepository
                .GetConnections()
                .OrderBy(c => c.Host);

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

            var databases = _databaseRepository
                .GetLoadedDatabases(selectedConnection)
                .OrderBy(d => d.Name);

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
            
            var snapshots = _snapshotRepository
                .GetLoadedSnapshots(selectedDatabase)
                .OrderBy(s => s.Name);

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

        /// <summary>
        /// Copies the names of selected list items to the clipboard when Ctrl+C is pressed.
        /// </summary>
        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.C || Keyboard.Modifiers != ModifierKeys.Control)
            {
                return;
            }

            if (CopySelectedItemNames((ListView)sender))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Selects the list item under the mouse pointer before opening its context menu.
        /// </summary>
        private void ListView_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var listView = (ListView)sender;
            var source = Mouse.DirectlyOver as DependencyObject;
            var listViewItem = source == null
                ? null
                : ItemsControl.ContainerFromElement(listView, source) as ListViewItem;

            if (listViewItem == null)
            {
                e.Handled = true;
                return;
            }

            listViewItem.IsSelected = true;
            listViewItem.Focus();
        }

        /// <summary>
        /// Copies the selected item names from a list view's context menu.
        /// </summary>
        private void CopyNameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var contextMenu = ItemsControl.ItemsControlFromItemContainer((MenuItem)sender) as ContextMenu;
            var listView = contextMenu?.PlacementTarget as ListView;
            if (listView != null)
            {
                CopySelectedItemNames(listView);
            }
        }

        /// <summary>
        /// Copies the names of selected list items to the clipboard.
        /// </summary>
        private static bool CopySelectedItemNames(ListView listView)
        {
            var selectedNames = listView.SelectedItems
                .Cast<object>()
                .Select(GetItemName)
                .Where(name => !string.IsNullOrEmpty(name));
            var clipboardText = string.Join(Environment.NewLine, selectedNames);

            if (string.IsNullOrEmpty(clipboardText))
            {
                return false;
            }

            Clipboard.SetText(clipboardText);
            return true;
        }

        /// <summary>
        /// Gets the displayed name of a connection, database, or snapshot.
        /// </summary>
        private static string GetItemName(object item)
        {
            var connection = item as ConnectionInfo;
            if (connection != null)
            {
                return connection.Host;
            }

            var database = item as DatabaseInfo;
            if (database != null)
            {
                return database.Name;
            }

            var snapshot = item as SnapshotInfo;
            return snapshot?.Name;
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

            var newSnapshotDialog = new NewSnapshotDialog();
            var result = newSnapshotDialog.Prompt(selectedDatabase.Name + "_Snapshot_" + DateTime.Now.ToFileTime());
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


        private void DeleteDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDatabases = databasesListView.SelectedItems.Cast<DatabaseInfo>().ToList();
            var deleteIncludingSnapshots = ShouldDeleteIncludingSnapshots(selectedDatabases);

            if (deleteIncludingSnapshots == null)
            {
                return;
            }

            selectedDatabases.ForEach(d =>
                HandleResult(
                    deleteIncludingSnapshots.Value
                        ? _deleteDatabaseService.TryDeleteDatabaseIncludingSnapshots(d)
                        : _databaseRepository.TryDeleteDatabase(d)));

            this.UpdateButtonStatus();
            this.UpdateDatabaseListView();
            this.UpdateSnapshotListView();
        }

        private bool? ShouldDeleteIncludingSnapshots(IReadOnlyCollection<DatabaseInfo> databases)
        {
            // User may hold Ctrl and/or Alt to also delete database snapshots without the need for user confirmation.
            if ((Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Alt)) != ModifierKeys.None)
            {
                return true;
            }

            if (!databases.Any(HasLoadedSnapshots))
            {
                return false;
            }

            var messageBoxResult = MessageBox.Show(
                "Do you also want to delete the snapshots?\n\n" +
                "Hint:\n" + 
                "Hold Ctrl or Alt while clicking the Delete button\n" +
                "to have this confirmation dialog skipped.",
                "Delete database snapshots",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Warning);

            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    return true;

                case MessageBoxResult.No:
                    return false;

                default:
                    return null;
            }
        }

        private bool HasLoadedSnapshots(DatabaseInfo database)
        {
            return _snapshotRepository.GetLoadedSnapshots(database).Any();
        }

        #endregion

    }
}
