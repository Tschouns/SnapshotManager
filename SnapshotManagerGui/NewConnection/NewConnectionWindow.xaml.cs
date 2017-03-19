//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui.NewConnection
{
    using Base;
    using DbServerPlugin;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for NewConnectionWindow.xaml
    /// </summary>
    public partial class NewConnectionWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewConnectionWindow"/> class.
        /// </summary>
        public NewConnectionWindow()
        {
            this.InitializeComponent();

            this.Model = new NewConnectionModel();
            this.DataContext = this.Model;
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        public NewConnectionModel Model { get; }

        /// <summary>
        /// Sets the available plugins.
        /// </summary>
        public void SetAvailableDbServerPlugins(IEnumerable<DbServerPluginInfo> availblePlugins)
        {
            ArgumentChecks.AssertNotNull(availblePlugins, nameof(availblePlugins));

            var pluginsOrdered = availblePlugins.OrderBy(p => p.ToString()).ToList();
            this.dbServerComboBox.ItemsSource = pluginsOrdered;
        }

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
