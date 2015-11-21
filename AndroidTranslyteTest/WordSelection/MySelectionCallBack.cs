using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WordSelection
{
    class MyNewCallBack: ActionMode.ICallback
    {

        public bool OnActionItemClicked(ActionMode mode, IMenuItem item)
        {
            throw new NotImplementedException();
        }

        public bool OnCreateActionMode(ActionMode mode, IMenu menu)
        {
            throw new NotImplementedException();
        }

        public void OnDestroyActionMode(ActionMode mode)
        {
            throw new NotImplementedException();
        }

        public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
        {
            throw new NotImplementedException();
        }

        public IntPtr Handle
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class MySelectionCallBack : ActionMode.ICallback, IJavaObject
    {

        public MySelectionCallBack (TextView book)
        {
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
                    // Finish and close the ActionMode
                    mode.Finish();
                    return true;
                default:
                    break;
            }
            return false;
        }

        public bool OnCreateActionMode(ActionMode mode, IMenu menu)
        {
            // Called when action mode is first created. The menu supplied
            // will be used to generate action buttons for the action mode

            // Here is an example MenuItem
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

        public IntPtr Handle
        {
            get { return Marshal.StringToHGlobalAnsi("Translyte"); }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}