using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Translyte.Android.Views
{
    [Activity(Label = "Settings")]
    public class SettingsView : SupportFragment, View.IOnClickListener
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View parentView = inflater.Inflate(Resource.Layout.SettingsView, container, false);

			Switch s = parentView.FindViewById<Switch> (Resource.Id.themeSwitch);
			ISharedPreferences pref = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
			var isDark = pref.GetBoolean("themeDark", false);
			s.Checked = isDark;

			s.CheckedChange += delegate(object sender, CompoundButton.CheckedChangeEventArgs e) {
				ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
				ISharedPreferencesEditor editor = prefs.Edit();
				editor.PutBoolean("themeDark", e.IsChecked);
				editor.Apply();
			};
			var s2 = parentView.FindViewById<Switch> (Resource.Id.appThemeSwitch);
			//ISharedPreferences pref = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
			isDark = pref.GetBoolean("appThemeDark", false);
			s2.Checked = isDark;

			s2.CheckedChange += delegate(object sender, CompoundButton.CheckedChangeEventArgs e) {
				ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
				ISharedPreferencesEditor editor = prefs.Edit();
				editor.PutBoolean("appThemeDark", e.IsChecked);
				editor.Apply();

			};
            //var themeRadioButton = parentView.FindViewById<RadioButton>(Resource.Id.themeRadioButton);

            /*themeRadioButton.Click += delegate
            {
                ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutBoolean("themeDark", themeRadioButton.Checked);
                editor.Apply();
            };
            var fontRadioButton = parentView.FindViewById<RadioButton>(Resource.Id.fontRadioButton);

            themeRadioButton.Click += delegate
            {
                ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutBoolean("fontLarge", themeRadioButton.Checked);
                editor.Apply();
            };*/

            Spinner textSizeSpinner = parentView.FindViewById<Spinner>(Resource.Id.textSize);
			//pref = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
			var size = pref.GetInt("textSize", 20);
			//textSizeSpinner.
			textSizeSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);

            var adapter1 = ArrayAdapter.CreateFromResource(Activity.BaseContext, Resource.Array.size_array, Resource.Layout.Spinner);//global::Android.Resource.Layout.SimpleSpinnerDropDownItem);
            adapter1.SetDropDownViewResource(global::Android.Resource.Layout.SimpleSpinnerDropDownItem);
            textSizeSpinner.Adapter = adapter1;
			textSizeSpinner.SetSelection(adapter1.GetPosition(size.ToString()));


            Spinner textFontSpinner = parentView.FindViewById<Spinner>(Resource.Id.textFont);
			var font = pref.GetString("textFont", "Verdana");
			//textSizeSpinner.
			textFontSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected2);

            var adapter2 = ArrayAdapter.CreateFromResource(Activity.BaseContext, Resource.Array.font_array, Resource.Layout.Spinner);//global::Android.Resource.Layout.SimpleSpinnerDropDownItem);
            adapter2.SetDropDownViewResource(global::Android.Resource.Layout.SimpleSpinnerDropDownItem);
            textFontSpinner.Adapter = adapter2;

            return parentView;
        }
		private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;
			var s = spinner.GetItemAtPosition (e.Position);
			int textSize = Int32.Parse(spinner.GetItemAtPosition (e.Position).ToString());

			ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
			ISharedPreferencesEditor editor = prefs.Edit();
			editor.PutInt("textSize", textSize);
			editor.Apply();
			//Toast.MakeText (this, toast, ToastLength.Long).Show ();
		}
		private void spinner_ItemSelected2 (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;
			var s = spinner.GetItemAtPosition (e.Position);
			string textFont = spinner.GetItemAtPosition (e.Position).ToString();

			ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
			ISharedPreferencesEditor editor = prefs.Edit();
			editor.PutString("textFont", textFont);
			editor.Apply();
			//Toast.MakeText (this, toast, ToastLength.Long).Show ();
		}
        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }
    }
}