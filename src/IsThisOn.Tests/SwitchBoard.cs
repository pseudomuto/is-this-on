using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Should.Fluent;

namespace IsThisOn.Tests
{
    public class SwitchBoard
    {
        public class ReloadSwitches
        {
            [Fact]
            public void LoadsSwitchesFromProvider()
            {
                IsThisOn.SwitchBoard.SwitchCount
                    .Should().Be.GreaterThan(0);
            }
        }

        public class IsOn
        {
            public class WhenSwitchExists
            {
                [Fact]
                public void EvaluatesSwitch()
                {
                    IsThisOn.SwitchBoard.IsOn("BoolSwitch")
                        .Should().Be.True();
                }
            }

            public class WhenSwitchDoesntExist
            {
                [Fact]
                public void DefaultsToFalse()
                {
                    IsThisOn.SwitchBoard.IsOn("NotFound")
                        .Should().Not.Be.True();
                }
            }
        }
    }
}
