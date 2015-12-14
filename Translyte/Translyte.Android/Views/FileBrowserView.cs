using System;
using Android.App;
using Android.OS;
using Android.Views;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Translyte.Android.Views
{
    [Activity(Label = "FileBrowser")]
    public class FileBrowserView : SupportFragment, View.IOnClickListener
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View parentView = inflater.Inflate(Resource.Layout.FileBrowserView, container, false);
            
            return parentView;
        }
        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }
    }
}