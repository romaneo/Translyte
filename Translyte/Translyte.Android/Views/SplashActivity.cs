using Android.App;
using Android.OS;
using System.Threading;

namespace Translyte.Android.Views
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            StartActivity(typeof(LibraryView));
        }
    }
}