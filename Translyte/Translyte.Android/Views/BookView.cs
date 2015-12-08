using System.Threading;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Widget;
using Newtonsoft.Json;
using Translyte.Android.CustomClasses;
using Translyte.Core;
using Translyte.Core.Models;
using SupportFragment = Android.Support.V4.App.Fragment;
using Android.Views;

namespace Translyte.Android.Views
{
    [Activity(Label = "Book")]
    public class BookView : SupportFragment
    {
        LibraryView ParentActivity
        {
            get;
            set;
        }

        public BookFullModel CurrentBook { get; set; }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //
            
            View parentView = inflater.Inflate(Resource.Layout.BookView, container, false);
            LibraryView parentActivity = Activity as LibraryView;
            ParentActivity = parentActivity;
            HasOptionsMenu = true;
            var resideMenu = parentActivity.ResideMenu;

            var title = parentView.FindViewById<TextView>(Resource.Id.tv_title);
            TextView content = parentView.FindViewById<TextView>(Resource.Id.tv_book);
            content.SetTextIsSelectable(true);
            content.CustomSelectionActionModeCallback = new WordSelector(content, ParentActivity);

            parentView.FindViewById(Resource.Id.tv_book).Click += (s, e) => resideMenu.OpenMenu(global::AndroidResideMenu.ResideMenu.Direction.Left);

            ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
            var isDark = prefs.GetBoolean("themeDark", false);
            if (isDark)
            {
                var layout = parentView.FindViewById<RelativeLayout>(Resource.Id.bookLayout);
                layout.SetBackgroundColor(Color.DarkGray);
                //var contentBook = parentView.FindViewById<TextView>(Resource.Id.tv_book);
                content.SetTextColor(Color.WhiteSmoke);
                title.SetTextColor(Color.WhiteSmoke);
            }
            var isLarge = prefs.GetBoolean("fontLarge", false);
            if (isLarge)
            {
                content.SetTextSize(ComplexUnitType.Dip, 20);
                title.SetTextSize(ComplexUnitType.Dip, 25);
            }

            //var extraData = parentActivity.Intent.GetStringExtra("book");
            string extraData = null;
            if (Arguments != null)
                extraData = Arguments.GetString("book");
            if (extraData != null)
            {
                var tempBook = JsonConvert.DeserializeObject<BookReviewModel>(extraData);
                title.Text = tempBook.Title;
                parentActivity.RunOnUiThread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Book curBook = new BookFullModel(tempBook.BookPath);
                    Book.Load(ref curBook);
                    content.Text = ((BookFullModel)curBook).Chapters[0].Content;
                });
            }
            return parentView;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            //var subMenu1 = menu.AddSubMenu("Action Item");
            //subMenu1.Add("Sample");
            //subMenu1.Add("Menu");
            //subMenu1.Add("Items");

            //var subMenu1Item = subMenu1.Item;
            //subMenu1Item.SetIcon(Resource.Drawable.icon_home);
            //subMenu1Item.SetShowAsAction(ShowAsAction.Always|ShowAsAction.WithText);

            //var subMenu2 = menu.AddSubMenu("Overflow Item");
            //subMenu2.Add("These");
            //subMenu2.Add("Are");
            //subMenu2.Add("Sample");
            //subMenu2.Add("Items");

            //var subMenu2Item = subMenu2.Item;
            //subMenu2Item.SetIcon(Resource.Drawable.Library);
             //ParentActivity.MenuInflater.Inflate(Resource.Drawable.optionsmenu, menu);
            //inflater.Inflate(Resource.Drawable.optionsmenu, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override void OnDestroyOptionsMenu()
        {
            //TextView text = ParentActivity.FindViewById<TextView>(Resource.Id.rmt);
            //int start = text.SelectionStart;
            //int ent = text.SelectionEnd;
            //string tx = text.Text.Substring(start, ent - start);
            //Toast.MakeText(ParentActivity, "blabla", ToastLength.Short).Show();
            base.OnDestroyOptionsMenu();
        }

        //protected override void OnSaveInstanceState(Bundle outState)
        //{
        //    outState.PutInt("counter", c);
        //    base.OnSaveInstanceState(outState);
        //}
        //protected override void OnRestoreInstanceState(Bundle savedState)
        //{
        //    base.OnRestoreSaveInstanceState(savedState);
        //    var myString = savedState.GetString("myString”);
        //    var myBool = GetBoolean("myBool”);
        //}
    }
}