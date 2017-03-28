//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Config
{
    using System.Configuration;
    using System.Linq;
    using Base;

    /// <summary>
    /// Collection which contains connection configuration elements.
    /// </summary>
    public class ConnectionConfigurationElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Determines whether the collection contains an element with the same key as the one specified.
        /// </summary>
        public bool Contains(ConnectionConfigurationElement element)
        {
            ArgumentChecks.AssertNotNull(element, nameof(element));

            var contains = BaseGetAllKeys()
                .Cast<string>()
                .Contains(this.GetElementKey(element));

            return contains;
        }

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        public void Add(ConnectionConfigurationElement element)
        {
            ArgumentChecks.AssertNotNull(element, nameof(element));

            this.BaseAdd(element);
        }

        /// <summary>
        /// Removes the specified element.
        /// </summary>
        public void Remove(ConnectionConfigurationElement element)
        {
            ArgumentChecks.AssertNotNull(element, nameof(element));

            this.BaseRemove(this.GetElementKey(element));
        }

        /// <summary>
        /// Clears all the elements in the collection
        /// </summary>
        public void Clear()
        {
            this.BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConnectionConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConnectionConfigurationElement) element).Host;
        }
    }
}
