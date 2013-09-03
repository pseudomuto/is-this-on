using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// A class the manages the set of feature switches and queries their status
    /// </summary>
    public sealed class SwitchBoard
    {
        private static Lazy<ConfigSwitchProvider> provider =
            new Lazy<ConfigSwitchProvider>(() =>
            {
                var type = Type.GetType(SwitchBoardConfig.Instance.Provider);
                return Activator.CreateInstance(type) as ConfigSwitchProvider;
            }, true);

        private static Lazy<IEnumerable<ISwitch>> switches =
            new Lazy<IEnumerable<ISwitch>>(() =>
            {
                return provider.Value.GetSwitches();
            }, true);

        /// <summary>
        /// Gets the number of switches defined
        /// </summary>
        public static int SwitchCount
        {
            get { return switches.Value.Count(); }
        }

        /// <summary>
        /// Reloads the switches from the configured <see cref="ISwitchProvider"/>
        /// </summary>
        public static void ReloadSwitches()
        {
            switches = new Lazy<IEnumerable<ISwitch>>(() =>
            {
                return provider.Value.GetSwitches();
            }, true);
        }

        /// <summary>
        /// Gets a valid indicating whether a switch is on
        /// </summary>
        /// <param name="name">The name of the switch</param>
        /// <returns>True is the feature is found and on, otherwise false</returns>
        public static bool IsOn(string name)
        {
            var thisSwitch = switches.Value.FirstOrDefault(s => s.Name.Equals(
                    name, 
                    StringComparison.OrdinalIgnoreCase
                ));

            return thisSwitch == null ? 
                false : 
                thisSwitch.IsActive();
        }        
    }
}
