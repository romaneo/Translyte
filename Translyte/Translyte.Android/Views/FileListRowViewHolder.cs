using Android.Widget;

namespace Translyte.Android.Views
{
    public class FileListRowViewHolder : Java.Lang.Object
    {
        public FileListRowViewHolder(TextView textView, ImageView imageView)
        {
            TextView = textView;
            ImageView = imageView;
        }

        public ImageView ImageView { get; private set; }
        public TextView TextView { get; private set; }

        /// <summary>
        ///   This method will update the TextView and the ImageView that are
        ///   are
        /// </summary>
        /// <param name="fileName"> </param>
        /// <param name="fileImageResourceId"> </param>
        public void Update(string fileName, int fileImageResourceId)
        {
            TextView.Text = fileName;
			if (fileImageResourceId!=0)
            ImageView.SetImageResource(fileImageResourceId);
        }
    }
}