using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    public class SwitchBoard
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

        public static int SwitchCount
        {
            get { return switches.Value.Count(); }
        }

        public static void ReloadSwitches()
        {
            switches = new Lazy<IEnumerable<ISwitch>>(() =>
            {
                return provider.Value.GetSwitches();
            }, true);
        }

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
