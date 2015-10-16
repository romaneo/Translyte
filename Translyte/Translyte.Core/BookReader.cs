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
            
            var nodes = xdoc.Root.Elements();
            var titleInfo = nodes.Where(x=>x.Name.LocalName=="description").SingleOrDefault()
                .Elements().Where(x=>x.Name.LocalName=="title-info").SingleOrDefault().Elements();
            book.Title = titleInfo.Where(x => x.Name.LocalName == "book-title").SingleOrDefault().Value;
            book.Author = titleInfo.Where(x => x.Name.LocalName == "author").SingleOrDefault().Elements()
                .Where(x => x.Name.LocalName == "first-name").SingleOrDefault().Value
                +" "+ titleInfo.Where(x => x.Name.LocalName == "author").SingleOrDefault().Elements()
                .Where(x => x.Name.LocalName == "last-name").SingleOrDefault().Value;
            book.Annotation = titleInfo.Where(x=>x.Name.LocalName=="annotation").SingleOrDefault().Value;
            book.Cover = titleInfo.Where(x => x.Name.LocalName == "coverpage").SingleOrDefault().Elements()
                .First().FirstAttribute.Value;
            book.Language = titleInfo.Where(x => x.Name.LocalName == "lang").SingleOrDefault().Value;
            book.Date = titleInfo.Where(x => x.Name.LocalName == "date").SingleOrDefault().Value;
            book.ID = nodes.Where(x=>x.Name.LocalName=="description").SingleOrDefault().Elements()
                .Where(x=>x.Name.LocalName=="document-info").SingleOrDefault().Elements()
                .Where(x => x.Name.LocalName == "id").SingleOrDefault().Value;
            book.Genres = 
                new List<string>(titleInfo.Where(x => x.Name.LocalName == "genre").Select(x => x.Value));
            book.Chapters = new List<ChapterModel>(nodes.Where(x => x.Name.LocalName == "body").First().Elements()
                .Where(x => x.Name.LocalName == "section").Select(x => new ChapterModel()
                {
                    //Title = x.Elements().Where(y => y.Name.LocalName == "title").SingleOrDefault().Value,
                    Content = x.Value
                }));
            return book;
        }
    }
}
