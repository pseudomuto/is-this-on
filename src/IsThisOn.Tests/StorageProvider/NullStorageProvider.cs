using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Should.Fluent;

namespace IsThisOn.Tests.StorageProvider
{
    public class NullStorageProvider
    {
        public class GetState
        {
            [Fact]
            public void AlwaysReturnsNull()
            {
                var provider = new IsThisOn.NullStorageProvider();

                provider.GetState("notfound")
                    .Should().Be.Null();

                provider.GetState("BoolSwitch")
                    .Should().Be.Null();
            }
        }
    }
}
