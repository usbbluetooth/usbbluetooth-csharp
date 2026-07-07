# USB Bluetooth

[![Build](https://github.com/usbbluetooth/usbbluetooth-csharp/actions/workflows/build.yml/badge.svg)](https://github.com/usbbluetooth/usbbluetooth-csharp/actions/workflows/build.yml)
[![CodeQL](https://github.com/usbbluetooth/usbbluetooth-csharp/actions/workflows/codeql.yml/badge.svg)](https://github.com/usbbluetooth/usbbluetooth-csharp/actions/workflows/codeql.yml)
[![NuGet](https://img.shields.io/nuget/v/UsbBluetooth)](https://www.nuget.org/packages/UsbBluetooth/)
[![Snyk package health](https://img.shields.io/badge/Snyk-package%20health-4C4A73?logo=snyk&logoColor=white)](https://security.snyk.io/package/nuget/UsbBluetooth)
[![OpenSSF Scorecard](https://api.scorecard.dev/projects/github.com/usbbluetooth/usbbluetooth-csharp/badge)](https://scorecard.dev/viewer/?uri=github.com/usbbluetooth/usbbluetooth-csharp)

Take full control of your USB Bluetooth controllers from C#!

For general documentation about the project, please visit [usbbluetooth.github.io](https://usbbluetooth.github.io).

For a C version of this library, check out [UsbBluetooth for C](https://github.com/usbbluetooth/usbbluetooth).

For a Python version of this library, check out [UsbBluetooth for Python](https://github.com/usbbluetooth/usbbluetooth-python) or [Scapy UsbBluetooth](https://github.com/usbbluetooth/scapy-usbbluetooth) for direct integration with Scapy.

## Installation

Use the [NuGet package](https://www.nuget.org/packages/UsbBluetooth/)!

## Usage

Once installed, you may list devices using `UsbBluetoothManager.ListControllersWithDriver()`, and for each device you may `Open()` the device, `Write()` and `Read()` to them and `Close()` it once you are done.
See the [Examples](Examples/) folder for some sample code.

## Plaform quirks

This package has some requirements to work because of different platform particularities.
To make sure the package works, please, see <https://usbbluetooth.github.io/quirks/>

## History

Package versions `< 0.1` were bindings around the original C library. The history for those packages can be found in the [usbbluetooth repository](https://github.com/usbbluetooth/usbbluetooth) in the respective tags.

Package versions `>= 0.1` are developed in this repo and based of `LibUsbDotNet`, the C# LibUSB binding.
