using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware.Display;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Speech;
using Java.Lang;
using WordSelection.Adapters;
using WordSelection.ViewModels;
using Math = System.Math;

namespace WordSelection
{
    [Activity(Label = "WordSelection", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //TextView book = FindViewById<TextView>(Resource.Id.tv_book);
            //book.SetTextIsSelectable(true);
            //book.CustomSelectionActionModeCallback = new MySelectionCallBack(book);

            List<GalleryBookItem> items = new List<GalleryBookItem>();
            items.Add(new GalleryBookItem()
            {
                Author = "android book",
                Title = "android",
                IsAvailable = true,
                Cover = Resource.Drawable.book1
            });
            items.Add(new GalleryBookItem()
            {
                Author = "book1",
                Title = "book1 author",
                IsAvailable = false,
                Cover = Resource.Drawable.book1
            });
            items.Add(new GalleryBookItem()
            {
                Author = "book2",
                Title = "book2 author",
                IsAvailable = false,
                Cover = Resource.Drawable.book2
            });
            items.Add(new GalleryBookItem()
            {
                Author = "book3",
                Title = "book3 author",
                IsAvailable = false,
                Cover = Resource.Drawable.book2
            });
            items.Add(new GalleryBookItem()
            {
                Author = "book4",
                Title = "book4 author",
                IsAvailable = true,
                Cover = Resource.Drawable.book2
            });
            GalleryAdapter adapter = new GalleryAdapter(this, items);

            ListView listView = FindViewById<ListView>(Resource.Id.ListView);

            listView.Adapter = adapter;
        }
    }
}

