//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPlugin.Services
{
    using Base;

    /// <summary>
    /// Represents a database.
    /// </summary>
    public class Database
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        public Database(string name)
        {
            ArgumentChecks.AssertNotNull(name, nameof(name));

            this.Name = name;
        }

        /// <summary>
        /// Gets the database name.
        /// </summary>
        public string Name { get; }
    }
}
