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

    public class NewConnectionModel : INotifyPropertyChanged
    {
        private IDbServerInfo _dbServer;
        private string _host;
        private int _portNumber;

        public event PropertyChangedEventHandler PropertyChanged;

        public IDbServerInfo DbServer
        {
            get { return this._dbServer; }
            set
            {
                this._dbServer = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyUtils.GetPropertyName<NewConnectionModel, IDbServerInfo>(m => m.DbServer)));
            }
        }

        public string Host
        {
            get { return this._host; }
            set
            {
                this._host = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyUtils.GetPropertyName<NewConnectionModel, string>(m => m.Host)));
            }
        }

        public int PortNumber
        {
            get { return this._portNumber; }
            set
            {
                this._portNumber = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyUtils.GetPropertyName<NewConnectionModel, int>(m => m.PortNumber)));
            }
        }
    }
}
