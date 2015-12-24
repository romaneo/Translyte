using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Translyte.Core.DataProvider.SQLiteBase;
using Translyte.Core.Models;

namespace Translyte.Core.DataProvider.SQLite
{
    public class BookRepository
    {
        BookDB db = null;
        protected static string dbLocation;
        //protected static TaskRepository me;

        public BookRepository(SQLiteConnection conn, string dbLocation)
        {
            db = new BookDB(conn, dbLocation);
        }

        public BookLocal GetBookLocal(int id)
        {
            return db.GetItem<BookLocal>(id);
        }

        public BookLocal GetCurrentBook()
        {
            return db.GetItems<BookLocal>().FirstOrDefault(x => x.IsCurrent == true);
        }

        public IEnumerable<BookLocal> GetBooksLocal()
        {
            return db.GetItems<BookLocal>();
        }

        public int SaveBookLocal(BookLocal item)
        {
            return db.SaveItem<BookLocal>(item);
        }

        public int DeleteBookLocal(int id)
        {
            return db.DeleteItem<BookLocal>(id);
        }

		public void DeleteAllBooks()
		{
			var books = GetBooksLocal ();
			foreach(var book in books)
				db.DeleteItem<BookLocal>(book.ID);
		}

        public void SetCurrentBook(BookLocal book)
        {
            var books = GetBooksLocal().ToList();
            foreach (var b in books)
            {
                b.IsCurrent = false;
                db.SaveItem<BookLocal>(b);
            }
            book.ID = GetBookByPath(book.BookPath).ID;
            db.SaveItem<BookLocal>(book);
            var s = GetBooksLocal();
        }

		public void UpdateBookPosition(BookLocal book)
		{
			var lBook = db.GetItems<BookLocal>().FirstOrDefault(x => x.BookPath == book.BookPath);
			lBook.Position = book.Position;
			db.SaveItem (lBook);
		}

        public BookLocal GetBookByPath(string path)
        {
            return  db.GetItems<BookLocal>().ToList().FirstOrDefault(x=>x.BookPath.Equals(path));

        }
    }
}
