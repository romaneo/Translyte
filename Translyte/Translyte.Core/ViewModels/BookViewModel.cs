using Cirrious.MvvmCross.ViewModels;
using Translyte.Core.Models;
using System;
using Cirrious.MvvmCross.Plugins.File;

namespace Translyte.Core.ViewModels
{
    public class BookViewModel 
		: MvxViewModel
    {
        public BookViewModel()
        {
<<<<<<< HEAD
            Path = "/sdcard/Oz.fb2";
            _book = new BookFullModel(Path);
            Book.Load(ref _book);
            _title = ((BookFullModel)_book).Chapters[1].Title;
            _content = ((BookFullModel)_book).Chapters[1].Content;
=======
            Path = "/sdcard/translyte/gg.fb2";
            _book = BookReader.Load(Path);
            _title = _book.Chapters[1].Title;
            _content = _book.Chapters[1].Content;
            
>>>>>>> origin/master
        }
        
        private Book _book;
        public string Path { get; set; }
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }
        private string _content="Book's content";
        public string Content { 
            get { return _content; } 
            set { 
                _content = value; 
                RaisePropertyChanged(()=>Content); 
            } 
        }
    }
}
