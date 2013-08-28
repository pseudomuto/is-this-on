using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    public interface ISwitchProvider
    {
        IEnumerable<ISwitch> GetSwitches();
    }
}
