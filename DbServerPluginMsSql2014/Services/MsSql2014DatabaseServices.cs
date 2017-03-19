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

    public class MsSql2014DatabaseServices : IDatabaseServices
    {
        public IEnumerable<string> GetAllDatabases(string host, int portNumber, string userId, string password)
        {
            throw new NotImplementedException();
        }
    }
}
