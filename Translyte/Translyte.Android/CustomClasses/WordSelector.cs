﻿using Android.App;
using Android.Views;
using Android.Widget;
using Math = System.Math;

namespace Translyte.Android.CustomClasses
{
    public class WordSelector : Java.Lang.Object, ActionMode.ICallback
    {
        public WordSelector (TextView book, Activity activity)
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
                    // Perform your definition lookup with the selected text
                    var selectedText = book.Text.Substring(min, max-min);
                    Toast.MakeText(ParentActivity, selectedText, ToastLength.Short).Show();
                    // Finish and close the ActionMode
                    mode.Finish();
                    return true;
                default:
                    break;
            }
            return false;
        }

        public Activity ParentActivity { get; set; }

        public bool OnCreateActionMode(ActionMode mode, IMenu menu)
        {
           ParentActivity.MenuInflater.Inflate(Resource.Drawable.optionsmenu, menu);            
            //menu.Add(0, 0, 0, "Translate").SetIcon(Resource.Drawable.translate);
            return true;
        }

        public void OnDestroyActionMode(ActionMode mode)
        {
        }

        public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
        {
            menu.RemoveItem(16908319);//Resource.Id.SelectAll);
            //menu.RemoveItem(Resource.Id.Cut);//17039363);
            //menu.RemoveItem(Resource.Id.Copy);//16908321);
            return true;
        }

    }
}