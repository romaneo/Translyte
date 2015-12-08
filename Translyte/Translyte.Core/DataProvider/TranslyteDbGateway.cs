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

        public List<BookReviewModel> GetBooksLocalReview()
        {
            
            var localBooks = _repository.GetBooksLocal().ToList();
            //_repository.DeleteBookLocal(localBooks[1].ID);
            var resBooks = new List<BookReviewModel>();
            foreach (var book in localBooks)
            {
                Book curBook = new BookReviewModel(book.BookPath);
                Book.Load(ref curBook);
                resBooks.Add((BookReviewModel)curBook);
            }
            return resBooks;
		}

        public List<BookReviewModel> GetBooksLocalReviewWithoutCurrent()
        {
            //var s = GetBooksLocalReview();
            //foreach (var t in s)
            //{
            //    _repository.DeleteBookLocal(Int32.Parse(t.ID));
            //}
           
            //_repository.SaveBookLocal(new BookLocal() { BookPath = "/sdcard/translyte/gg.fb2", Position = 0, IsCurrent = false });
            //_repository.SaveBookLocal(new BookLocal() { BookPath = "/sdcard/translyte/Brodyagi_Dharmy.fb2", Position = 0, IsCurrent = true });
            var localBooks = _repository.GetBooksLocal().ToList();
            var resBooks = new List<BookReviewModel>();
            foreach (var book in localBooks)
            {
                if (!book.IsCurrent)
                {
                    Book curBook = new BookReviewModel(book.BookPath);
                    Book.Load(ref curBook);
                    resBooks.Add((BookReviewModel)curBook);
                }
                
            }
            return resBooks;
        }

        public BookReviewModel GetCurrentBook()
        {
            var localBook = _repository.GetCurrentBook();
            Book curBook = new BookReviewModel();
            if (localBook != null)
            {
                curBook.BookPath = localBook.BookPath;
                Book.Load(ref curBook);
            }
            return (BookReviewModel)curBook;
        }

        public int SaveBookLocal(BookLocal item)
		{
            return _repository.SaveBookLocal(item);
		}
		
		public int DeleteBook(int id)
		{
            return _repository.DeleteBookLocal(id);
		}

        public void SetCurrentBook(Book book)
        {
            //int id = 0;
            //Int32.TryParse(book.ID, out id);
            BookLocal local = new BookLocal() { BookPath = book.BookPath, Position = book.Position, IsCurrent = true};
            _repository.SetCurrentBook(local);
        }
        
    }
}
