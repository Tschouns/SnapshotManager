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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            this.ConnectionRepository = new ConnectionRepository();
        }

        /// <summary>
        /// Gets the <see cref="ConnectionRepository"/>;
        /// </summary>
        public IConnectionRepository ConnectionRepository { get; }

        private void UpdateConnectionsListView()
        {
            this.connectionsListView.Items.Clear();
            foreach (var connection in this.ConnectionRepository.GetConnections())
            {
                this.connectionsListView.Items.Add(connection);
            }
        }

        private void addConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            var newConnectionDialog = new NewConnectionDialog();
            var result = newConnectionDialog.Prompt(DbServerPluginRegistry.GetAllPlugins());
            if (result.HasValue)
            {
                this.ConnectionRepository.AddConnection(result.Value);
                this.UpdateConnectionsListView();
            }
        }
    }
}
