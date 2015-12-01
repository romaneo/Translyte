using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Translyte.Android.CustomClasses;
using Translyte.Core;
using Translyte.Core.Models;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Translyte.Android.Views
{
     [Activity(Label = "Settings")]
    public class SettingsView : SupportFragment, View.IOnClickListener
    {
         public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
         {
             //
             View parentView = inflater.Inflate(Resource.Layout.SettingsView, container, false);
             return parentView;
         }
        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }
    }
}