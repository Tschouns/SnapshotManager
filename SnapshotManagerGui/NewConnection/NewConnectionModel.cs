//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui.NewConnection
{
    using Base;
    using DbServerPlugin;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class NewConnectionModel : INotifyPropertyChanged
    {
        private IDbServerInfo _dbServer;
        private string _host;
        private int _portNumber;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the DB server.
        /// </summary>
        public IDbServerInfo DbServer
        {
            get { return this._dbServer; }
            set
            {
                if (this._dbServer != value)
                {
                    this._dbServer = value;
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

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
