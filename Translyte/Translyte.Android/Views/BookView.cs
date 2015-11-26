using System.Threading;
using Android.App;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using Translyte.Android.CustomClasses;
using Translyte.Core;
using Translyte.Core.Models;
using Translyte.Core.ViewModels;

namespace Translyte.Android.Views
{
    [Activity(Label = "Book")]
    public class BookView : Activity
    {
        public BookFullModel CurrentBook { get; set; }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.BookView);
            TextView content = FindViewById<TextView>(Resource.Id.tv_book);
            var title = FindViewById<TextView>(Resource.Id.tv_title);
            content.SetTextIsSelectable(true);
            content.CustomSelectionActionModeCallback = new WordSelector(content);
            var extraData = Intent.GetStringExtra("book");
            if (extraData != null)
            {

                var tempBook = JsonConvert.DeserializeObject<BookReviewModel>(extraData);
                title.Text = tempBook.Title;
                RunOnUiThread(() => 
                {
                    Thread.CurrentThread.IsBackground = true;
                    Book curBook = new BookFullModel(tempBook.BookPath);
                    Book.Load(ref curBook);
                    content.Text = ((BookFullModel)curBook).Chapters[0].Content;
                });
                
                
            }
                
            
        }
    }
}