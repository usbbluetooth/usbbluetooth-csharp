namespace UsbBluetooth
{
    internal enum HciPacketType : byte
    {
        Command = 0x01,
        AclData = 0x02,
        Event = 0x04,
    }
}
