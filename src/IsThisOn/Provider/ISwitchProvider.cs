using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// An interface for classes that will read in configuration and supply 
    /// <see cref="ISwitch"/> objects to <see cref="SwitchBoard"/>
    /// </summary>
    public interface ISwitchProvider
    {
        /// <summary>
        /// Loads and creates <see cref="ISwitch"/> objects
        /// </summary>
        IEnumerable<ISwitch> GetSwitches();
    }
}
