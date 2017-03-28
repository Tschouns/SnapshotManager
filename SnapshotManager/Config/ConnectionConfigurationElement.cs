//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Config
{
    using System.Configuration;

    /// <summary>
    /// Configuration element which represents a database server connection.
    /// </summary>
    public class ConnectionConfigurationElement : ConfigurationElement
    {
        private const string ConnectionDbServerInfo = "DbServer";
        private const string ConnectionHost = "Host";
        private const string ConnectionUsesIntegratedSecurity = "IntegratedSecurity";
        private const string ConnectionUserId = "UserId";
        private const string ConnectionPassword = "Password";

        private const string Empty = "";
        private const string BooleanTrue = "Yes";
        private const string BooleanFalse = "No";

        /// <summary>
        /// Gets or sets the DB server plugin info attribute.
        /// </summary>
        [ConfigurationProperty(ConnectionDbServerInfo, DefaultValue = Empty)]
        public string DbServerPluginInfo
        {
            get
            {
                return (string)this[ConnectionDbServerInfo];
            }
            set
            {
                this[ConnectionDbServerInfo] = value;
            }
        }

        /// <summary>
        /// Gets or sets the host attribute.
        /// </summary>
        [ConfigurationProperty(ConnectionHost, DefaultValue = Empty)]
        public string Host
        {
            get
            {
                return (string)this[ConnectionHost];
            }
            set
            {
                this[ConnectionHost] = value;
            }
        }

        /// <summary>
        /// Gets or sets the integrated security attribute.
        /// </summary>
        public bool UsesIntegratedSecurity
        {
            get
            {
                return this.IntegratedSecurity == BooleanTrue;
            }
            set
            {
                this.IntegratedSecurity = value ? BooleanTrue : BooleanFalse;
            }
        }

        /// <summary>
        /// Gets or sets the user ID attribute.
        /// </summary>
        [ConfigurationProperty(ConnectionUserId, DefaultValue = Empty)]
        public string UserId
        {
            get
            {
                return (string)this[ConnectionUserId];
            }
            set
            {
                this[ConnectionUserId] = value;
            }
        }

        /// <summary>
        /// Gets or sets the password attribute.
        /// </summary>
        [ConfigurationProperty(ConnectionPassword, DefaultValue = Empty)]
        public string Password
        {
            get
            {
                return (string)this[ConnectionPassword];
            }
            set
            {
                this[ConnectionPassword] = value;
            }
        }

        /// <summary>
        /// This property is used merely to parse the value from the XML - the value is only exposed
        /// via the <see cref="UsesIntegratedSecurity"/> property, typed as <see cref="bool"/>.
        /// </summary>
        [ConfigurationProperty(ConnectionUsesIntegratedSecurity, DefaultValue = BooleanTrue)]
        private string IntegratedSecurity
        {
            get
            {
                return (string)this[ConnectionUsesIntegratedSecurity];
            }
            set
            {
                this[ConnectionUsesIntegratedSecurity] = value;
            }
        }
    }
}
