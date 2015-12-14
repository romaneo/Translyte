using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Translyte.Core.Models
{
    //This class represents book cover that contains general
    //information about a book.
    public class BookReviewModel : Book
    {
        public BookReviewModel(string bookPath) 
            : base(bookPath) {}

        public BookReviewModel() 
            : base("") {}

    }
}
