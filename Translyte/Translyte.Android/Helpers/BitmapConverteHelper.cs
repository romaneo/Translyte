using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Translyte.Android.Helpers
{
    public class BitmapConverteHelper
    {
        public Bitmap GetCover(string imageText)
        {
            Byte[] bitmapData = Convert.FromBase64String(FixBase64ForImage(imageText));
            var imageBitmap = BitmapFactory.DecodeByteArray(bitmapData, 0, bitmapData.Length);
            return imageBitmap;
        }


        private string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);
            sbText.Replace("\r\n", String.Empty); sbText.Replace(" ", String.Empty);
            return sbText.ToString();
        }

    }
}