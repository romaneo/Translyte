using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Parse;
using Translyte.Core.Models;

namespace Translyte.Core.Parse
{
    class ParseAdapter
    {
        private const string AppId = "5iwX6JgTYmHJcwUsbbbtJYy0sRf8OOns8CLdMcqz";
        private const string DotNetKey = "6MeGivIrAf2tvIUhTXLeIJ9ShwrUKgtPxAoUai4g";

        public static void InitializeApp()
        {
            try
            {
                ParseClient.Initialize(AppId, DotNetKey);
            }
            catch (Exception)
            {
                
                Debug.WriteLine("Parse app init error.");
            }
        }

        public static async void SignIn(string userName, string userPasword)
        {
            try
            {
                await ParseUser.LogInAsync(userName, userPasword);
            }
            catch (Exception)
            {
                new ParseUser() { Username = userName, Password = userPasword }.SignUpAsync();
            }
            
        }

        //public static async void SaveBook()
        //{
        //    XDocument xdoc = XDocument.Load("/sdcard/translyte/gg.fb2");
        //    Stream str = new MemoryStream();
        //    xdoc.Save(str);
        //    str.Position = 0;
        //    ParseFile file = new ParseFile("gg.fb2", str);
        //    await file.SaveAsync();

        //    ParseObject newFile = new ParseObject("BookFile");
        //    newFile["File"] = file;
        //    await newFile.SaveAsync();
        //}

    }
}
