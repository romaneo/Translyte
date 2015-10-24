using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using Cirrious.MvvmCross.Droid.Views;

namespace Translyte.Android.Views
{
    //public static class DropboxCredentials
    //{
    //    // To get your credentials, create your own Drobox App.
    //    // Visit the following link: https://www.dropbox.com/developers/apps
    //    public static string AppKey = "goenzzov1epvl88";
    //    public static string AppSecret = "6ms3j2412l0dm8p";
    //    public static string FolderPath = "/";
    //    public static string PreferencesName = "Prefs";
    //}

    [Activity(Label = "Dropbox")]
    public class DropboxView : MvxActivity
    {      
        ListView lstBooksName;

        private API.DropboxSync _sync = new API.DropboxSync();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DropboxView);
            _sync.BootstrapDropbox(this);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            //var code = 0;

            if (requestCode == 0)
            {
                _sync.StartApp();
            }
            else
            {
                _sync.Account.Unlink();
                _sync.BootstrapDropbox(this);
            }
        }

        

    }
}