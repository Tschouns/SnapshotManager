//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base
{
    /// <summary>
    /// Represents a method result which may or may not contain a value of
    /// type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value contained by the result
    /// </typeparam>
    public class NullableResult<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullableResult{T}"/> class.
        /// </summary>
        /// <param name="value"></param>
        public NullableResult(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets a value indicating whether the result contains an actual value.
        /// </summary>
        public bool HasValue => this.Value != null;
    }
}
