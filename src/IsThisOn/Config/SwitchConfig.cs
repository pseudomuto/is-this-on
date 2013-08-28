using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    public class SwitchConfig : ConfigurationElement
    {
        private const string NAME_PROP = "name";
        private const string TYPE_PROP = "type";
        private const string VALUE_PROP = "value";

        [ConfigurationProperty(NAME_PROP)]
        public string Name
        {
            get { return this[NAME_PROP] as string; }
            set { this[NAME_PROP] = value; }
        }

        [ConfigurationProperty(TYPE_PROP)]
        public string Type
        {
            get { return this[TYPE_PROP] as string; }
            set { this[TYPE_PROP] = value; }
        }

        [ConfigurationProperty(VALUE_PROP)]
        public string Value
        {
            get { return this[VALUE_PROP] as string; }
            set { this[VALUE_PROP] = value; }
        }
    }
}
