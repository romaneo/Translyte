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

namespace Translyte.Android.Views
{
    public class TranslateMessage : DialogFragment
    {
        public string Original { get; set; }
        public string Translation { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.TranslateMessage, container, false);
            Dialog.SetTitle(Original);
            TextView t = view.FindViewById<TextView>(Resource.Id.tranlation);
            t.Text = Translation;

            return view;
        }
    }
}