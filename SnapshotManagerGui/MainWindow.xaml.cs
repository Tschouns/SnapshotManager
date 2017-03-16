//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui
{
    using DbServerPlugin;
    using DbServerPluginMsSql2014;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Quick hack...
            DbServerPluginRegistry.RegisterPlugin(
                new MsSql2014DbServerInfo(),
                new MsSql2014CommandProvider());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
