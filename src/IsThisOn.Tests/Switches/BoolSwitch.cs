using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Should.Fluent;

namespace IsThisOn.Tests.Switches
{
    public class BoolSwitch
    {
        private IsThisOn.BoolSwitch _subject = new IsThisOn.BoolSwitch();

        [Fact]
        public void DefaultsToFalse()
        {
            this._subject.IsActive()
                .Should().Not.Be.True();
        }

        [Fact]
        public void DependsOnRefValue()
        {
            this._subject.SetRefValue("tRue");
            this._subject.IsActive()
                .Should().Be.True();
        }
    }
}
