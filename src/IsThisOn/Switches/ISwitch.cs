using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    public interface ISwitch
    {
        string Name { get; set; }

        void SetRefValue(object value);
        bool IsActive();                
    }
}
