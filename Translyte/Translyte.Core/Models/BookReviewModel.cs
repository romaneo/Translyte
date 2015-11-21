using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Translyte.Core.Models
{
    public class BookReviewModel : Book
    {
        public BookReviewModel(string bookPath) : base(bookPath) { }

        public BookReviewModel() : base("") { }

    }
}
