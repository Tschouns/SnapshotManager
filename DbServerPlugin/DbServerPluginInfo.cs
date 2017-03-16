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
            IDbServerInfo serverInfo,
            ICommandProvider commands)
        {
            ArgumentChecks.AssertNotNull(serverInfo, nameof(serverInfo));
            ArgumentChecks.AssertNotNull(commands, nameof(commands));

            this.ServerInfo = serverInfo;
            this.Commands = commands;
        }

        /// <summary>
        /// Gets the <see cref="IDbServerInfo"/> for this plug-in.
        /// </summary>
        public IDbServerInfo ServerInfo { get; }

        /// <summary>
        /// Gets the <see cref="ICommandProvider"/> for this plug-in.
        /// </summary>
        public ICommandProvider Commands { get; }
    }
}
