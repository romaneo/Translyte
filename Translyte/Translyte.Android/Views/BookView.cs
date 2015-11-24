using Android.App;
using Android.OS;
using Android.Widget;
using Translyte.Android.CustomClasses;

namespace Translyte.Android.Views
{
    [Activity(Label = "Book")]
    public class BookView : Activity
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