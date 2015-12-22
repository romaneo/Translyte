using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using AndroidResideMenu;
using Newtonsoft.Json;
using Translyte.Android.CustomClasses;
using Translyte.Android.Helpers;
using Translyte.Core;
using Translyte.Core.DataProvider;
using Translyte.Core.DataProvider.SQLite;
using Translyte.Core.Models;
using EnvironmentAnd = Android.OS.Environment;
using Environment = System.Environment;
using Fragment = Android.Support.V4.App.Fragment;

namespace Translyte.Android.Views
{
    [Activity(Label = "Translyte", Theme = "@android:style/Theme.Light.NoTitleBar")]
    public class LibraryView : FragmentActivity, View.IOnClickListener
    {
        public override void OnBackPressed()
        {
            UpdateView();
            base.OnBackPressed();
        }

        public global::AndroidResideMenu.ResideMenu ResideMenu { get; private set; }
        private LibraryView _context;
        private ResideMenuItem _itemLibrary;
        private ResideMenuItem _bookBrowser;
        private ResideMenuItem _bookmarks;
        private ResideMenuItem _itemSettings;

        public TranslyteDbGateway TranslyteDbGateway { get; set; }
        Connection conn;
        private List<BookReviewModel> _books = new List<BookReviewModel>();
        private GalleryAdapter adapter;

        public BookReviewModel CurrentBook { get; set; }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LibraryView);
            _context = this;
            SetupMenu();

            var sqliteFilename = "TaskDB.db3";
            string libraryPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            conn = new Connection(path);
            TranslyteDbGateway = new TranslyteDbGateway(conn);

			Java.IO.File dir =  new Java.IO.File(EnvironmentAnd.ExternalStorageDirectory.AbsolutePath + @"/translyte");
			if (!dir.Exists ()) {
				dir.Mkdirs ();
				TranslyteDbGateway.DeleteAllBooks ();
			}				
            
            _books = TranslyteDbGateway.GetBooksLocalReviewWithoutCurrent();

            adapter = new GalleryAdapter(this, _books);
            ListView listView = FindViewById<ListView>(Resource.Id.ListView);
            listView.Adapter = adapter;
            listView.ItemClick += OnListItemClick;

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
                    string jsonModel = JsonConvert.SerializeObject(CurrentBook);
                    var fragment = new BookView();
                    Bundle bookBundle = new Bundle();
                    bookBundle.PutString("book", jsonModel);
                    fragment.Arguments = bookBundle;
                    ChangeFragment(fragment);
                };
                TextView title = FindViewById<TextView>(Resource.Id.TitleCurrent);
                title.Text = CurrentBook.Title;

                TextView author = FindViewById<TextView>(Resource.Id.AuthorCurrent);
                author.Text = CurrentBook.Author;
            }
        }

        private void SetupMenu()
        {
            ResideMenu = new global::AndroidResideMenu.ResideMenu(this);
            ResideMenu.SetBackground(Resource.Drawable.menu_background);
            ResideMenu.AttachToActivity(this);

            ResideMenu.SetScaleValue(0.6F);

            _itemLibrary = new ResideMenuItem(this, Resource.Drawable.Library, "Library");
            _bookBrowser = new ResideMenuItem(this, Resource.Drawable.icon_profile, "Load books");
            _bookmarks = new ResideMenuItem(this, Resource.Drawable.Bookmarks, "Bookmark");
            _itemSettings = new ResideMenuItem(this, Resource.Drawable.icon_settings, "Settings");

            _itemLibrary.SetOnClickListener(this);
            _bookBrowser.SetOnClickListener(this);
            _bookmarks.SetOnClickListener(this);
            _itemSettings.SetOnClickListener(this);

            ResideMenu.AddMenuItem(_itemLibrary, global::AndroidResideMenu.ResideMenu.Direction.Left);
            ResideMenu.AddMenuItem(_bookBrowser, global::AndroidResideMenu.ResideMenu.Direction.Left);
            ResideMenu.AddMenuItem(_bookmarks, global::AndroidResideMenu.ResideMenu.Direction.Left);
            ResideMenu.AddMenuItem(_itemSettings, global::AndroidResideMenu.ResideMenu.Direction.Left);

            ResideMenu.SetSwipeDirectionDisable(AndroidResideMenu.ResideMenu.Direction.Right);

            FindViewById(Resource.Id.title_bar_left_menu).Click += (s, e) => { ResideMenu.OpenMenu(global::AndroidResideMenu.ResideMenu.Direction.Left); };
        }

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            return ResideMenu.DispatchTouchEvent(ev);
        }

        public void OnClick(View view)
        {
            if (view == _itemLibrary)
            {
                OnBackPressed();
				_books.Clear();
				_books = TranslyteDbGateway.GetBooksLocalReviewWithoutCurrent();
				UpdateView();
            }
            if (view == _itemSettings)
            {
                ChangeFragment(new SettingsView());
            }
            if (view == _bookBrowser)
            {
                ChangeFragment(new FileExplorerView());
            }
            ResideMenu.CloseMenu();
        }

        private void ChangeFragment(Fragment targetFragment)
        {
            ResideMenu.ClearIgnoredViewList();
            
            SupportFragmentManager
                    .BeginTransaction().AddToBackStack(null)
                    .Replace(Resource.Id.main_fragment, targetFragment, "fragment")
                    .SetTransitionStyle(global::Android.Support.V4.App.FragmentTransaction.TransitFragmentFade)
                    .Commit();
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
            //var intent = new Intent(this, typeof(BookView));
                string jsonModel = JsonConvert.SerializeObject(book);
               // intent.PutExtra("book", jsonModel);
                //StartActivity(intent);
                var fragment = new BookView();
                Bundle bundle = new Bundle();
                bundle.PutString("book", jsonModel);
                fragment.Arguments = bundle;
                ChangeFragment(fragment);
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