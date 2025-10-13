using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;
using System.Collections.ObjectModel;

namespace UsbBluetooth.LibUsbHelpers
{
    internal static class UsbDeviceHelpers
    {
        internal static ReadOnlyCollection<UsbEndpointInfo> FindUsbEndpoints(this UsbDevice device, EndpointType epType, EndpointDirection epDirection)
        {
            return device.Configs.SelectMany(config => config.FindUsbEndpoints(epType, epDirection)).ToList().AsReadOnly();
        }

        internal static UsbEndpointReader OpenEndpointReader(this UsbDevice device, UsbEndpointInfo epInfo)
        {
            return device.OpenEndpointReader((ReadEndpointID)epInfo.EndpointAddress);
        }

        internal static UsbEndpointWriter OpenEndpointWriter(this UsbDevice device, UsbEndpointInfo epInfo)
        {
            return device.OpenEndpointWriter((WriteEndpointID)epInfo.EndpointAddress);
        }
    }
}
