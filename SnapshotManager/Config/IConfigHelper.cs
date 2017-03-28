//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Config
{
    using System.Collections.Generic;
    using System.Configuration;

    /// <summary>
    /// Provides methods to get app specific configuration item from a specified
    /// <see cref="Configuration"/>, or add such item to said <see cref="Configuration"/>.
    /// </summary>
    public interface IConfigHelper
    {
        /// <summary>
        /// Gets all configured connections from the specified <see cref="Configuration"/>.
        /// </summary>
        IEnumerable<ConnectionInfo> GetConnectionsFromConfiguration(Configuration config);

        /// <summary>
        /// Adds the specified connection to the specified <see cref="Configuration"/>.
        /// </summary>
        void AddConnectionToConfiguration(Configuration config, ConnectionInfo connection);

        /// <summary>
        /// Clears all the connections from the specified <see cref="Configuration"/>.
        /// </summary>
        void ClearConnectionsFromConfiguration(Configuration config);
    }
}
