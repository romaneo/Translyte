using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translyte.Core.DataProvider.SQLite;
using Translyte.Core.DataProvider.SQLiteBase;
using Translyte.Core.Models;
using Translyte.Core.Parse;

namespace Translyte.Core.DataProvider
{
    public class TranslyteDbGateway
    {
        private BookRepository _repository;
        private ParseAdapter _parseAdapter = new ParseAdapter();
        public List<BookReviewModel> GetSyncLocalBooks()
        {
            var remoteBooks = _parseAdapter.GetBooks();
            var localBooks = _parseAdapter.GetBooks();
            // logic comparing
            var books = new List<BookReviewModel>();
            return books;
        }
        

        public TranslyteDbGateway(SQLiteConnection conn) 
        {
            _repository = new BookRepository(conn, "");
        }

        public BookLocal GetBookLocal(int id)
		{
            return _repository.GetBookLocal(id);
		}

        public IList<BookLocal> GetBooksLocal()
		{
            return new List<BookLocal>(_repository.GetBooksLocal());
		}

        public int SaveBookLocal(BookLocal item)
		{
            return _repository.SaveBookLocal(item);
		}
		
		public int DeleteTask(int id)
		{
            return _repository.DeleteBookLocal(id);
		}
        
    }
}
