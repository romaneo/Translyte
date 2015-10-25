using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Translyte.Core.Models
{
    public class BookFullModel : Book
    {
        public BookFullModel(string bookPath) : base(bookPath) { }

        public List<string> Genres { get; set; }

        public List<ChapterModel> Chapters { get; set; }

        public string Annotation { get; set; }

        public string Year { get; set; }

        public string Language { get; set; }
    }
}
