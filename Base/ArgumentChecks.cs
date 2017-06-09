//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base
{
    using System;

    /// <summary>
    /// Provides runtime checks for arguments, throws exceptions.
    /// </summary>
    public static class ArgumentChecks
    {
        /// <summary>
        /// Asserts that a specified argument is not <c>null</c>.
        /// </summary>
        public static void AssertNotNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Asserts that a specified string argument is not <c>null</c> or empty.
        /// </summary>
        public static void AssertNotNullOrEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
