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
using Translyte.Android.CustomClasses;
using Translyte.Core.DataProvider;
using Translyte.Core.DataProvider.SQLite;
using Translyte.Core.ViewModels;
using Environment = System.Environment;

namespace Translyte.Android.Views
{
    [Activity(Label = "Library")]
    public class LibraryView : MvxActivity
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

            List<BookViewModel> items = new List<BookViewModel>();
            items.Add(new BookViewModel()
            {
                Author = "android book",
                Title = "android",
                IsAvailable = true,
                //Cover = Resource.Drawable.book1
            });
            items.Add(new BookViewModel()
            {
                Author = "book1",
                Title = "book1 author",
                IsAvailable = false,
                //Cover = Resource.Drawable.book1
            });
            items.Add(new BookViewModel()
            {
                Author = "book2",
                Title = "book2 author",
                IsAvailable = false,
                //Cover = Resource.Drawable.book2
            });
            items.Add(new BookViewModel()
            {
                Author = "book3",
                Title = "book3 author",
                IsAvailable = false,
                //Cover = Resource.Drawable.book2
            });
            items.Add(new BookViewModel()
            {
                Author = "book4",
                Title = "book4 author",
                IsAvailable = true,
                //Cover = Resource.Drawable.book2
            });
            GalleryAdapter adapter = new GalleryAdapter(this, items);

            ListView listView = FindViewById<ListView>(Resource.Id.ListView);

            listView.Adapter = adapter;

            ImageView image = FindViewById<ImageView>(Resource.Id.Cover);
            image.Click += delegate
            {
                StartActivity(typeof(BookView));
            };
        }

       
    }
}