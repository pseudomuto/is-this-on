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
            });

        private static IEnumerable<ISwitch> switches;

        public static int SwitchCount
        {
            get { return switches.Count(); }
        }

        public static void InitializeSwitches()
        {
            switches = provider.Value.GetSwitches();
        }

        public static bool IsOn(string name)
        {
            var thisSwitch = switches.FirstOrDefault(s => s.Name.Equals(
                    name, 
                    StringComparison.OrdinalIgnoreCase
                ));

            return thisSwitch == null ? 
                false : 
                thisSwitch.IsActive();
        }        
    }
}
