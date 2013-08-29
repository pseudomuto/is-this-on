using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Should.Fluent;

namespace IsThisOn.Tests.Switches
{
    public class SwitchFactory
    {
        public class CreateSwitch
        {
            public class WhenCreatedWithATypeName
            {
                [Fact]
                public void CreatesSwitch()
                {
                    var featureSwitch = IsThisOn.SwitchFactory.CreateSwitch(
                            "testSwitch",
                            "IsThisOn.BoolSwitch, IsThisOn",
                            true
                        );

                    featureSwitch.Should().Not.Be.Null();
                }
            }

            public class WhenCreatedWithAType
            {
                [Fact]
                public void CreatesSwitch()
                {
                    var featureSwitch = IsThisOn.SwitchFactory.CreateSwitch(
                            "testSwitch",
                            typeof(IsThisOn.BoolSwitch), 
                            true
                        );

                    featureSwitch.Should().Not.Be.Null();                    
                }
            }
        }
    }
}
