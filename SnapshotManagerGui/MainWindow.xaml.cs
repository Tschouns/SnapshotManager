//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui
{
    using DbServerPlugin;
    using DbServerPluginMsSql2014;
    using NewConnection;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // Quick hack...
            DbServerPluginRegistry.RegisterPlugin(
                MsSql2014.Identifier,
                new MsSql2014DbServerInfo(),
                null);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void addConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            var newConnectionWindow = new NewConnectionWindow();
            var result = newConnectionWindow.ShowDialog();

            if (result.GetValueOrDefault())
            {
                MessageBox.Show("Connect... " + newConnectionWindow.Model.DbServer.Description);
            }
        }
    }
}
