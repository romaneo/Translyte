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

namespace Translyte.Android
{
	public class FileExplorerAdapter
			: BaseAdapter<BookReviewModel>
		{
			private List<BookReviewModel> _items;
			private Context _context;

		public FileExplorerAdapter(Context context, List<BookReviewModel> items)
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
				View row = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.FileListItemView, null, false);

			ImageView image = row.FindViewById<ImageView>(Resource.Id.file_picker_image);
			if (_items[position].IsLocal)
			image.SetImageResource (Resource.Drawable.fileSave);
			else 
				image.SetImageResource (Resource.Drawable.file);


			TextView title = row.FindViewById<TextView>(Resource.Id.file_picker_text);
			title.Text = _items[position].Title;
    

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

