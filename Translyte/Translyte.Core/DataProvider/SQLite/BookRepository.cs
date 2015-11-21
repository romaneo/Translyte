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
    }
}
