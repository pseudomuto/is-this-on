using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    public class SwitchBoardConfig : ConfigurationSection
    {
        private const string SECTION_NAME = "switchboard";
        private const string PROVIDER_PROP = "provider";
        private const string PROVIDER_DATA_PROP = "providerData";
        private const string SWITCHES_PROP = "features";

        private static Lazy<SwitchBoardConfig> _instance = new Lazy<SwitchBoardConfig>(() =>
        {
            return ConfigurationManager.GetSection(SECTION_NAME) as SwitchBoardConfig;
        });

        public static SwitchBoardConfig Instance { get { return _instance.Value;  } }

        [ConfigurationProperty(PROVIDER_PROP)]
        public string Provider
        {
            get { return this[PROVIDER_PROP] as string; }
            set { this[PROVIDER_PROP] = value; }
        }

        [ConfigurationProperty(PROVIDER_DATA_PROP)]
        public string ProviderData
        {
            get { return this[PROVIDER_DATA_PROP] as string; }
            set { this[PROVIDER_DATA_PROP] = value; }
        }

        [ConfigurationProperty(SWITCHES_PROP)]
        public SwitchConfigCollection Switches
        {
            get { return this[SWITCHES_PROP] as SwitchConfigCollection; }
            set { this[SWITCHES_PROP] = value; }
        }
    }
}
