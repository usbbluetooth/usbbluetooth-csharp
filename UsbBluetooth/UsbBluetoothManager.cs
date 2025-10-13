using System.Collections.ObjectModel;
using UsbBluetooth.LibUsbHelpers;

namespace UsbBluetooth
{
    public static class UsbBluetoothManager
    {
        public static ReadOnlyCollection<Controller> ListControllers()
        {
            return LibUsbContext.UsbContextInstance.List().Where(device => device.IsBluetooth()).Select(device => new Controller(device)).ToList().AsReadOnly();
        }

        public static ReadOnlyCollection<Controller> ListControllersWithDriver()
        {
            return LibUsbContext.UsbContextInstance.List().Where(device => device.IsBluetooth() && device.HasDriver()).Select(device => new Controller(device)).ToList().AsReadOnly();
        }
    }
}
