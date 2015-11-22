using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Translyte.Core
{
    public abstract class Book
    {
        public Book(string bookPath)
        {
            BookPath = bookPath;
            Position = 0;
        }
        public int Position { get; set; }
        public string ID { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Cover { get; set; }

        public string BookPath { get; set; }        
    }
}
