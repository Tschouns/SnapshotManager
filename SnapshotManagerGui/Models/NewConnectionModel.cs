//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui.Models
{
    using DbServerPlugin;

    /// <summary>
    /// Represents the view model of the "New Connection" dialog.
    /// </summary>
    public class NewConnectionModel : BaseViewModel
    {
        private DbServerPluginInfo _dbServerPlugin;
        private string _host = string.Empty;
        private int _portNumber = 0;
        private string _userId = string.Empty;
        private string _password = string.Empty;

        /// <summary>
        /// Gets or sets the DB server plugin.
        /// </summary>
        public DbServerPluginInfo DbServerPlugin
        {
            get { return this._dbServerPlugin; }
            set
            {
                if (this._dbServerPlugin != value)
                {
                    this._dbServerPlugin = value;
                    this.InvokePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host
        {
            get { return this._host; }
            set
            {
                if (this._host != value)
                {
                    this._host = value;
                    this.InvokePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        public int PortNumber
        {
            get { return this._portNumber; }
            set
            {
                if (this._portNumber != value)
                {
                    this._portNumber = value;
                    this.InvokePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public string UserId
        {
            get { return this._userId; }
            set
            {
                if (this._userId != value)
                {
                    this._userId = value;
                    this.InvokePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password
        {
            get { return this._password; }
            set
            {
                if (this._password != value)
                {
                    this._password = value;
                    this.InvokePropertyChanged();
                }
            }
        }
    }
}
