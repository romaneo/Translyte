using Android.OS;
using Android.Widget;
using SupportFragment = Android.Support.V4.App.Fragment;
using Android.Views;
//using SherlockActionBar = Xamarin.ActionbarSherlockBinding.App.ActionBar;

namespace WordSelection
{
    public class HomeFragment : SupportFragment
    {
        MainActivity ParentActivity
        {
            get;
            set;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var parentView = inflater.Inflate(Resource.Layout.Home, container, false);
            MainActivity parentActivity = Activity as MainActivity;
            ParentActivity = parentActivity;
            HasOptionsMenu = true;
            var resideMenu = parentActivity.ResideMenu;
            TextView textView = parentView.FindViewById<TextView>(Resource.Id.rmt);
            textView.SetTextIsSelectable(true);
            textView.CustomSelectionActionModeCallback = new MySelectionCallBack(textView, ParentActivity);

            parentView.FindViewById(Resource.Id.rmt).Click += (s, e) => resideMenu.OpenMenu(global::AndroidResideMenu.ResideMenu.Direction.Left);

           // FrameLayout ignoredView = parentView.FindViewById<FrameLayout>(Resource.Id.ignored_view);
          //  resideMenu.AddIgnoredView(ignoredView);

            return parentView;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            //var subMenu1 = menu.AddSubMenu("Action Item");
            //subMenu1.Add("Sample");
            //subMenu1.Add("Menu");
            //subMenu1.Add("Items");

            //var subMenu1Item = subMenu1.Item;
            //subMenu1Item.SetIcon(Resource.Drawable.icon_home);
            //subMenu1Item.SetShowAsAction(ShowAsAction.Always|ShowAsAction.WithText);

            //var subMenu2 = menu.AddSubMenu("Overflow Item");
            //subMenu2.Add("These");
            //subMenu2.Add("Are");
            //subMenu2.Add("Sample");
            //subMenu2.Add("Items");

            //var subMenu2Item = subMenu2.Item;
            //subMenu2Item.SetIcon(Resource.Drawable.Library);
           // ParentActivity.MenuInflater.Inflate(Resource.Drawable.optionsmenu, menu);
            inflater.Inflate(Resource.Drawable.optionsmenu, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        

        public override void OnDestroyOptionsMenu()
        {
            //TextView text = ParentActivity.FindViewById<TextView>(Resource.Id.rmt);
            //int start = text.SelectionStart;
            //int ent = text.SelectionEnd;
            //string tx = text.Text.Substring(start, ent - start);
            Toast.MakeText(ParentActivity, "blabla", ToastLength.Short).Show();
            base.OnDestroyOptionsMenu();
        }
    }
}