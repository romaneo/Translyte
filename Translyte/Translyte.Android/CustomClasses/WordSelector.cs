using System.Threading.Tasks;
using Android.App;
using Android.Views;
using Android.Widget;
using Translyte.Android.Views;
using Translyte.Core;
using Math = System.Math;

namespace Translyte.Android.CustomClasses
{
    public class WordSelector : Java.Lang.Object, ActionMode.ICallback
    {
        public WordSelector(TextView book, Activity activity)
        {
            ParentActivity = activity;
            this.book = book;
        }

        private TextView book;

        public bool OnActionItemClicked(ActionMode mode, IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.translate:
                    int min = 0;
                    int max = book.Text.Length;
                    if (book.IsFocused)
                    {
                        int selStart = book.SelectionStart;
                        int selEnd = book.SelectionEnd;

                        min = Math.Max(0, Math.Min(selStart, selEnd));
                        max = Math.Max(0, Math.Max(selStart, selEnd));
                    }
                    var selectedText = book.Text.Substring(min, max - min);
                    try
                    {
                        var translator = new Translator("en", "ru");
                        var res = translator.Translate(selectedText).GetAwaiter().GetResult();
                        ShowEditDialog(selectedText, res);
                    }
                    catch (System.Exception)
                    {                        
                        Toast.MakeText(ParentActivity, "Missing internet connection", ToastLength.Short).Show();
                    }
                    mode.Finish();
                    return true;
                default:
                    break;
            }
            return false;
        }

        private void ShowEditDialog(string orign, string translate)
        {
            var fm = ParentActivity.FragmentManager.BeginTransaction();
            var translateDialog = new TranslateMessage
            {
                Original = orign,
                Translation = translate
            };
            translateDialog.Show(fm, "TranslateMessage");
        }

        public Activity ParentActivity { get; set; }

        public bool OnCreateActionMode(ActionMode mode, IMenu menu)
        {
            ParentActivity.MenuInflater.Inflate(Resource.Drawable.optionsmenu, menu);
            return true;
        }

        public void OnDestroyActionMode(ActionMode mode)
        {
        }

        public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
        {
            menu.RemoveItem(global::Android.Resource.Id.SelectAll);
            return true;
        }
    }
}