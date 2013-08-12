using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Should.Core;
using Should.Fluent;

namespace IsThisOn.Tests
{
    public class Feature
    {
        public class Constructor
        {
            public class WhenGivenOnlyName
            {
                private IsThisOn.Feature _subject = new IsThisOn.Feature("My Feature");

                [Fact]
                public void ValidatesNameIsNotNullOrEmpty()
                {
                    Assert.Throws<ArgumentNullException>(() =>
                    {
                        new IsThisOn.Feature("");
                    });
                }

                [Fact]
                public void StoresLowercasedName()
                {
                    this._subject.Name.Should().Equal("my feature");
                }

                [Fact]
                public void DefaultsIsActiveToTrue()
                {
                    this._subject.IsActive.Should().Be.True();
                }
            }
        }

        public class IsActive
        {
            public class WhenInitializedWithBoolean
            {
                private IsThisOn.Feature _subject = new IsThisOn.Feature("Some Feature", false);

                [Fact]
                public void IsPreDetermined()
                {
                    this._subject.IsActive.Should().Not.Be.True();
                }
            }
        }
    }
}
