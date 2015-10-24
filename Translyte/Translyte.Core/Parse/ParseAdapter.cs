using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                new ParseUser(){Username = userName, Password = userPasword}.SignUpAsync();
            }
            
            
        }


    }
}
