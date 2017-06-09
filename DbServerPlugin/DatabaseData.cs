//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DbServerPlugin
{
    using System.Collections.Generic;
    using Base;

    /// <summary>
    /// Represents a specific database.
    /// </summary>
    public class DatabaseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseData"/> class.
        /// </summary>
        public DatabaseData(
            string name,
            IEnumerable<string> physicalFilePaths)
        {
            ArgumentChecks.AssertNotNullOrEmpty(name, nameof(name));
            ArgumentChecks.AssertNotNull(physicalFilePaths, nameof(physicalFilePaths));

            this.Name = name;
            this.PhysicalFilePaths = physicalFilePaths;
        }

        /// <summary>
        /// Gets the database name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets physical file paths (including location and file name).
        /// </summary>
        public IEnumerable<string> PhysicalFilePaths { get; }
    }
}
