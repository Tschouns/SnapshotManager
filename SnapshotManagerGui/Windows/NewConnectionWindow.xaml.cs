//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui.Windows
{
    using Base;
    using DbServerPlugin;
    using Models;
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

            this.dbServerComboBox.Focus();
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

            // Preselect the first entry (if any).
            if (pluginsOrdered.Any())
            {
                this.Model.DbServerPlugin = pluginsOrdered.First();
            }

            this.dbServerComboBox.IsEnabled = pluginsOrdered.Count > 1;
        }

        private void SetLoginInfoIsEnabled(bool isEnabled)
        {
            this.userIdLabel.IsEnabled = isEnabled;
            this.userIdTextBox.IsEnabled = isEnabled;
            this.passwordLabel.IsEnabled = isEnabled;
            this.passwordTextBox.IsEnabled = isEnabled;
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            this.Model.Password = this.passwordTextBox.Password;

            this.DialogResult = true;
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void IntegratedSecurityCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.SetLoginInfoIsEnabled(false);
        }

        private void IntegratedSecurityCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.SetLoginInfoIsEnabled(true);
        }
    }
}
