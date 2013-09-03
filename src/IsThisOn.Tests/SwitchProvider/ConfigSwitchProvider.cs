using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

using Should.Fluent;

namespace IsThisOn.Tests.SwitchProvider
{
    public class ConfigSwitchProvider
    {
        public class GetSwitches
        {
            private IsThisOn.ConfigSwitchProvider _subject =
                new IsThisOn.ConfigSwitchProvider();

            [Fact]
            public void LoadsSwitchesFromJson()
            {
                this._subject.GetSwitches().Count()
                    .Should().Be.GreaterThan(0);
            }

            [Fact]
            public void SetsRefValueCorrectly()
            {
                var sw = this._subject.GetSwitches().First(
                    s => s.Name.Equals("boolswitch", StringComparison.OrdinalIgnoreCase)
                );

                sw.IsActive().Should().Be.True();
            }

            [Fact]
            public void SetsCacheDurationValueCorrectly()
            {
                var sw = this._subject.GetSwitches().First(
                    s => s.Name.Equals("boolswitch", StringComparison.OrdinalIgnoreCase)
                );

                sw.CacheDuration.Should().Equal(StorageDuration.Long);
            }
        }
    }
}
