using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    public class BoolSwitch : ISwitch
    {
        private bool _isActive;

        public string Name { get; set; }

        public void SetRefValue(object value)
        {
            if (value != null)
            {
                this._isActive = value.ToString().Equals(
                        "true", 
                        StringComparison.OrdinalIgnoreCase
                    );
            }
        }

        public bool IsActive()
        {
            return this._isActive;
        }
    }
}
