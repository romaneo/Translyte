using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware.Display;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Speech;
using Java.Lang;
using Math = System.Math;

namespace WordSelection
{
    [Activity(Label = "WordSelection", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            TextView book = FindViewById<TextView>(Resource.Id.tv_book);
            book.SetTextIsSelectable(true);
            //book.CustomSelectionActionModeCallback = new MySelectionCallBack(book);
        }
    }
}

