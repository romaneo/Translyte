using Android.App;
using Android.OS;

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