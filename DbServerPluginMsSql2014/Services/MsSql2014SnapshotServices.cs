//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPluginMsSql2014.Services
{
    using System;
    using System.Collections.Generic;
    using DbServerPlugin.Services;

    public class MsSql2014SnapshotServices : ISnapshotServices
    {
        public void CreateSnapshot(string snapshotName, string database, string host, int portNumber, string userId, string password)
        {
            throw new NotImplementedException();
        }

        public void DeleteSnapshot(string snapshotName, string host, int portNumber, string userId, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllSnapshots(string database, string host, int portNumber, string userId, string password)
        {
            throw new NotImplementedException();
        }

        public void RestoreSnapshot(string snapshotName, string host, int portNumber, string userId, string password)
        {
            throw new NotImplementedException();
        }
    }
}
