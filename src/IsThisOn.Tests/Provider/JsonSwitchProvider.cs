using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

using Should.Fluent;

namespace IsThisOn.Tests.Provider
{
    public class JsonSwitchProvider
    {
        public class GetSwitches
        {
            private IsThisOn.JsonSwitchProvider _subject;

            public GetSwitches()
            {
                var mock = new Mock<IsThisOn.JsonSwitchProvider>();
                mock.CallBase = true;
                mock.Setup(m => m.GetUri()).Returns(new Uri("http://example.com/switches.json"));
                mock.Setup(m => m.DownloadJson(It.IsAny<Uri>()))
                    .Returns(() =>
                    {
                        var sb = new StringBuilder();
                        sb.Append("{\"features\": [");
                        sb.Append("{ \"name\": \"BoolSwitch\", \"type\": \"IsThisOn.BoolSwitch\", \"value\": \"true\" },");
                        sb.Append("{ \"name\": \"PercentageSwitch\", \"type\": \"IsThisOn.PercentageSwitch\", \"value\": \"55.23\" }");
                        sb.Append("]}");

                        return sb.ToString();
                    });

                this._subject = mock.Object;
            }

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
        }
    }
}
