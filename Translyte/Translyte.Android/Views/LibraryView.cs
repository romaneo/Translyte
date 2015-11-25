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
using Translyte.Core.DataProvider;
using Translyte.Core.DataProvider.SQLite;
using Translyte.Core.ViewModels;
using Environment = System.Environment;

namespace Translyte.Android.Views
{
    [Activity(Label = "Library", MainLauncher = true)]
    public class LibraryView : Activity
    {
        public TranslyteDbGateway TranslyteDbGateway { get; set; }
        Connection conn;
        private List<BookViewModel> _books; 
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LibraryView);
            var sqliteFilename = "TaskDB.db3";
            string libraryPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            conn = new Connection(path);

            TranslyteDbGateway = new TranslyteDbGateway(conn);

            _books = new List<BookViewModel>();
            _books.Add(new BookViewModel()
            {
                Title = "Ancient tales",
                Author = "Ducan Long",
                IsAvailable = true,
                Cover = Resource.Drawable.book1
            });
            _books.Add(new BookViewModel()
            {
                Title = "Responsive web design",
                Author = "Ethan Macrotte",
                IsAvailable = false,
                Cover = Resource.Drawable.book2
            });
            _books.Add(new BookViewModel()
            {
                Title = "Game of thrones",
                Author = "George R.R.",
                IsAvailable = false,
                Cover = Resource.Drawable.game
            });
            _books.Add(new BookViewModel()
            {
                Title = "Stephen King",
                Author = "The dark tower",
                IsAvailable = false,
                Cover = Resource.Drawable.dark
            });
            _books.Add(new BookViewModel()
            {
                Title = "The lord of the rings",
                Author = "J.R.R. Tolkien",
                IsAvailable = true,
                Cover = Resource.Drawable.lotr
            });

            GalleryAdapter adapter = new GalleryAdapter(this, _books);
            ListView listView = FindViewById<ListView>(Resource.Id.ListView);
            listView.Adapter = adapter;
            listView.ItemClick += OnListItemClick; 
            ImageView image = FindViewById<ImageView>(Resource.Id.Cover);
            var book = new BookViewModel();

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
            var intent = new Intent(this, typeof(BookView));
                string jsonModel = JsonConvert.SerializeObject(book);
                intent.PutExtra("book", jsonModel);
                StartActivity(intent);
        }
    }
}