using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// A collection of <see cref="SwitchConfig"/> objects
    /// </summary>
    [ConfigurationCollection(typeof(SwitchConfig), AddItemName = "switch")]
    public class SwitchConfigCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Gets a <see cref="SwitchConfig"/> element
        /// </summary>
        /// <param name="index">The index of the element</param>
        /// <returns>A <see cref="SwitchConfig"/> object</returns>
        public SwitchConfig this[int index]
        {
            get
            {
                return this.BaseGet(index) as SwitchConfig;
            }
        }

        /// <summary>
        /// Creates a new element
        /// </summary>
        /// <returns>A new <see cref="SwitchConfig"/> object</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new SwitchConfig();
        }

        /// <summary>
        /// Gets a key to compare children
        /// </summary>
        /// <param name="element">The element to compare</param>
        /// <returns>The <see cref="SwitchConfig.Name"/> value</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as SwitchConfig).Name;
        }        
    }
}
