using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;

namespace Translyte.Core.ViewModels
{
    class LibraryViewModel
    {
        public LibraryViewModel()
        {
            BooksList = new List<BookStatus>();
            BooksList.Add(new BookStatus(){Name = "sadf", Status = true});
        }

        public List<BookStatus> BooksList { get; set; }
    }

    public class BookStatus
    {
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
