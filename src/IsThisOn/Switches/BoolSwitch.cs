using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// A switch that is on or off based on a boolean reference value
    /// </summary>
    public class BoolSwitch : ISwitch
    {
        private bool _isActive;

        /// <summary>
        /// The length of time to cache the result for
        /// </summary>
        public StorageDuration CacheDuration { get; set; }

        /// <summary>
        /// The name of the switch
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sets the active flag
        /// </summary>
        /// <param name="value">Whether or not the switch is active (string or bool)</param>
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

        /// <summary>
        /// Gets a value indicating whether or not this switch is active
        /// </summary>
        /// <returns>True if active, otherwise false</returns>
        public bool IsActive()
        {
            return this._isActive;
        }
    }
}
