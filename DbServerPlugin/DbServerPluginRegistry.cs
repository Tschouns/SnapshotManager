//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPlugin
{
    using Base;
    using DbServerPluginMsSql2014.Services;
    using Services;
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
            IDatabaseServices databases,
            ISnapshotServices snapshots)
        {
            var plugin = new DbServerPluginInfo(
                pluginIdentifier,
                serverInfo,
                new DbServerPluginServiceFacade(
                    databases,
                    snapshots));

            _dbServerPlugins.Add(plugin);
        }

        /// <summary>
        /// Gets all registered DB server plug-ins.
        /// </summary>
        public static IEnumerable<DbServerPluginInfo> GetAllPlugins()
        {
            return _dbServerPlugins.ToList();
        }

        /// <summary>
        /// Gets all registered DB server plug-in identifiers.
        /// </summary>
        public static IEnumerable<string> GetAllPluginIdentifiers()
        {
            return _dbServerPlugins.Select(p => p.PluginIdentifier).ToList();
        }

        /// <summary>
        /// Gets the registered DB server plug-in by the specified identifier.
        /// </summary>
        public static NullableResult<DbServerPluginInfo> GetPluginByIdentifier(string pluginIdentifier)
        {
            ArgumentChecks.AssertNotNull(pluginIdentifier, nameof(pluginIdentifier));

            var plugin = _dbServerPlugins.Where(p => p.PluginIdentifier == pluginIdentifier).FirstOrDefault();

            return new NullableResult<DbServerPluginInfo>(plugin);
        }
    }
}
