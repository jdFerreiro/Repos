using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SICONDEP.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {

        #region Properties
        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
        #endregion
        public LoginPageViewModel()
        {
            email = string.Empty;
            password = string.Empty;

        }
    }
}
