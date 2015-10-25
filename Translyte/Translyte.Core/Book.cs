using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Translyte.Core.Parsers;

namespace Translyte.Core
{
    public abstract class Book
    {
        public Book(string bookPath)
        {
            BookPath = bookPath;
        }

        public string ID { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Cover { get; set; }

        public string BookPath { get; set; }

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
                case "moby":
                    parser = new MOBYParser();
                    break;
                default: return;
            }

            parser.Parse(ref book);
        }
    }
}
