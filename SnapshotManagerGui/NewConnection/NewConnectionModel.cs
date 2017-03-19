﻿//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui.NewConnection
{
    using DbServerPlugin;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class NewConnectionModel : INotifyPropertyChanged
    {
        private DbServerPluginInfo _dbServerPlugin;
        private string _host;
        private int _portNumber;
        private string _userId;
        private string _password;

        public event PropertyChangedEventHandler PropertyChanged;

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
                    this.OnPropertyChanged();
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
                    this.OnPropertyChanged();
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
                    this.OnPropertyChanged();
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
                    this.OnPropertyChanged();
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
                    this.OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
