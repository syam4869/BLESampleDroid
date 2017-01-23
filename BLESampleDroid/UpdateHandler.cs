using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;

namespace BLESampleDroid
{
    class UpdateHandler : Handler
    {
        ArrayAdapter adapter;
        Activity context;
        ArrayAdapter ListAdapter { get; set; }

        public UpdateHandler(Activity context, ArrayAdapter adapter)
        {
            this.context = context;
            this.adapter = adapter;
        }

        public override void HandleMessage(Message msg)
        {
            base.HandleMessage(msg);

            var bundle = msg.Data;

            BeaconInfo beaconInfo = bundle.Get("beaconInfo").JavaCast<BeaconInfo>();
            adapter.Insert(beaconInfo.ToString(),0);


        }
    }
}