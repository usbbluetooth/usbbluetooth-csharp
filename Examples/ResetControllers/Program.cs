using UsbBluetooth;

Console.WriteLine("Resetting connected Bluetooth controllers that have the correct driver:");

foreach (Controller controller in UsbBluetoothManager.ListControllersWithDriver())
{
    try
    {
        Console.WriteLine($"Opening {controller}");
        controller.Open();

        Console.WriteLine($"Sending a reset command...");
        controller.Write([0x01, 0x03, 0x0c, 0x00]);

        byte[] response = controller.Read();
        Console.WriteLine($"Got a response: {Convert.ToHexString(response)}");

        controller.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine($"Failed: {e}");
    }
}
