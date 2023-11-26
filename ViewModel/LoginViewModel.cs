using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_DemoApp.Model;
using WPF_DemoApp.Repositories;

namespace WPF_DemoApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _userName;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        public string UserName 
        { 
            get => _userName; set { _userName = value; OnPropertyChanged(nameof(UserName)); }
        }
        public SecureString Password 
        { 
            get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        public string ErrorMessage
        { 
            get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }
        public bool IsViewVisible 
        { 
            get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); }
        }

        // -> Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        // Constructors

        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPasswordCommand("", ""));
        }

        private void ExecuteRecoverPasswordCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            return ValidAccount();
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(UserName, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(UserName), null);
                IsViewVisible = false;
            } else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }



        // Utility methods
        private bool ValidAccount()
        {
            if (string.IsNullOrWhiteSpace(UserName) || Password == null || UserName.Length < 3 || Password.Length < 3) return false;
            return true;
        }
    }
}
