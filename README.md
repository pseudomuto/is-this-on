# Is This On? 

[![Build Status](https://travis-ci.org/pseudomuto/is-this-on.png)](https://travis-ci.org/pseudomuto/is-this-on)

A Feature Flipper for .NET Applications.

## Overview

This project aims to allow developers to employ some continuous deployment practices easily by toggling features on/off based on boolean values, percentages, user role, etc.

The see what I mean by Continuous Delivery, check out [this presentation](http://prezi.com/5zm8xplapff2/continuous-deployment/)

## Usage

Setup the `SwitchBoard` in your web/app config file:

    <?xml version="1.0" encoding="utf-8" ?>
  <configuration>
    <configSections>
      <section 
        name="switchboard" 
        type="IsThisOn.SwitchBoardConfig, 
        IsThisOn" />
    </configSections>
    
    <switchboard provider="IsThisOn.ConfigSwitchProvider, IsThisOn">
      <features>
        <switch name="BoolSwitch" type="IsThisOn.BoolSwitch, IsThisOn" value="true" />
        <switch name="PercentageSwitch" type="IsThisOn.PercentageSwitch, IsThisOn" value="55.2" />
      </features>
    </switchboard>
  </configuration>

Then in your app, ask the SwitchBoard if a feature is on:

    if (SwitchBoard.IsOn("BoolSwitch")) {
        // do the new thing...
    } else {
        // do the old thing...
    }

Or, you could (maybe should?) do both things:

    if (SwitchBoard.IsOn("BoolSwitch")) {
        // do the new thing...
    }
    
    // do the old thing...

## Upcoming

1. Many more switch types

## Contributing

You know the drill...

1. Fork It
2. Change It
3. Test It
4. Pull It
