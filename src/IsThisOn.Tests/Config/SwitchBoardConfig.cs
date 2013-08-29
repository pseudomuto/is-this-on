using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Should.Fluent;
using Xunit;

namespace IsThisOn.Tests.Config
{
    public class SwitchBoardConfig
    {
        public class Instance
        {
            private IsThisOn.SwitchBoardConfig _subject = IsThisOn.SwitchBoardConfig.Instance;

            [Fact]
            public void LoadsProviderFromConfig()
            {
                this._subject.Provider
                    .Should().Equal("IsThisOn.ConfigSwitchProvider, IsThisOn");
            }

            [Fact]
            public void LoadsProviderDataConfig()
            {
                this._subject.ProviderData
                    .Should().Equal("SomeProviderData");
            }

            [Fact]
            public void LoadsSwitchesFromConfig()
            {
                this._subject.Switches.Count
                    .Should().Be.GreaterThan(0);
            }
        }
    }
}
