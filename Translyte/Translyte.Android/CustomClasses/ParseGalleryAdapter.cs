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
using Translyte.Core.ViewModels;

namespace Translyte.Android.CustomClasses
{
    class ParseGalleryAdapter : BaseAdapter<BookViewModel>
    {
        private List<BookViewModel> _items;
        private Context _context;

        public ParseGalleryAdapter(Context context, List<BookViewModel> items)
        {
            _items = items;
            _context = context;
        }

        public override BookViewModel this[int position]
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

            ImageView image = row.FindViewById<ImageView>(Resource.Id.Cover);
            image.SetImageResource(_items[position].Cover);

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
            get { return this.Count; }
        }
    }
}