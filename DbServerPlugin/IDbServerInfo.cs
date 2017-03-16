//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPlugin
{
    /// <summary>
    /// Provides information about the database server.
    /// </summary>
    public interface IDbServerInfo
    {
        /// <summary>
        /// Gets the database server description.
        /// </summary>
        string Description { get; }
    }
}
