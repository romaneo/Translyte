using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Translyte.Android;
using Translyte.Android.Helpers;
using Translyte.Core.Models;
using Translyte.Core.ViewModels;
using Object = Java.Lang.Object;

namespace Translyte.Android.CustomClasses
{
    class GalleryAdapter : BaseAdapter<BookReviewModel>
    {
        private List<BookReviewModel> _items;
        private Context _context;

        public GalleryAdapter(Context context, List<BookReviewModel> items)
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

            ImageView image = row.FindViewById<ImageView>(Resource.Id.Cover);
            if (_items[position].Cover != null)
            {
                var img = new BitmapConverteHelper().GetCover(_items[position].Cover);
                image.SetImageBitmap(img);
            }
            

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