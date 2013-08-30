using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// A definition for a switch
    /// </summary>
    public class SwitchConfig : ConfigurationElement
    {
        private const string NAME_PROP = "name";
        private const string TYPE_PROP = "type";
        private const string VALUE_PROP = "value";

        /// <summary>
        /// The name of the switch (must be unique)
        /// </summary>
        [ConfigurationProperty(NAME_PROP, IsKey = true)]
        public string Name
        {
            get { return this[NAME_PROP] as string; }
            set { this[NAME_PROP] = value; }
        }

        /// <summary>
        /// The full type name of the switch
        /// </summary>
        [ConfigurationProperty(TYPE_PROP)]
        public string Type
        {
            get { return this[TYPE_PROP] as string; }
            set { this[TYPE_PROP] = value; }
        }

        /// <summary>
        /// The (optional) reference value to pass to the switch
        /// </summary>
        [ConfigurationProperty(VALUE_PROP)]
        public string Value
        {
            get { return this[VALUE_PROP] as string; }
            set { this[VALUE_PROP] = value; }
        }
    }
}
