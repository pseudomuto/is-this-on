using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    public class PercentageSwitch : ISwitch
    {
        private double _chance;
        private Random _random = new Random();

        public string Name { get; set; }

        public void SetRefValue(object value)
        {
            if (value == null) return;

            var typeConverter = TypeDescriptor.GetConverter(typeof(double));
            this._chance = (double)typeConverter.ConvertFromString(value.ToString());
        }

        public bool IsActive()
        {
            var num = this._random.NextDouble() * 100.0;
            return num < this._chance;
        }
    }
}
