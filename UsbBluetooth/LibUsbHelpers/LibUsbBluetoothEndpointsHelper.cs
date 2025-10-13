using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;

namespace UsbBluetooth.LibUsbHelpers
{
    internal static class LibUsbBluetoothEndpointsHelper
    {
        internal static UsbEndpointReader OpenBluetoothAclEndpointReader(this UsbDevice dev)
        {
            UsbEndpointInfo epInfo = dev.FindUsbEndpoints(EndpointType.Bulk, EndpointDirection.In).First();
            return dev.OpenEndpointReader(epInfo);
        }

        internal static UsbEndpointWriter OpenBluetoothAclEndpointWriter(this UsbDevice dev)
        {
            UsbEndpointInfo epInfo = dev.FindUsbEndpoints(EndpointType.Bulk, EndpointDirection.Out).First();
            return dev.OpenEndpointWriter(epInfo);
        }

        internal static UsbEndpointReader OpenBluetoothEventEndpointReader(this UsbDevice dev)
        {
            UsbEndpointInfo epInfo = dev.FindUsbEndpoints(EndpointType.Interrupt, EndpointDirection.In).First();
            return dev.OpenEndpointReader(epInfo);
        }

        internal static int WriteBluetoothCommandControlTransfer(this IUsbDevice dev, UsbInterfaceInfo interfaceInfo, byte[] bytes)
        {
            if (bytes == null)
                return 0;
            UsbSetupPacket usbSetupPacket = new UsbSetupPacket((byte)(UsbCtrlFlags.Direction_Out | UsbCtrlFlags.RequestType_Class | UsbCtrlFlags.Recipient_Interface), 0, 0, interfaceInfo.Number, bytes.Length);
            return dev.ControlTransfer(usbSetupPacket, bytes, 0, bytes.Length);
        }

        internal static int WriteBluetoothCommandControlTransfer(this IUsbDevice dev, UsbInterfaceInfo interfaceInfo, Span<byte> bytes)
        {
            return dev.WriteBluetoothCommandControlTransfer(interfaceInfo, bytes.ToArray());
        }

        internal static UsbInterfaceInfo FindBluetoothInterface(this UsbConfigInfo configInfo)
        {
            return configInfo.Interfaces.Where(iface => iface.IsBluetooth()).First();
        }
    }
}
