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
    public class ParseAdapter
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

        public async void AddBook(BookReviewModel book)
        {
            XDocument xdoc = XDocument.Load(book.BookPath);
            Stream str = new MemoryStream();
            xdoc.Save(str);
            str.Position = 0;
            ParseFile file = new ParseFile(book.Title, str);
            await file.SaveAsync();

            ParseObject newFile = new ParseObject("BookFile");
            newFile["File"] = file;
            await newFile.SaveAsync();

            var newBook = new ParseObject("Book");
            newBook["Author"] = book.Author;
            newBook["Title"] = book.Title;
            newBook["LocalBookId"] = book.ID;
            newBook["Position"] = book.Position;
            newBook["File"] = file;
            newBook["User"] = ParseUser.CurrentUser;
            await newBook.SaveAsync();
        }

        public async Task<string> GetBookByLocalId(string localBookId)
        {
            var query = ParseObject.GetQuery("Book")
    .WhereEqualTo("localBookId", localBookId);
            ParseObject results = await query.FirstOrDefaultAsync();
            var s = results.ClassName;
            return results.ToString();
        }

    }
}
