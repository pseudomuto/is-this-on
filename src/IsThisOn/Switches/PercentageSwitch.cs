using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// A switch that is active for <code>n</code> percent of the time
    /// </summary>
    public class PercentageSwitch : ISwitch
    {
        private double _chance;
        private Random _random = new Random();

        /// <summary>
        /// The name of the switch
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Set the percentage value
        /// </summary>
        /// <param name="value">
        /// A number in the range [0,100] indicating the chance of being active
        /// </param>
        public void SetRefValue(object value)
        {
            if (value == null) return;

            var typeConverter = TypeDescriptor.GetConverter(typeof(double));
            this._chance = (double)typeConverter.ConvertFromString(value.ToString());
        }

        /// <summary>
        /// Gets a value inidicating if this switch is active
        /// </summary>
        /// <returns>True for a percentage of times this is called</returns>
        public bool IsActive()
        {
            var num = this._random.NextDouble() * 100.0;
            return num < this._chance;
        }
    }
}
