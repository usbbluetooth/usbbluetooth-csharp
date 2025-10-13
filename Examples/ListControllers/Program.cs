using UsbBluetooth;

Console.WriteLine("Listing connected Bluetooth controllers:");

foreach (Controller controller in UsbBluetoothManager.ListControllers())
{
    Console.WriteLine(controller);
}
