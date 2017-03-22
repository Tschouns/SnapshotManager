//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Repositories
{
    using Base;

    /// <summary>
    /// Represents a result which tells whether an operation was successful or not, and
    /// also contains an error message if the operation failed.
    /// </summary>
    public class SuccessResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessResult"/> class.
        /// </summary>
        private SuccessResult(bool successful, string errorMessage)
        {
            this.Successful = successful;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Creates a successful <see cref="SuccessResult"/>.
        /// </summary>
        public static SuccessResult CreateSuccessful()
        {
            return new SuccessResult(true, string.Empty);
        }

        /// <summary>
        /// Creates a failed <see cref="SuccessResult"/>.
        /// </summary>
        public static SuccessResult CreateFailed(string errorMessage)
        {
            ArgumentChecks.AssertNotNull(errorMessage, nameof(errorMessage));

            return new SuccessResult(false, errorMessage);
        }

        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        public bool Successful { get; }

        /// <summary>
        /// Gets the error message, in case the operation failed.
        /// </summary>
        public string ErrorMessage { get; }
    }
}
