using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translyte.Core.Models;
using Translyte.Core.Parse;

namespace Translyte.Core.DataProvider
{
    public class TranslyteDbGateway
    {
        private ParseAdapter _parseAdapter = new ParseAdapter();
        public List<BookReviewModel> GetSyncLocalBooks()
        {
            var remoteBooks = _parseAdapter.GetBooks();
            //var localBooks = _sqliteAdapter.GetBooks();
            var books = new List<BookReviewModel>();
            return books;
        }

        
    }
}
