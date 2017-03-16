//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPlugin
{
    using System.Collections.Generic;

    /// <summary>
    /// Allows to register and retrieve DB server plug-ins.
    /// </summary>
    public static class DbServerPluginRegistry
    {
        private static readonly IList<DbServerPluginInfo> _dbServerPlugins = new List<DbServerPluginInfo>();

        /// <summary>
        /// Registers a DB server plug-in.
        /// </summary>
        public static void RegisterPlugin(
            IDbServerInfo serverInfo,
            ICommandProvider commands)
        {
            var plugin = new DbServerPluginInfo(
                serverInfo,
                commands);

            _dbServerPlugins.Add(plugin);
        }

        /// <summary>
        /// Gets all registered DB server plug-ins.
        /// </summary>
        public static IEnumerable<DbServerPluginInfo> GetAllPlugins()
        {
            return _dbServerPlugins;
        }
    }
}
