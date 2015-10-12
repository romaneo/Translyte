using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Translyte.Core.Models;
using Cirrious.MvvmCross.Plugins.File;

namespace Translyte.Core
{
    public static class BookReader
    {
        public static BookModel Load(string path)
        {
            XDocument xdoc = XDocument.Load(path);
            BookModel book = new BookModel();
            book.Title = xdoc.Element("FictionBook").Element("description").Element("title-info").Element("author").Value;

            return book;
        }
    }
}
