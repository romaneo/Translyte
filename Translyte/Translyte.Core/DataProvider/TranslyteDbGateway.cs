using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translyte.Core.DataProvider.SQLite;
using Translyte.Core.DataProvider.SQLiteBase;
using Translyte.Core.Models;
using Translyte.Core.Parse;
using Translyte.Core.ViewModels;

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

        public List<BookViewModel> GetBooksLocal()
        {
            var localBooks = _repository.GetBooksLocal().ToList();
            //_repository.DeleteBookLocal(localBooks[0].ID);
            var resBooks = new List<BookViewModel>();
            foreach (var book in localBooks)
            {
                Book curBook = new BookReviewModel(book.BookPath);
                curBook.BookPath = book.BookPath;
                Book.Load(ref curBook);
                var resBook = new BookViewModel() { Author = curBook.Author, Path = curBook.BookPath, Title = curBook.Title, Cover = curBook.Cover};
                resBooks.Add(resBook);
            }
            return resBooks;
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
