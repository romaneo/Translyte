using Android.App;
using Android.OS;
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
            var view = inflater.Inflate(Resource.Layout.TranslateMessage, container, false);
            Dialog.SetTitle(Original);
            var t = view.FindViewById<TextView>(Resource.Id.tranlation);
            t.Text = Translation;
            return view;
        }
    }
}