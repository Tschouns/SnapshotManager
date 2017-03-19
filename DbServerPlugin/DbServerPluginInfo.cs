//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPlugin
{
    using Base;

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
            IDbServerPluginServiceFacade services)
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
        /// Gets the <see cref="IDbServerPluginServiceFacade"/> for this plug-in.
        /// </summary>
        public IDbServerPluginServiceFacade Services { get; }
    }
}
