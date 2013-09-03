using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// The main configuration section for IsThisOn
    /// </summary>
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

        /// <summary>
        /// The one and only config instance
        /// </summary>
        public static SwitchBoardConfig Instance { get { return _instance.Value;  } }

        /// <summary>
        /// The full type name of the <see cref="ISwitchProvider"/> to be used
        /// </summary>
        [ConfigurationProperty(PROVIDER_PROP)]
        public string Provider
        {
            get { return this[PROVIDER_PROP] as string; }
            set { this[PROVIDER_PROP] = value; }
        }

        /// <summary>
        /// An optional string to be used by <see cref="Provider"/>
        /// </summary>
        [ConfigurationProperty(PROVIDER_DATA_PROP)]
        public string ProviderData
        {
            get { return this[PROVIDER_DATA_PROP] as string; }
            set { this[PROVIDER_DATA_PROP] = value; }
        }

        /// <summary>
        /// The switch definitions to be used if using <see cref="ConfigSwitchProvider"/>
        /// </summary>
        [ConfigurationProperty(SWITCHES_PROP)]
        public SwitchConfigCollection Switches
        {
            get { return this[SWITCHES_PROP] as SwitchConfigCollection; }
            set { this[SWITCHES_PROP] = value; }
        }
    }
}
