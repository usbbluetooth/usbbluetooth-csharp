using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.LibUsb;
using UsbBluetooth.LibUsbHelpers;

namespace UsbBluetooth
{
    public class Controller
    {
        private readonly IUsbDevice mDevice;
        private UsbInterfaceInfo? mInterface = null;
        private UsbEndpointReader? mEventEndpointReader = null;
        private UsbEndpointWriter? mAclEndpointWriter = null;
        private UsbEndpointReader? mAclEndpointReader = null;

        public bool IsOpen { get; private set; } = false;

        public Controller(IUsbDevice dev)
        {
            mDevice = dev;
        }

        public int VendorId
        {
            get
            {
                return mDevice.VendorId;
            }
        }

        public int ProductId
        {
            get
            {
                return mDevice.ProductId;
            }
        }

        public void Open()
        {
            // Get an implementation instance, some methods are only in this class...
            UsbDevice usbDev = (UsbDevice)mDevice;

            // Try and open the device...
            if (!mDevice.TryOpen())
                return;

            // If the kernel supports it, detach the kernel driver...
            if (usbDev.SupportsDetachKernelDriver())
                usbDev.DetachKernelDriver(0);

            // Set device configuration
            int configNumber = mDevice.Configs.First().ConfigurationValue;
            mDevice.SetConfiguration(configNumber);

            // Find the Bluetooth interface...
            mInterface = usbDev.ActiveConfigDescriptor.FindBluetoothInterface();
            if (mInterface == null)
                throw new Exception("Could not find the Bluetooth interface in the USB device configuration.");

            // Claim the device interface
            mDevice.ClaimInterface(mInterface.Number);

            // Obtain the needed endpoint readers and writers...
            mEventEndpointReader = usbDev.OpenBluetoothEventEndpointReader();
            if (mEventEndpointReader == null)
                throw new Exception("Could not open an event enpoint reader in the USB device.");
            mAclEndpointReader = usbDev.OpenBluetoothAclEndpointReader();
            if (mAclEndpointReader == null)
                throw new Exception("Could not open an ACL enpoint reader in the USB device.");
            mAclEndpointWriter = usbDev.OpenBluetoothAclEndpointWriter();
            if (mAclEndpointWriter == null)
                throw new Exception("Could not open an ACL enpoint writer in the USB device.");

            // Update status
            IsOpen = true;
        }

        public void Close()
        {
            // Get an implementation instance, some methods are only in this class...
            UsbDevice usbDev = (UsbDevice)mDevice;

            // Release endpoint readers and writers...
            mEventEndpointReader = null;
            mAclEndpointReader = null;
            mAclEndpointWriter = null;

            // Release interfaces
            if (mInterface != null)
                mDevice.ReleaseInterface(mInterface.Number);

            // If the kernel supports it, reattach the kernel driver...
            if (usbDev.SupportsDetachKernelDriver())
                usbDev.AttachKernelDriver(0);

            // Close device
            mDevice.Close();

            // Update status
            IsOpen = false;
        }

        public int Write(byte[] packet)
        {
            if (!IsOpen)
                throw new DeviceClosedException();

            // Copy the packet data...
            Span<byte> data = packet.AsSpan().Slice(1);

            // Check the packet type...
            HciPacketType packetType = (HciPacketType)packet[0];
            switch (packetType)
            {
                case HciPacketType.Command:
                    return mDevice.WriteBluetoothCommandControlTransfer(mInterface, data);
                case HciPacketType.AclData:
                    int transferredLen = 0;
                    mAclEndpointWriter?.Write(data, 1000, out transferredLen);
                    return transferredLen;
                default:
                    throw new NotImplementedException($"{packetType} is not yet supported by UsbBluetooth. Please report this issue.");
            }
        }

        public byte[]? Read()
        {
            if (!IsOpen)
                throw new DeviceClosedException();

            // Create a buffer...
            byte[] packet = new byte[1024];
            Span<byte> packetData = packet.AsSpan().Slice(1);

            // Read data endpoint
            int transferLen = 0;
            Error err = mAclEndpointReader.Read(packetData.ToArray(), 1000, out transferLen);
            if (err == Error.Success && transferLen > 0)
            {
                packet[0] = (byte)HciPacketType.AclData;
                return packet.AsSpan().Slice(0, transferLen).ToArray();
            }

            // No luck, try the events endpoint...
            transferLen = 0;
            err = mEventEndpointReader.Read(packetData.ToArray(), 1000, out transferLen);
            if (err == Error.Success && transferLen > 0)
            {
                packet[0] = (byte)HciPacketType.Event;
                return packet.AsSpan().Slice(0, transferLen).ToArray();
            }

            // No data, just return null
            return null;
        }

        public override string ToString()
        {
            return $"Controller{{vid=0x{VendorId:x4}, pid=0x{ProductId:x4}}}";
        }
    }
}
