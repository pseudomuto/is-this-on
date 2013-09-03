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
                private ISwitch _subject = IsThisOn.SwitchFactory.CreateSwitch(
                            "testSwitch",
                            "IsThisOn.BoolSwitch, IsThisOn",
                            true
                        );

                [Fact]
                public void CreatesSwitch()
                {
                    this._subject.Should().Not.Be.Null();
                }

                [Fact]
                public void SetsCacheDuration()
                {
                    this._subject.CacheDuration
                        .Should().Equal(StorageDuration.None);
                }
            }

            public class WhenCreatedWithAType
            {
                private ISwitch _subject = IsThisOn.SwitchFactory.CreateSwitch(
                            "testSwitch",
                            typeof(IsThisOn.BoolSwitch),
                            true,
                            StorageDuration.Long
                        );

                [Fact]
                public void CreatesSwitch()
                {
                    this._subject.Should().Not.Be.Null();                    
                }

                [Fact]
                public void SetsCacheDuration()
                {
                    this._subject.CacheDuration
                        .Should().Equal(StorageDuration.Long);
                }
            }
        }
    }
}
