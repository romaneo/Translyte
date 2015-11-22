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
using WordSelection.ViewModels;

namespace WordSelection.Adapters
{
    class GalleryAdapter:BaseAdapter<GalleryBookItem>
    {
        private List<GalleryBookItem> _items;
        private Context _context;

        public GalleryAdapter(Context context, List<GalleryBookItem> items )
        {
            _items = items;
            _context = context;
        }

        public override GalleryBookItem this[int position]
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

            CheckBox checkBox = row.FindViewById<CheckBox>(Resource.Id.IsAvailable);
            checkBox.Checked = _items[position].IsAvailable;

            return row;
        }
    }
}