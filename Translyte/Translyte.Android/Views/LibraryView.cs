using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using Translyte.Android.CustomClasses;
using Translyte.Core;
using Translyte.Core.DataProvider;
using Translyte.Core.DataProvider.SQLite;
using Translyte.Core.Models;
using Translyte.Core.ViewModels;
using Environment = System.Environment;

namespace Translyte.Android.Views
{
    [Activity(Label = "Library", MainLauncher = true)]
    public class LibraryView : Activity
    {
        public TranslyteDbGateway TranslyteDbGateway { get; set; }
        Connection conn;
        private List<BookReviewModel> _books; 
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LibraryView);
            var sqliteFilename = "TaskDB.db3";
            string libraryPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            conn = new Connection(path);

            TranslyteDbGateway = new TranslyteDbGateway(conn);

            _books = TranslyteDbGateway.GetBooksLocalReview();

            GalleryAdapter adapter = new GalleryAdapter(this, _books);
            ListView listView = FindViewById<ListView>(Resource.Id.ListView);
            listView.Adapter = adapter;
            listView.ItemClick += OnListItemClick; 
            ImageView image = FindViewById<ImageView>(Resource.Id.Cover);
            var book = new BookReviewModel();

            image.Click += delegate
            {
                var intent = new Intent(this, typeof(BookView));
                string jsonModel = JsonConvert.SerializeObject(book);
                intent.PutExtra("book", jsonModel);
                StartActivity(intent);
            };
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var book = _books[e.Position];
            Book curBook = new BookReviewModel(book.BookPath);
            //curBook.BookPath = book.BookPath;
            Book.Load(ref curBook);
            //Book res = (BookReviewModel) curBook;
            var intent = new Intent(this, typeof(BookView));
                string jsonModel = JsonConvert.SerializeObject(book);
                intent.PutExtra("book", jsonModel);
                StartActivity(intent);
        }
    }
}