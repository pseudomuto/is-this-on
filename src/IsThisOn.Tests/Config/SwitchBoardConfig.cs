﻿using System;
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
                    .Should().Not.Be.NullOrEmpty();
            }

            [Fact]
            public void LoadsStorageProviderFromConfig()
            {
                this._subject.StorageProvider
                    .Should().Not.Be.NullOrEmpty();
            }

            [Fact]
            public void LoadsProviderDataConfig()
            {
                this._subject.ProviderData
                    .Should().Not.Be.NullOrEmpty();
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
