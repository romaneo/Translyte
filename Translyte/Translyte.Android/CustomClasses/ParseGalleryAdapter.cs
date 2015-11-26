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
using Translyte.Core.Models;
using Translyte.Core.ViewModels;

namespace Translyte.Android.CustomClasses
{
    class ParseGalleryAdapter : BaseAdapter<BookReviewModel>
    {
        private List<BookReviewModel> _items;
        private Context _context;

        public ParseGalleryAdapter(Context context, List<BookReviewModel> items)
        {
            _items = items;
            _context = context;
        }

        public override BookReviewModel this[int position]
        {
            get { return _items[position]; }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.BookItemView, null, false);

            TextView title = row.FindViewById<TextView>(Resource.Id.Title);
            title.Text = _items[position].Title;

            TextView author = row.FindViewById<TextView>(Resource.Id.Author);
            author.Text = _items[position].Author;           

            return row;
        }

        public override int GetItemViewType(int position)
        {
            return position; //(position == this.Count - 1) ? 1 : 0;
        }

        public override int ViewTypeCount
        {
            get { return 1; }
        }
    }
}