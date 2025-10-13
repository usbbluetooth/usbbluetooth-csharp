using LibUsbDotNet.LibUsb;

namespace UsbBluetooth.LibUsbHelpers
{
    internal static class LibUsbHasDriverHelper
    {
        internal static bool HasDriver(this IUsbDevice dev)
        {
            bool ok = dev.TryOpen();
            dev.Close();
            return ok;
        }
    }
}
