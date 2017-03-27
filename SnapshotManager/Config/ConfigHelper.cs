//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Config
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public class ConfigHelper : IConfigHelper
    {
        public void AddConnectionToConfiguration(Configuration config, ConnectionInfo connection)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConnectionInfo> GetConnectionsFromConfiguration(Configuration config)
        {
            throw new NotImplementedException();
        }

        public void RemoveConnectionFromConfiguration(Configuration config, ConnectionInfo connection)
        {
            throw new NotImplementedException();
        }
    }
}
