using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Translyte.Core.Models
{
    public class BookModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Name { get { return Author + " - " + Title; }  set { } }
        public string Cover { get; set; }
        public List<string> Genres { get; set; }
        public int Year { get; set; }
        public string Language { get; set; }
        public string Annotation { get; set; }
        public List<ChapterModel> Chapters { get; set; }
    }
}
