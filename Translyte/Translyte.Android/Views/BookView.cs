using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Translyte.Core.ViewModels;

namespace Translyte.Android.Views
{
    [Activity(Label = "Book")]
    public class BookView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.BookView);
            
        }
    }
}