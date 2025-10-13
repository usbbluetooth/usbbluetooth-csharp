using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;
using System.Collections.ObjectModel;

namespace UsbBluetooth.LibUsbHelpers
{
    internal static class UsbConfigInfoHelpers
    {
        internal static ReadOnlyCollection<UsbEndpointInfo> FindUsbEndpoints(this UsbConfigInfo configInfo, EndpointType epType, EndpointDirection epDirection)
        {
            return configInfo.Interfaces.SelectMany(iface => iface.FindUsbEndpoints(epType, epDirection)).ToList().AsReadOnly();
        }
    }
}
