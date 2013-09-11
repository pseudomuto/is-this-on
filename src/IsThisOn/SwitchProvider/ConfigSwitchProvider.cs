using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// An <see cref="ISwitchProvider"/> that reads in switch configuration from app config
    /// </summary>
    public class ConfigSwitchProvider : ISwitchProvider
    {
        /// <summary>
        /// Loads switches from app config
        /// </summary>
        /// <returns><see cref="ISwitch"/> objects for each defined switch</returns>
        public IEnumerable<ISwitch> GetSwitches()
        {
            for (int i = 0; i < SwitchBoardConfig.Instance.Switches.Count; i++)
            {
                var currentSwitch = SwitchBoardConfig.Instance.Switches[i];
                
                yield return SwitchFactory.CreateSwitch(
                    currentSwitch.Name,
                    currentSwitch.Type,
                    currentSwitch.Value,
                    currentSwitch.CacheDuration
                );
            }
        }
    }
}
