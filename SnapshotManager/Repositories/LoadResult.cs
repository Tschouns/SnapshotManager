//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Base;

namespace SnapshotManager.Repositories
{
    /// <summary>
    /// Represents a result for a "load" method.
    /// </summary>
    public class LoadResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadResult"/> class.
        /// </summary>
        private LoadResult(bool successful, string errorMessage)
        {
            this.Successful = successful;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Creates a successful <see cref="LoadResult"/>.
        /// </summary>
        public static LoadResult CreateSuccessful()
        {
            return new LoadResult(true, string.Empty);
        }

        /// <summary>
        /// Creates a failed <see cref="LoadResult"/>.
        /// </summary>
        public static LoadResult CreateFailed(string errorMessage)
        {
            ArgumentChecks.AssertNotNull(errorMessage, nameof(errorMessage));

            return new LoadResult(false, errorMessage);
        }

        /// <summary>
        /// Gets a value indicating whether the loading was successful.
        /// </summary>
        public bool Successful { get; }

        /// <summary>
        /// Gets the error message, in case the loading was not successful.
        /// </summary>
        public string ErrorMessage { get; }
    }
}
