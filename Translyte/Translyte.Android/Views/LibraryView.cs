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
using Translyte.Android.Helpers;
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
        private List<BookReviewModel> _books = new List<BookReviewModel>();
        private GalleryAdapter adapter;

        public BookReviewModel CurrentBook { get; set; }
        protected override void OnCreate(Bundle bundle)
        {
            var sqliteFilename = "TaskDB.db3";
            string libraryPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            conn = new Connection(path);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LibraryView);
            TranslyteDbGateway = new TranslyteDbGateway(conn);
            if (TranslyteDbGateway.GetBooksLocalReview().Count == 0)
            {
                //TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/sdcard/translyte/gg.fb2", Position = 0, IsCurrent = false });
                //TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/sdcard/translyte/Brodyagi_Dharmy.fb2", Position = 0, IsCurrent = true });
                //TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/sdcard/translyte/Genri.fb2", Position = 0, IsCurrent = false });
                //TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/sdcard/translyte/Kafka.fb2", Position = 0, IsCurrent = false });
                //TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/sdcard/translyte/Karl.fb2", Position = 0, IsCurrent = false });
                //TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/sdcard/translyte/Pail.fb2", Position = 0, IsCurrent = false });
                //TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/sdcard/translyte/Starik.fb2", Position = 0, IsCurrent = false });
            }
            
            _books = TranslyteDbGateway.GetBooksLocalReviewWithoutCurrent();

            adapter = new GalleryAdapter(this, _books);
            ListView listView = FindViewById<ListView>(Resource.Id.ListView);
            listView.Adapter = adapter;
            listView.ItemClick += OnListItemClick;
            //BookReviewModel curBook;

            if ((CurrentBook = TranslyteDbGateway.GetCurrentBook()) != null)
            {
                ImageView image = FindViewById<ImageView>(Resource.Id.CoverCurrent);
                if (CurrentBook.Cover != null)
                {
                    var img = new BitmapConverteHelper().GetCover(CurrentBook.Cover);
                    image.SetImageBitmap(img);
                }
                image.Click += delegate
                {
                    var intent = new Intent(this, typeof(BookView));
                    string jsonModel = JsonConvert.SerializeObject(CurrentBook);
                    intent.PutExtra("book", jsonModel);
                    StartActivity(intent);
                };
                TextView title = FindViewById<TextView>(Resource.Id.TitleCurrent);
                title.Text = CurrentBook.Title;

                TextView author = FindViewById<TextView>(Resource.Id.AuthorCurrent);
                author.Text = CurrentBook.Author;
            }
            
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            UpdateView();
        }

        protected override void OnResume()
        {
            base.OnResume();
            //UpdateView();
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
            TranslyteDbGateway.SetCurrentBook(curBook);
            if (curBook.ID != CurrentBook.ID)
            {
                CurrentBook = (BookReviewModel)curBook;
                _books.Clear();
                _books = TranslyteDbGateway.GetBooksLocalReviewWithoutCurrent();

                

            }
        }

        private void UpdateView()
        {
            adapter = new GalleryAdapter(this, _books);
            ListView listView = FindViewById<ListView>(Resource.Id.ListView);
            listView.Adapter = adapter;
            if ((CurrentBook = TranslyteDbGateway.GetCurrentBook()) != null)
            {
                ImageView image = FindViewById<ImageView>(Resource.Id.CoverCurrent);
                if (CurrentBook.Cover != null)
                {
                    var img = new BitmapConverteHelper().GetCover(CurrentBook.Cover);
                    image.SetImageBitmap(img);
                }

                TextView title = FindViewById<TextView>(Resource.Id.TitleCurrent);
                title.Text = CurrentBook.Title;

                TextView author = FindViewById<TextView>(Resource.Id.AuthorCurrent);
                author.Text = CurrentBook.Author;
            }
        }
    }
}