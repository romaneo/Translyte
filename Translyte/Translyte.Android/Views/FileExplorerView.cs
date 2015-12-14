using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Translyte.Core.DataProvider;
using Translyte.Core.DataProvider.SQLite;
using Translyte.Core.Models;
using SupportFragment = Android.Support.V4.App.Fragment;
using EnvironmentAnd = Android.OS.Environment;
using Environment = System.Environment;

namespace Translyte.Android.Views
{
	[Activity(Label = "FileExplorer")]
	public class FileExplorerView : SupportFragment, View.IOnClickListener
	{
		private FileExplorerAdapter adapter;
		public TranslyteDbGateway TranslyteDbGateway { get; set; }
		Connection conn;
		View ParentView { get; set; }

		List<BookReviewModel> _books = new List<BookReviewModel>();

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View parentView = inflater.Inflate(Resource.Layout.FileExplorerView, container, false);
			ParentView = parentView;
			Java.IO.File dir =  new Java.IO.File(EnvironmentAnd.ExternalStorageDirectory.AbsolutePath + @"/translyte");
			var s = dir.ListFiles ();

			var sqliteFilename = "TaskDB.db3";
			string libraryPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var path = Path.Combine(libraryPath, sqliteFilename);
			conn = new Connection(path);
			TranslyteDbGateway = new TranslyteDbGateway(conn);
			var localBooks = TranslyteDbGateway.GetBooksLocalReview ();
			foreach(var t in s)
			{
				var isLocal = localBooks.Any (ss=>ss.BookPath.Equals(t.AbsolutePath));
				_books.Add (new BookReviewModel(){BookPath = t.AbsolutePath, IsLocal = isLocal, Title = t.Name});
			}
			adapter = new FileExplorerAdapter(parentView.Context, _books);
			ListView listView = parentView.FindViewById<ListView>(Resource.Id.FilesListView);
			listView.Adapter = adapter;
			listView.ItemClick += OnListItemClick;

			return parentView;
		}
		public void OnClick(View v)
		{
			throw new NotImplementedException();
		}

		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var book = _books[e.Position];
			if (book.BookPath.Contains (".fb2") && !book.IsLocal) {
				book.IsLocal = true;
				//var curBook = new BookReviewModel(fileSystemInfo.FullName);	
				TranslyteDbGateway.SaveBookLocal (new BookLocal () { BookPath = book.BookPath, Position = 0, IsCurrent = true });
				TranslyteDbGateway.SetCurrentBook (book);
				Toast.MakeText(this.Activity, "Loading...", ToastLength.Short).Show();

				Java.IO.File dir =  new Java.IO.File(EnvironmentAnd.ExternalStorageDirectory.AbsolutePath + @"/translyte");
				var s = dir.ListFiles ();

				_books = new List<BookReviewModel> ();
				var localBooks = TranslyteDbGateway.GetBooksLocalReview ();
				foreach(var t in s)
				{
					var isLocal = localBooks.Any (ss=>ss.BookPath.Equals(t.AbsolutePath));
					_books.Add (new BookReviewModel(){BookPath = t.AbsolutePath, IsLocal = isLocal, Title = t.Name});
				}

				adapter = new FileExplorerAdapter(ParentView.Context, _books);
				ListView listView = ParentView.FindViewById<ListView>(Resource.Id.FilesListView);
				listView.Adapter = adapter;
				listView.ItemClick += OnListItemClick;
				/*this.FragmentManager
					.BeginTransaction().AddToBackStack(null)
					.Replace(Resource.Id.main_fragment, new BookView(), "fragment")
					.SetTransitionStyle(global::Android.Support.V4.App.FragmentTransaction.TransitFragmentFade)
					.Commit();*/
			} else
				Toast.MakeText(this.Activity, "This book has been already added.", ToastLength.Short).Show();
			/*var book = _books[e.Position];
			Book curBook = new BookReviewModel(book.BookPath);
			//curBook.BookPath = book.BookPath;
			Book.Load(ref curBook);
			//Book res = (BookReviewModel) curBook;
			//var intent = new Intent(this, typeof(BookView));
			string jsonModel = JsonConvert.SerializeObject(book);
			// intent.PutExtra("book", jsonModel);
			//StartActivity(intent);
			var fragment = new BookView();
			Bundle bundle = new Bundle();
			bundle.PutString("book", jsonModel);
			fragment.Arguments = bundle;
			ChangeFragment(fragment);
			TranslyteDbGateway.SetCurrentBook(curBook);
			if (curBook.ID != CurrentBook.ID)
			{
				CurrentBook = (BookReviewModel)curBook;
				_books.Clear();
				_books = TranslyteDbGateway.GetBooksLocalReviewWithoutCurrent();
			}*/
		}
	}
}

