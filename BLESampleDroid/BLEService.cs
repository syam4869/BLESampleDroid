using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Widget;

namespace BLESampleDroid
{
    /// <summary>
    /// Service for BLEScan
    /// </summary>
    [Service]
    [IntentFilter(new String[] { "BLEService" })]
    public class BLEService : Service
    {
        //BlueTooth
        BluetoothManager manager = null;
        BluetoothAdapter bluetoothAdapter = null;
        BluetoothLeScanner mBluetoothLeScanner = null;

        //BlueToothスキャン時にコールバック呼び出し
        private BleScanCallback mScanCallback;

        public BLEService()
        {
        }

        /// <summary>
        /// Ons the create.
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();

            //BlueTooth準備
            manager = (BluetoothManager)GetSystemService(Context.BluetoothService);
            bluetoothAdapter = manager.Adapter;

        }


        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ServiceStart時に呼び出される
        /// </summary>
        /// <returns>The start command.</returns>
        /// <param name="intent">Intent.</param>
        /// <param name="flags">Flags.</param>
        /// <param name="startId">Start identifier.</param>
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            //BluetoorhがON状態の時にサービス処理開始
            if (bluetoothAdapter.Enable())
            {
                Transceiver();

            }//if

            return base.OnStartCommand(intent, flags, startId);
        }

        private void Transceiver()
        {

            mBluetoothLeScanner = bluetoothAdapter.BluetoothLeScanner;

            List<ScanFilter> mScanFilters = new List<ScanFilter>();

            ScanSettings.Builder mScanSettingBuilder = new ScanSettings.Builder();
            mScanSettingBuilder.SetScanMode(Android.Bluetooth.LE.ScanMode.LowLatency);

            ScanSettings mScanSettigs = mScanSettingBuilder.Build();

            mScanCallback = new BleScanCallback(this);

            mBluetoothLeScanner.StartScan(mScanFilters, mScanSettigs, mScanCallback);
        }

        /// <summary>
        /// ServiceStop時に呼び出される
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();

            if (mBluetoothLeScanner != null)
            {
                mBluetoothLeScanner.StopScan(mScanCallback);

            }
        }
    }

    class BleScanCallback : ScanCallback
    {
        private Context context;

        public BleScanCallback(Context context)
        {
            this.context = context;
        }

        public override void OnScanResult([GeneratedEnum] ScanCallbackType callbackType, ScanResult result)
        {
            base.OnScanResult(callbackType, result);
            var beaconInfo = new BeaconInfo();

            beaconInfo.macaddress = result.Device.Address;
            beaconInfo.rssi = result.Rssi.ToString();
            beaconInfo.recievetime = DateTimeOffset.Now.ToString("yyyy/MM/dd HH:mm:ss");
            beaconInfo.devicename = result.ScanRecord.DeviceName;
            if (beaconInfo != null)
            {
                MySendBroadCast(beaconInfo);
            }

            }

            private void MySendBroadCast(BeaconInfo beaconInfo)
            {
                var intent = new Intent("UPDATE_ACTION");
                intent.PutExtra("beaconInfo", beaconInfo);
                context.SendBroadcast(intent);
            }

        }

}
