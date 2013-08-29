using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Should.Fluent;

namespace IsThisOn.Tests.Switches
{
    public class PercentageSwitch
    {
        private IsThisOn.PercentageSwitch _subject = new IsThisOn.PercentageSwitch();

        [Fact]
        public void IsNeverTrueWhenRefValueIsZero()
        {
            for(int i=0;i<1000;i++)
            {
                this._subject.IsActive()
                    .Should().Not.Be.True();
            }
        }

        [Fact]
        public void IsAlwaysTrueWhenRefValueIsOneHundred()
        {
            this._subject.SetRefValue(100);

            for (int i = 0; i < 1000; i++)
            {
                this._subject.IsActive()
                    .Should().Be.True();
            }
        }

        [Fact]
        public void CanBeTrueWhenRefValueIsInTheMiddle()
        {
            this._subject.SetRefValue(50);
            var active = false;

            // 1/100K should be true at least once 
            // with a 50/50 right?
            for (int i = 0; i < 100000; i++)
            {
                active = this._subject.IsActive();
                if (active) break;
            }

            active.Should().Be.True();
        }
    }
}
