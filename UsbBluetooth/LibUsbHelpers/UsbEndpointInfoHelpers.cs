using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;

namespace UsbBluetooth.LibUsbHelpers
{
    internal static class UsbEndpointInfoHelpers
    {
        internal static EndpointType GetEndpointType(this UsbEndpointInfo epInfo)
        {
            return (EndpointType)(epInfo.Attributes & 0x03);
        }

        internal static EndpointDirection GetEndpointDirection(this UsbEndpointInfo epInfo)
        {
            return (EndpointDirection)(epInfo.EndpointAddress & 0x80);
        }
    }
}
