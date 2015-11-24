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
using Java.Lang;
using Translyte.Android.CustomClasses;

namespace Translyte.Android.Views
{
    [Activity(Label = "DropBox")]
    public class BookView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.BookView);
            TextView book = FindViewById<TextView>(Resource.Id.tv_book);
            book.SetTextIsSelectable(true);
            book.CustomSelectionActionModeCallback = new WordSelector(book);
        }
    }
}