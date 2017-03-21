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

            try
            {
                var databases = this._databaseRepository.GetDatabasesForConnection(selectedConnection);
                foreach (var database in databases)
                {
                    this.databasesListView.Items.Add(database);
                }
            }
            catch(SnapshotException ex)
            {
                MessageBox.Show(ex.Message + $" ({ex.InnerException.Message})");
            }
        }

        private void connectionsListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.UpdateDatabaseListView();
        }

        private void AddConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            var newConnectionDialog = new NewConnectionDialog();
            var result = newConnectionDialog.Prompt(DbServerPluginRegistry.GetAllPlugins());
            if (result.HasValue)
            {
                this._connectionRepository.AddConnection(result.Value);
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
    }
}
