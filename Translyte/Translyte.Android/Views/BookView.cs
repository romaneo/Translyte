using System.Threading;
using Android.App;
using Android.Content;
using Android.Graphics;
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
        private View ParentView { get; set; }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ParentView = inflater.Inflate(Resource.Layout.BookView, container, false);
            LibraryView parentActivity = Activity as LibraryView;
            ParentActivity = parentActivity;
            HasOptionsMenu = true;
            var resideMenu = parentActivity.ResideMenu;

            var title = ParentView.FindViewById<TextView>(Resource.Id.tv_title);
            TextView content = ParentView.FindViewById<TextView>(Resource.Id.tv_book);
            content.SetTextIsSelectable(true);


            ParentView.FindViewById(Resource.Id.tv_book).Click += (s, e) => resideMenu.OpenMenu(global::AndroidResideMenu.ResideMenu.Direction.Left);

            ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
            var isDark = prefs.GetBoolean("themeDark", false);
            if (isDark)
            {
                var layout = ParentView.FindViewById<RelativeLayout>(Resource.Id.bookLayout);
                layout.SetBackgroundColor(Color.DarkGray);
                content.SetTextColor(Color.WhiteSmoke);
                title.SetTextColor(Color.WhiteSmoke);
            }
			var size = prefs.GetInt("textSize", 20);
			content.SetTextSize(ComplexUnitType.Dip, size);
			title.SetTextSize(ComplexUnitType.Dip, size + 5);

			var textFont = prefs.GetString("textFont", "Arial");
			var res="";
			switch(textFont)
			{
			case "Arial":
				res = "arial";
				break;
			case "Verdana":
				res = "airstrike";
				break;
			case "Calibri":
				res = "BodoniFLF-Roman";
				break;
			default :
				res = "Raleway-Medium";
				break;
			}
			Typeface tf = Typeface.CreateFromAsset (Application.Context.Assets, "fonts/" + res + ".ttf");
			content.Typeface = tf;
			title.SetTextSize(ComplexUnitType.Dip, 25);
            
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
                    content.Text = ((BookFullModel)curBook).Content;
                    content.CustomSelectionActionModeCallback = new WordSelector(content, ParentActivity, ((BookFullModel)curBook).Language);
                });
            }
            return ParentView;
        }

        public override void OnDestroy()
        {
            var scroll = ParentView.FindViewById<ScrollView>(Resource.Id.sv_bookContent);
            var position = scroll.ScrollY;
            //TODO: save position to global book object
            base.OnDestroy();
        }
    }
}