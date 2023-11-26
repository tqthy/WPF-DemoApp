using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_DemoApp.Model;
using WPF_DemoApp.Repositories;

namespace WPF_DemoApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        // Fields
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;
        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            _currentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }
        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                _currentUserAccount.Username = user.Username;
                _currentUserAccount.DisplayName = $"Welcome {user.Name} {user.LastName}!";
                _currentUserAccount.ProfilePicture = null;
            } else
            {
                _currentUserAccount.DisplayName = "Invalid user, not logged in";
            }
        }
    }
}
