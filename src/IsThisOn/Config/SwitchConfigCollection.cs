using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    [ConfigurationCollection(typeof(SwitchConfig), AddItemName = "switch")]
    public class SwitchConfigCollection : ConfigurationElementCollection
    {
        public SwitchConfig this[int index]
        {
            get
            {
                return this.BaseGet(index) as SwitchConfig;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SwitchConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as SwitchConfig).Name;
        }        
    }
}
