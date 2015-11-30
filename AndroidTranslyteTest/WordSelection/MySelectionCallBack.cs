using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Lang;
using Java.Util;

using System;
using String = System.String;
using File = Java.IO.File;
using Math = System.Math;
using Uri = Android.Net.Uri;

namespace WordSelection
{

    public class MySelectionCallBack : Java.Lang.Object, ActionMode.ICallback
    {

        public MySelectionCallBack (TextView book, Activity activity)
        {
            ParentActivity = activity;
            this.book = book;
        }

        private TextView book;

        public bool OnActionItemClicked(ActionMode mode, IMenuItem item)
        {
            switch (item.ItemId) 
            {
                case 0:
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
            // Called when action mode is first created. The menu supplied
            // will be used to generate action buttons for the action mode            
            // Here is an example MenuItem
            //ParentActivity.MenuInflater.Inflate(Resource.Drawable.optionsmenu, menu);
            menu.Add(0, 0, 0, "Definition").SetIcon(Resource.Drawable.Icon);
            return true;
        }

        public void OnDestroyActionMode(ActionMode mode)
        {
        }

        public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
        {
            // Remove the "select all" option
            menu.RemoveItem(Android.Resource.Id.SelectAll);
            // Remove the "cut" option
            menu.RemoveItem(Android.Resource.Id.Cut);
            // Remove the "copy all" option
            menu.RemoveItem(Android.Resource.Id.Copy);
            return true;
        }

        //public IntPtr Handle
        //{
        //    get { return Marshal.StringToHGlobalAnsi("Translyte"); }
        //}

        //public void Dispose()
        //{
        //    //throw new NotImplementedException();
        //}
    }
}