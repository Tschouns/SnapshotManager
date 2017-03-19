//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPlugin
{
    using System.Collections.Generic;
    using System.Linq;

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
            string pluginIdentifier,
            IDbServerInfo serverInfo,
            IDbServerPluginServiceFacade services)
        {
            var plugin = new DbServerPluginInfo(
                pluginIdentifier,
                serverInfo,
                services);

            _dbServerPlugins.Add(plugin);
        }

        /// <summary>
        /// Gets all registered DB server plug-ins.
        /// </summary>
        public static IEnumerable<string> GetAllPlugins()
        {
            return _dbServerPlugins.Select(p => p.PluginIdentifier).ToList();
        }
    }
}
