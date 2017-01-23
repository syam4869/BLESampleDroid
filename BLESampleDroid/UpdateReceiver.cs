
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using BLESampleDroid;

namespace BLESampleDroid
{
    class UpdateReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var bundle = intent.Extras;

            if (RegisterHandler != null)
            {
                var message = new Message();

                var data = new Bundle();
                data.PutAll(bundle);
                message.Data = data;

                RegisterHandler.SendMessage(message);
            }
        }

        public Handler RegisterHandler { get; set; }
    }

}