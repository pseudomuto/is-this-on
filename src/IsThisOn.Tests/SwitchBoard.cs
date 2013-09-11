using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Should.Fluent;
using Moq;

namespace IsThisOn.Tests
{
    public class SwitchBoard
    {
        public class ReloadSwitches
        {
            public ReloadSwitches()
            {
                IsThisOn.SwitchBoard.SwitchCount
                    .Should().Be.GreaterThan(0);
            }

            [Fact]
            public void NotifiesStorageProvider()
            {
                var mock = new Mock<IStorageProvider>(MockBehavior.Strict);
                mock.Setup(m => m.ClearState()).Verifiable();

                IsThisOn.SwitchBoard.SetStorageProvider(mock.Object);                
                IsThisOn.SwitchBoard.ReloadSwitches();
                mock.Verify();
            }
        }

        public class IsOn
        {
            [Fact]
            public void ChecksStorageProviderForState()
            {
                var mock = new Mock<IStorageProvider>();
                mock.Setup(m => m.GetState("BoolSwitch"))
                    .Returns(() => null)
                    .Verifiable();

                IsThisOn.SwitchBoard.SetStorageProvider(mock.Object);
                IsThisOn.SwitchBoard.IsOn("BoolSwitch");

                mock.Verify();
            }

            public class WhenSwitchExists
            {
                [Fact]
                public void EvaluatesSwitch()
                {
                    IsThisOn.SwitchBoard.SetStorageProvider(null);

                    IsThisOn.SwitchBoard.IsOn("BoolSwitch")
                        .Should().Be.True();
                }

                public class AndCacheDurationIsNone
                {
                    [Fact]
                    public void CallsIsActiveEachTime()
                    {
                        var mock = new Mock<IStorageProvider>(MockBehavior.Strict);                        
                        IsThisOn.SwitchBoard.SetStorageProvider(mock.Object);

                        IsThisOn.SwitchBoard.IsOn("BoolSwitchNoCache");
                        IsThisOn.SwitchBoard.IsOn("BoolSwitchNoCache");

                        mock.Verify();
                    }
                }

                public class AndCacheDurationIsNotNone
                {
                    [Fact]
                    public void StoresResultForFutureCalls()
                    {
                        var mock = new Mock<IStorageProvider>();
                        mock.Setup(m => m.GetState("BoolSwitch"))
                            .Returns(() => null)
                            .Verifiable();

                        mock.Setup(m => m.StoreState("BoolSwitch", true, StorageDuration.Long))
                            .Verifiable();

                        IsThisOn.SwitchBoard.SetStorageProvider(mock.Object);
                        
                        // this call should execute store state
                        IsThisOn.SwitchBoard.IsOn("BoolSwitch")
                            .Should().Be.True();

                        // this one should just return the value
                        IsThisOn.SwitchBoard.IsOn("BoolSwitch")
                            .Should().Be.True();

                        mock.Verify();
                    }
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

                [Fact]
                public void DoesNotLookupOrStoreResult()
                {
                    var mock = new Mock<IStorageProvider>(MockBehavior.Strict);                    
                    IsThisOn.SwitchBoard.SetStorageProvider(mock.Object);

                    IsThisOn.SwitchBoard.IsOn("NotFound");

                    // only setup methods should be called
                    mock.Verify();
                }
            }
        }
    }
}
