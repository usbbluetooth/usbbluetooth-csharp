using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.LibUsb;

namespace UsbBluetooth.LibUsbHelpers
{
    internal static class LibUsbIsBluetoothHelper
    {
        internal static bool IsBluetooth(this IUsbDevice usbDevice)
        {
            // Check the descriptor...
            if (usbDevice.Info.IsBluetooth())
                return true;
            // If the descriptor is generic, we should check the interfaces...
            foreach (UsbConfigInfo config in usbDevice.Configs)
                foreach (UsbInterfaceInfo iface in config.Interfaces)
                    if (iface.IsBluetooth())
                        return true;
            // Not a Bluetooth device...
            return false;
        }

        internal static bool IsBluetooth(this UsbDeviceInfo devInfo)
        {
            return devInfo.DeviceClass == 0xE0 && devInfo.DeviceSubClass == 0x01 && devInfo.DeviceProtocol == 0x01;
        }

        internal static bool IsBluetooth(this UsbInterfaceInfo iface)
        {
            return iface.Class == ClassCode.Wireless && iface.SubClass == 0x01 && iface.Protocol == 0x01;
        }
    }
}
