using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Translyte.Core.DataProvider;
using Translyte.Core.DataProvider.SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Translyte.Android.CustomClasses;
using Translyte.Android.Helpers;
using Translyte.Core.Models;
using Environment = System.Environment;

namespace Translyte.Android.Views
{
    public class FileListFragment : ListFragment
    {
        public static readonly string DefaultInitialDirectory = "/";
        private FileListAdapter _adapter;
        private DirectoryInfo _directory;

		public TranslyteDbGateway TranslyteDbGateway { get; set; }
		Connection conn;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _adapter = new FileListAdapter(Activity, new FileSystemInfo[0]);
            ListAdapter = _adapter;
			var sqliteFilename = "TaskDB.db3";
			string libraryPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var path = Path.Combine(libraryPath, sqliteFilename);
			conn = new Connection(path);
			TranslyteDbGateway = new TranslyteDbGateway(conn);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var fileSystemInfo = _adapter.GetItem(position);

            if (fileSystemInfo.IsFile())
            {
                // Do something with the file.  In this case we just pop some toast.
                Log.Verbose("FileListFragment", "The file {0} was clicked.", fileSystemInfo.FullName);
				Toast.MakeText(Activity, "You selected file " + fileSystemInfo.Extension, ToastLength.Short).Show();
				if (fileSystemInfo.Extension.Equals(".fb2"))
				{
					var curBook = new BookReviewModel(fileSystemInfo.FullName);	
					TranslyteDbGateway.SaveBookLocal(new BookLocal() { BookPath = fileSystemInfo.FullName, Position = 0, IsCurrent = true });
					TranslyteDbGateway.SetCurrentBook(curBook);
				}
            }
            else
            {
                // Dig into this directory, and display it's contents
                RefreshFilesList(fileSystemInfo.FullName);
            }

            base.OnListItemClick(l, v, position, id);
        }

        public override void OnResume()
        {
            base.OnResume();
            RefreshFilesList(DefaultInitialDirectory);
        }

        public void RefreshFilesList(string directory)
        {
            IList<FileSystemInfo> visibleThings = new List<FileSystemInfo>();
            var dir = new DirectoryInfo(directory);

            try
            {
                foreach (var item in dir.GetFileSystemInfos().Where(item => item.IsVisible()))
                {
                    visibleThings.Add(item);
                }
            }
            catch (Exception ex)
            {
                Log.Error("FileListFragment", "Couldn't access the directory " + _directory.FullName + "; " + ex);
                Toast.MakeText(Activity, "Problem retrieving contents of " + directory, ToastLength.Long).Show();
                return;
            }

            _directory = dir;

            _adapter.AddDirectoryContents(visibleThings);

            // If we don't do this, then the ListView will not update itself when then data set 
            // in the adapter changes. It will appear to the user that nothing has happened.
            ListView.RefreshDrawableState();

            Log.Verbose("FileListFragment", "Displaying the contents of directory {0}.", directory);
        }
    }
}