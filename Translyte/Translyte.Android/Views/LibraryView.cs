using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
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
using Translyte.Core.ViewModels;
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
        private ResideMenuItem _itemProfile;
        private ResideMenuItem _itemCalendar;
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

			Java.IO.File dir =  new Java.IO.File(EnvironmentAnd.ExternalStorageDirectory.AbsolutePath + @"/translyte");
			if (!dir.Exists ())
				dir.Mkdirs ();

            //if (bundle == null)
            //    ChangeFragment(new BookView());

            var sqliteFilename = "TaskDB.db3";
            string libraryPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            conn = new Connection(path);
            TranslyteDbGateway = new TranslyteDbGateway(conn);
            //TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/gg.fb2", Position = 0, IsCurrent = false });
            ////TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/Brodyagi_Dharmy.fb2", Position = 0, IsCurrent = true });
            ////TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/Genri.fb2", Position = 0, IsCurrent = false });
            //TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/Kafka.fb2", Position = 0, IsCurrent = false });
            //TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/Karl.fb2", Position = 0, IsCurrent = false });
            //TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/Pail.fb2", Position = 0, IsCurrent = false });
            //TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/Starik.fb2", Position = 0, IsCurrent = false });
			//TranslyteDbGateway.DeleteAllBooks();
            /*if (TranslyteDbGateway.GetBooksLocalReview().Count == 0)
            {
                TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/gg.fb2", Position = 0, IsCurrent = false });
                TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/Brodyagi_Dharmy.fb2", Position = 0, IsCurrent = true });
                TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/Genri.fb2", Position = 0, IsCurrent = false });
                TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/Kafka.fb2", Position = 0, IsCurrent = false });
                TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/Karl.fb2", Position = 0, IsCurrent = false });
                TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/Pail.fb2", Position = 0, IsCurrent = false });
                TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = "/storage/sdcard0/translyte/Starik.fb2", Position = 0, IsCurrent = false });
            }*/
            
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
                    //var intent = new Intent(this, typeof(BookView));
                    string jsonModel = JsonConvert.SerializeObject(CurrentBook);
                    //intent.PutExtra("book", jsonModel);
                    //StartActivity(intent);
                    var fragment = new BookView();
                    Bundle bundle2 = new Bundle();
                    bundle2.PutString("book", jsonModel);
                    fragment.Arguments = bundle2;
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

            //Now done with MenuOpened/Closed handlers!
            //ResideMenu.SetMenuListener(this);

            ResideMenu.MenuOpened += OnMenuOpened2;
            ResideMenu.MenuClosed += OnMenuClosed;

            ResideMenu.SetScaleValue(0.6F);

            // create menu items;
            _itemLibrary = new ResideMenuItem(this, Resource.Drawable.Library, "Library");
            _itemProfile = new ResideMenuItem(this, Resource.Drawable.icon_profile, "Profile");
            _itemCalendar = new ResideMenuItem(this, Resource.Drawable.Bookmarks, "Bookmark");
            _itemSettings = new ResideMenuItem(this, Resource.Drawable.icon_settings, "Settings");

            _itemLibrary.SetOnClickListener(this);
            _itemProfile.SetOnClickListener(this);
            _itemCalendar.SetOnClickListener(this);
            _itemSettings.SetOnClickListener(this);

            ResideMenu.AddMenuItem(_itemLibrary, global::AndroidResideMenu.ResideMenu.Direction.Left);
            ResideMenu.AddMenuItem(_itemProfile, global::AndroidResideMenu.ResideMenu.Direction.Left);
            ResideMenu.AddMenuItem(_itemCalendar, global::AndroidResideMenu.ResideMenu.Direction.Left);
            ResideMenu.AddMenuItem(_itemSettings, global::AndroidResideMenu.ResideMenu.Direction.Left);

            // You can disable a direction by setting ->
            //ResideMenu.setSwipeDirectionDisable//(ResideMenu.DIRECTION_RIGHT);
            ResideMenu.SetSwipeDirectionDisable(AndroidResideMenu.ResideMenu.Direction.Right);

            FindViewById(Resource.Id.title_bar_left_menu).Click += (s, e) => { ResideMenu.OpenMenu(global::AndroidResideMenu.ResideMenu.Direction.Left); };
            //FindViewById(Resource.Id.title_bar_right_menu).Click += (s, e) => { ResideMenu.OpenMenu(global::AndroidResideMenu.ResideMenu.Direction.Right); };
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
				//UpdateView();
            }
            //else if (view == _itemProfile)
            //{
            //    ChangeFragment(new ProfileFragment());
            //}
            //else if (view == _itemCalendar)
            //{
            //    ChangeFragment(new CalendarFragment());
            //}
            //else 
            if (view == _itemSettings)
            {
                ChangeFragment(new SettingsView());
            }
            if (view == _itemProfile)
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

        private void OnMenuOpened2(object sender, EventArgs e)
        {
            //Toast.MakeText(this, "Menu is opened!", ToastLength.Short).Show();
        }

        private void OnMenuClosed(object sender, EventArgs e)
        {
            //Toast.MakeText(this, "Menu is closed!", ToastLength.Short).Show();
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