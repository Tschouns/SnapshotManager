//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManagerGui
{
    using Base;
    using Windows;

    /// <summary>
    /// GUI helper class which prompts the "New Snapshot" dialog.
    /// </summary>
    public class NewSnapshotDialog
    {
        /// <summary>
        /// Promts the user to enter the name for the new snapshot.
        /// </summary>
        public NullableResult<string> Prompt(string suggestedName)
        {
            ArgumentChecks.AssertNotNullOrEmpty(suggestedName, nameof(suggestedName));

            var newSnapshotWindow = new NewSnapshotWindow();
            newSnapshotWindow.SnapshotName = suggestedName;

            if (newSnapshotWindow.ShowDialog() == true &&
                !string.IsNullOrWhiteSpace(newSnapshotWindow.SnapshotName))
            {
                return new NullableResult<string>(newSnapshotWindow.SnapshotName);
            }

            return new NullableResult<string>(null);
        }
    }
}
