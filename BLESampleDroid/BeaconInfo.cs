using System;
using Android.OS;
using Android.Runtime;
using Java.Interop;
using Java.Lang;

namespace BLESampleDroid
{
    public class BeaconInfo : Java.Lang.Object, IParcelable
    {


        public string macaddress { get; set;}
        public string devicename { get; set; }
        public string rssi { get; set; }
        public string recievetime { get; set; }

        public override string ToString()
        {
            return string.Format("macaddress={0}, devicename={1}, rssi={2}, recievetime={3}", macaddress, devicename, rssi, recievetime);
        }

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteString(macaddress);
            dest.WriteString(devicename);
            dest.WriteString(rssi);
            dest.WriteString(recievetime);

        }

        [ExportField("CREATOR")]
        public static IParcelableCreator GetCreator()
        {
            return new MyParcelableCreator();
        }

        private class MyParcelableCreator :Java.Lang.Object, IParcelableCreator
        {


            public Java.Lang.Object CreateFromParcel(Parcel source)
            {
                var macaddress = source.ReadString();
                var devicename = source.ReadString();
                var rssi = source.ReadString();
                var recievetime = source.ReadString();


                return new BeaconInfo() { macaddress = macaddress, devicename = devicename, rssi = rssi,recievetime = recievetime };
                
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public Java.Lang.Object[] NewArray(int size)
            {
                return new Java.Lang.Object[size];
            }
        }
    }
}
