//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui.NewConnection
{
    using Base;
    using DbServerPlugin;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for NewConnectionWindow.xaml
    /// </summary>
    public partial class NewConnectionWindow : Window
    {
        public NewConnectionWindow()
        {
            this.InitializeComponent();

            var dbServerPluginInfos = DbServerPluginRegistry.GetAllPlugins();
            var dbServers = dbServerPluginInfos.Select(p => p.ServerInfo).ToList();

            this.dbServerComboBox.ItemsSource = dbServers;

            this.Model = new NewConnectionModel();
            this.DataContext = this.Model;
        }

        public NewConnectionModel Model { get; }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
