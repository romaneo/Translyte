using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;

namespace Translyte.Android.Views
{
     [Activity(Label = "Authentication")]
    class AuthenticationView : MvxActivity
    {
         protected override void OnCreate(Bundle bundle)
         {
             base.OnCreate(bundle);

             SetContentView(Resource.Layout.AuthenticationView);
         }
    }
}