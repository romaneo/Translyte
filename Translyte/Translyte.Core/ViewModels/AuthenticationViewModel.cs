using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using Parse;
using Translyte.Core.Models;
using Translyte.Core.Parse;

namespace Translyte.Core.ViewModels
{
    class AuthenticationViewModel : MvxViewModel
    {
        private readonly ParseUser _currentUser;
        private string _password;
        public AuthenticationViewModel()
        {
            _currentUser = new ParseUser();
        }

        public string UserName
        {
            get { return _currentUser.Username;}
            set
            {
                _currentUser.Username = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _currentUser.Password = value;
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }


        public IMvxCommand SignInCommand { get { return new MvxCommand(SignIn, () => true); } }

        private void SignIn()
        {
            ParseAdapter.SignIn(_currentUser.Username, _password);
            ShowViewModel<BookViewModel>();
        }
    }
}
