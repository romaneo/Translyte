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

        public BookReviewModel GetBookByLocalId(string localBookId)
        {
            var query = ParseObject.GetQuery("Book")
    .WhereEqualTo("LocalBookId", localBookId);
            var result = query.FirstOrDefaultAsync().GetAwaiter().GetResult();

            var bookResult = new BookReviewModel();
            bookResult.Position = result.Get<int>("Position");
            bookResult.Author = result.Get<string>("Author");
            bookResult.Title = result.Get<string>("Title");
            bookResult.ID = result.Get<string>("LocalBookId");
            return bookResult;
        }

        public List<BookReviewModel> GetBooks()
        {
            var query = ParseObject.GetQuery("Book");

            var result = query.FindAsync().GetAwaiter().GetResult();
            var books = new List<BookReviewModel>();
            var book = new BookReviewModel();
            foreach (var res in result)
            {
                book.Position = res.Get<int>("Position");
                book.Author = res.Get<string>("Author");
                book.Title = res.Get<string>("Title");
                book.ID = res.Get<string>("LocalBookId");
                books.Add(book);
            }
            return books;
        }

    }
}
