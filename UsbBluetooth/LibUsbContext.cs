using LibUsbDotNet.LibUsb;

namespace UsbBluetooth
{
    internal class LibUsbContext
    {
        private static readonly Lazy<UsbContext> mUsbContextInstance = new(() => new UsbContext());

        internal static UsbContext UsbContextInstance
        {
            get
            {
                return mUsbContextInstance.Value;
            }
        }
    }
}
