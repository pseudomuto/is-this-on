using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Should.Fluent;
using Moq;
using System.Web;

namespace IsThisOn.Tests.StorageProvider
{
    public class CookieStorageProvider
    {
        private HttpCookieCollection _defaultCookies = new HttpCookieCollection();
        private HttpCookieCollection _outputCookies = new HttpCookieCollection();
        private IsThisOn.CookieStorageProvider _subject;

        public CookieStorageProvider()
        {
            var mock = new Mock<IsThisOn.CookieStorageProvider>();
            mock.CallBase = true;
            mock.SetupGet(m => m.IncomingCookies).Returns(_defaultCookies);
            mock.SetupGet(m => m.OutgoingCookies).Returns(_outputCookies);

            this._subject = mock.Object;
        }

        public class StoreState : CookieStorageProvider
        {
            [Fact]
            public void SetsCookieValue()
            {
                this._subject.StoreState("test value", true, StorageDuration.Long);

                var result = this._outputCookies["itofs_test_value"];

                result.Should().Not.Be.Null();
                result.Value.Should().Equal(true.ToString());
            }

            [Fact]
            public void OverwritesExistingValue()
            {
                this._defaultCookies.Add(new HttpCookie("itofs_my_cookie", true.ToString()));
                this._subject.StoreState("MY COOKIE", false, StorageDuration.Long);

                this._subject.GetState("my cookIe")
                    .Should().Not.Be.True();
            }
        }

        public class GetState : CookieStorageProvider
        {
            public GetState()
            {
                this._defaultCookies.Add(new HttpCookie("itofs_my_cookie", true.ToString()));
                this._defaultCookies.Add(new HttpCookie("itofs_false_cookie", false.ToString()));
            }

            [Fact]
            public void ReturnsNullForNotFound()
            {
                this._subject.GetState("NotFound")
                    .Should().Be.Null();
            }

            [Fact]
            public void GetsCookieValue()
            {
                this._subject.GetState("My Cookie")
                    .Should().Be.True();
            }

            [Fact]
            public void GetsCookieValueEvenWhenFalse()
            {
                this._subject.GetState("FALSE COOKIE")
                    .Should().Not.Be.True();
            }
        }

        public class RemoveState : CookieStorageProvider
        {
            [Fact]
            public void RemovesStoredValue()
            {
                this._defaultCookies.Add(new HttpCookie("itofs_test", true.ToString()));
                this._subject.StoreState("test", false, StorageDuration.Long);

                this._subject.RemoveState("test");

                this._subject.GetState("test")
                    .Should().Be.Null();
            }
        }

        public class ClearState : CookieStorageProvider
        {
            public ClearState()
            {
                this._defaultCookies.Add(new HttpCookie("itofs_my_cookie", true.ToString()));
                this._defaultCookies.Add(new HttpCookie("itofs_false_cookie", false.ToString()));
            }

            [Fact]
            public void RemovesStatesForAllSwitches()
            {
                this._subject.StoreState("some value", false, StorageDuration.Long);

                this._subject.ClearState();

                this._defaultCookies.Count.Should().Equal(0);
                this._outputCookies.Count.Should().Equal(0);
            }

            [Fact]
            public void OnlyClearsSwitchStates()
            {
                this._defaultCookies.Add(new HttpCookie("some_cookie", true.ToString()));

                this._subject.ClearState();

                this._defaultCookies.Count.Should().Equal(1);
                this._outputCookies.Count.Should().Equal(0);
            }
        }
    }
}
