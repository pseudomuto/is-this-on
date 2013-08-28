using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    public class ConfigSwitchProvider : ISwitchProvider
    {
        public IEnumerable<ISwitch> GetSwitches()
        {
            for (int i = 0; i < SwitchBoardConfig.Instance.Switches.Count; i++)
            {
                var currentSwitch = SwitchBoardConfig.Instance.Switches[i];

                yield return SwitchFactory.CreateSwitch(
                    currentSwitch.Name,
                    currentSwitch.Type,
                    currentSwitch.Value
                );
            }
        }
    }
}
