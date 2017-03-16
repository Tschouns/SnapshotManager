//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPluginMsSql2014
{
    using System;
    using DbServerPlugin;

    /// <summary>
    /// See <see cref="IDbServerInfo"/>.
    /// </summary>
    public class MsSql2014DbServerInfo : IDbServerInfo
    {
        /// <summary>
        /// See <see cref="IDbServerInfo.Description"/>.
        /// </summary>
        string IDbServerInfo.Description => DbServerInfo.Description;
    }
}
