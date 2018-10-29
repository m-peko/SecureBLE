using System;
using System.Collections.Generic;
using Android.App;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Runtime;
using Android.Support.Design.Widget;
using static Xamarin.Forms.Forms;

namespace ArduinoApp
{
    public partial class MainPage
    {
        public static List<BluetoothDevice> discoveredDevices = new List<BluetoothDevice>();

        public MainPage()
        {
            InitializeComponent();
        }

        async void ConnectToArduino_Clicked(object sender, EventArgs args)
        {
            // TODO: replace Context with local context
            var contentView = (Context as Activity)?.FindViewById(Android.Resource.Id.Content);

            BluetoothAdapter mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            BluetoothLeScanner mBleScanner = mBluetoothAdapter.BluetoothLeScanner;

            // Device does not support Bluetooth
            if (mBluetoothAdapter == null)
            {
                Snackbar.Make(contentView, "Device doesn't support Bluetooth.", Snackbar.LengthLong).Show();
                return;
            }

            //Buletooth is not enabled
            if (!mBluetoothAdapter.IsEnabled)
            {
                //ask to enable bluetooth
                var answer = await DisplayAlert("", "Would you like enable Bluetooth?", "Yes", "No");
                if (!answer) return;

                //enable bluetooth and display message
                mBluetoothAdapter.Enable();
                Snackbar.Make(contentView, "Bluetooth is enabled.", Snackbar.LengthLong).Show();
            }

            //TODO: bt is enabled - start scanning BLE devices
            mBleScanner.StartScan(new BluetoothScanCallback());
            //TODO: stop scan

            //TODO: show list of found devices on next screen
            //TODO: when clicked on item in list initiate connection with device

        }

        public class BluetoothScanCallback : ScanCallback
        {
            public override void OnScanResult([GeneratedEnum] ScanCallbackType callbackType, ScanResult result)
            {
                discoveredDevices.Add(result.Device);
            }
        }

    }
}
