//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Base;

namespace SnapshotManager
{
    /// <summary>
    /// Represents a database server connection.
    /// </summary>
    public class ConnectionInfo
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ConnectionInfo(
            string userId,
            string password)
        {
            ArgumentChecks.AssertNotNull(userId, nameof(userId));
            ArgumentChecks.AssertNotNull(password, nameof(password));

            this.UserId = userId;
            this.Password = password;
        }

        /// <summary>
        /// Gets the user ID used for the connection.
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Gets the password used for the connection.
        /// </summary>
        public string Password { get; }
    }
}
