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
        public abstract class BaseTest
        {
            protected BaseTest()
            {
                IsThisOn.SwitchBoard.InitializeSwitches();
            }
        }

        public class InitializeSwitches : BaseTest
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
            public class WhenSwitchExists : BaseTest
            {
                [Fact]
                public void EvaluatesSwitch()
                {
                    IsThisOn.SwitchBoard.IsOn("BoolSwitch")
                        .Should().Be.True();
                }
            }

            public class WhenSwitchDoesntExist : BaseTest
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
