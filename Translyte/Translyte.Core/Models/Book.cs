using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Translyte.Core.Parsers;

namespace Translyte.Core
{
    //This class represents base book model.
    public abstract class Book
    {
        public Book(string bookPath)
        {
            BookPath = bookPath;
            Position = 0;
        }
        
        public string ID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string BookPath { get; set; }
        public int Position { get; set; }
        //This function loads books different types by using
        //appropriate parser.
        public static void Load(ref Book book)
        {
            IParser parser;
            var ext = book.BookPath.Substring(book.BookPath.LastIndexOf(".")+1);
            switch (ext)
            {
                case "fb2":
                    parser = new FB2Parser();
                    break;
                case "epub":
                    parser = new EPUBParser();
                    break;
                case "mobi":
                    parser = new MOBIParser();
                    break;
                default: return;
            }

            parser.Parse(ref book);
        }
    }
}
