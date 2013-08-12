using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsThisOn
{
    public class Feature
    {
        public string Name { get; private set; }

        public bool IsActive { get; private set; }

        public Feature(string name, bool isActive = true)
        {
            Guard.AgainstNullOrEmpty("name", name);

            this.Name = name.ToLower();
            this.IsActive = isActive;
        }
    }
}
