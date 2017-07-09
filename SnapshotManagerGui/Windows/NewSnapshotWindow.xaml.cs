using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SnapshotManagerGui.Windows
{
    /// <summary>
    /// Interaction logic for NewSnapshotWindow.xaml
    /// </summary>
    public partial class NewSnapshotWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewSnapshotWindow"/> class.
        /// </summary>
        public NewSnapshotWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the snapshot name.
        /// </summary>
        public string SnapshotName
        {
            get
            {
                return this.snapShotNameTextBox.Text;
            }
            set
            {
                this.snapShotNameTextBox.Text = value;
                this.snapShotNameTextBox.SelectAll();
                this.snapShotNameTextBox.Focus();
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.SnapshotName))
            {
                MessageBox.Show(Messages.EnterSnapshotName);
            }

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
