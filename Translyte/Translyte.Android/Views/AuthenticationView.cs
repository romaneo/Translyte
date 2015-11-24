using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Translyte.Android.Views
{
     [Activity(Label = "Authentication")]
    class AuthenticationView : Activity
    {
         protected override void OnCreate(Bundle bundle)
         {
             base.OnCreate(bundle);

             SetContentView(Resource.Layout.AuthenticationView);
         }
    }
}