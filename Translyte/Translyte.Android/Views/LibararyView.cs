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
using Translyte.Core.DataProvider;
using Translyte.Core.DataProvider.SQLite;
using Environment = System.Environment;

namespace Translyte.Android.Views
{
    [Activity(Label = "Dropbox")]
    public class LibararyView : MvxActivity
    {
        public TranslyteDbGateway TranslyteDbGateway { get; set; }
        Connection conn;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LibraryView);
            var sqliteFilename = "TaskDB.db3";
            string libraryPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            conn = new Connection(path);

            TranslyteDbGateway = new TranslyteDbGateway(conn);
        }
       
    }
}