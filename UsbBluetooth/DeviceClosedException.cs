namespace UsbBluetooth
{
    internal class DeviceClosedException : Exception
    {
        public DeviceClosedException() : base("You are attempting to write or read from a device that has not been opened.")
        {
        }
    }
}
