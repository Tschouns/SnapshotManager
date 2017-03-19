//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui.NewConnection
{
    using Base;
    using DbServerPlugin;
    using SnapshotManager;
    using System.Collections.Generic;

    /// <summary>
    /// GUI helper class which prompts the "New Connection" dialog.
    /// </summary>
    public class NewConnectionDialog
    {
        /// <summary>
        /// Promts the user to enter a new connection.
        /// </summary>
        public NullableResult<ConnectionInfo> Prompt(IEnumerable<DbServerPluginInfo> availablePlugins)
        {
            ArgumentChecks.AssertNotNull(availablePlugins, nameof(availablePlugins));

            var newConnectionWindow = new NewConnectionWindow();

            newConnectionWindow.SetAvailableDbServerPlugins(availablePlugins);

            if (newConnectionWindow.ShowDialog() == true)
            {
                var model = newConnectionWindow.Model;
                var newConnection = new ConnectionInfo(
                    model.DbServerPlugin,
                    model.Host,
                    model.PortNumber,
                    model.UserId,
                    model.Password);
            }

            return new NullableResult<ConnectionInfo>(null);
        }
    }
}
