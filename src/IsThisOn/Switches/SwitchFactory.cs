using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    public sealed class SwitchFactory
    {
        public static ISwitch CreateSwitch(string name, string switchType, object refValue)
        {
            var typeObj = Type.GetType(switchType);
            return CreateSwitch(name, typeObj, refValue);
        }

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
