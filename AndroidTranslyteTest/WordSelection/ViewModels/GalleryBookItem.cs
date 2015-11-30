using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Translyte.Core.Models;

namespace WordSelection.ViewModels
{
    class GalleryBookItem
    {
        private BookFullModel _book;
        public GalleryBookItem(BookFullModel book)
        {
            _book = book;
        }

        public GalleryBookItem()
        {
            
        }

        public int Cover { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
        //public Image Rate { get; set; }


    }
}