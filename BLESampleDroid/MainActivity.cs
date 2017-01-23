using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using static Android.Widget.AdapterView;
using System;
using Android.Content;

namespace BLESampleDroid
{
    [Activity(Label = "BLESampleDroid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity, GestureDetector.IOnGestureListener
    {
        //タッチイベントを監視するクラス
        GestureDetector gestureDetector;
        //Listを管理するクラス
        ArrayAdapter adapter;

        /// <summary>
        /// EntryMethod
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //listview
            var listview = FindViewById<ListView>(Resource.Id.listView);
            adapter = new ArrayAdapter(this,Android.Resource.Layout.TestListItem);
            listview.Adapter = adapter;

            //ScanButton
            Button scanbtn = FindViewById<Button>(Resource.Id.scanButton);
            //ScanButtonEvent
            scanbtn.Click += (StartIntentSender, args) =>
            {
                if (scanbtn.Text.Equals(this.GetString(Resource.String.scanstart)))
                {
                    scanbtn.Text = this.GetString(Resource.String.scanstop);
                    StartService(new Intent("BLEService"));

                }
                else 
                {
                    scanbtn.Text = this.GetString(Resource.String.scanstart);
                    StopService(new Intent("BLEService"));

                }//if

            };//scanbtn.Click

            var upReceiver = new UpdateReceiver();
            var intentFilter = new IntentFilter("UPDATE_ACTION");

            // レシーバーの登録
            RegisterReceiver(upReceiver, intentFilter);

            // ハンドラーの登録
            upReceiver.RegisterHandler = new UpdateHandler(this, adapter);

        }

        public bool OnDown(MotionEvent e)
        {
            throw new NotImplementedException();
        }

        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            throw new NotImplementedException();
        }

        public void OnLongPress(MotionEvent e)
        {
            throw new NotImplementedException();
        }

        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            throw new NotImplementedException();
        }

        public void OnShowPress(MotionEvent e)
        {
            throw new NotImplementedException();
        }

        public bool OnSingleTapUp(MotionEvent e)
        {
            throw new NotImplementedException();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            gestureDetector.OnTouchEvent(e);
            return false;
        }
    }
}

