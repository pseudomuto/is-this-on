using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// A factory class that creates <see cref="ISwitch"/> objects
    /// </summary>
    public sealed class SwitchFactory
    {
        /// <summary>
        /// Creates an <see cref="ISwitch"/>
        /// </summary>
        /// <param name="name">The name of the switch</param>
        /// <param name="switchType">The full-qualified type name of the switch to create</param>
        /// <param name="refValue">The reference value</param>
        /// <returns>A new <see cref="ISwitch"/> object</returns>
        public static ISwitch CreateSwitch(string name, string switchType, object refValue)
        {
            var typeObj = Type.GetType(switchType);
            return CreateSwitch(name, typeObj, refValue);
        }

        /// <summary>
        /// Creates an <see cref="ISwitch"/>
        /// </summary>
        /// <param name="name">The name of the switch</param>
        /// <param name="switchType">The <see cref="Type"/> of switch to create</param>
        /// <param name="refValue">The reference value</param>
        /// <returns>A new <see cref="ISwitch"/> object</returns>
        public static ISwitch CreateSwitch(string name, Type switchType, object refValue)
        {
            var featureSwitch = Activator.CreateInstance(switchType) as ISwitch;
            featureSwitch.Name = name;

            if (refValue != null)
            {
                featureSwitch.SetRefValue(refValue);
            }

            return featureSwitch;
        }
    }
}
