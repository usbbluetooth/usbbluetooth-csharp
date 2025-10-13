using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;
using System.Collections.ObjectModel;

namespace UsbBluetooth.LibUsbHelpers
{
    internal static class UsbInterfaceInfoHelpers
    {
        internal static ReadOnlyCollection<UsbEndpointInfo> FindUsbEndpoints(this UsbInterfaceInfo ifaceInfo, EndpointType epType, EndpointDirection epDirection)
        {
            return ifaceInfo.Endpoints.Where(epInfo => epInfo.GetEndpointType() == epType && epInfo.GetEndpointDirection() == epDirection).ToList().AsReadOnly();
        }
    }
}
