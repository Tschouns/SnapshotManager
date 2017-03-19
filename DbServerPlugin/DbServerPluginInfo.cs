//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPlugin
{
    using Base;
    using DbServerPluginMsSql2014.Services;

    /// <summary>
    /// Provides all the plug-in specific information and interfaces.
    /// </summary>
    public class DbServerPluginInfo
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DbServerPluginInfo(
            string pluginIdentifier,
            IDbServerInfo serverInfo,
            DbServerPluginServiceFacade services)
        {
            ArgumentChecks.AssertNotNull(pluginIdentifier, nameof(pluginIdentifier));
            ArgumentChecks.AssertNotNull(serverInfo, nameof(serverInfo));
            ArgumentChecks.AssertNotNull(services, nameof(services));

            this.PluginIdentifier = PluginIdentifier;
            this.ServerInfo = serverInfo;
            this.Services = services;
        }

        /// <summary>
        /// Gets an identifier for this plug-in, which can be used as key of for identifying the
        /// plug-in in a database or XML file.
        /// </summary>
        public string PluginIdentifier { get; }

        /// <summary>
        /// Gets the <see cref="IDbServerInfo"/> for this plug-in.
        /// </summary>
        public IDbServerInfo ServerInfo { get; }

        /// <summary>
        /// Gets the <see cref="DbServerPluginServiceFacade"/> for this plug-in.
        /// </summary>
        public DbServerPluginServiceFacade Services { get; }

        /// <summary>
        /// See <see cref="object.ToString"/>.
        /// </summary>
        public override string ToString()
        {
            return this.ServerInfo.Description;
        }
    }
}
