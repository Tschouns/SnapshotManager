//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
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
        public DbServerPluginInfo(ICommandProvider commands)
        {
            ArgumentChecks.AssertNotNull(commands, nameof(commands));

            this.Commands = commands;
        }

        /// <summary>
        /// Gets the <see cref="ICommandProvider"/> for this plug-in.
        /// </summary>
        public ICommandProvider Commands { get; }
    }
}
