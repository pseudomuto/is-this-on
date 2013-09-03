using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// An interface that defines a feature switch
    /// </summary>
    public interface ISwitch
    {
        /// <summary>
        /// The length of time to cache the result for
        /// </summary>
        StorageDuration CacheDuration { get; set; }

        /// <summary>
        /// The name of the switch
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Sets a reference values for a switch
        /// </summary>
        /// <remarks>This is optional and switch-specific</remarks>
        /// <param name="value">The value</param>
        void SetRefValue(object value);

        /// <summary>
        /// Gets a value indicating the status of this switch
        /// </summary>
        /// <returns>True if this switch is on, otherwise false</returns>
        bool IsActive();                
    }
}
