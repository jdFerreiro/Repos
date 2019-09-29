using Prism.Navigation;
using SICONDEP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SICONDEP.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private INavigationService NavigationService { get; }

        public AuthenticationService(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
        }
    
        public bool Login(string userCode, string userPassword)
        {
            /*
             *  Incorporar la autheticación de usuario
            */

            UserLoggedViewModel.Name = "Demo User";

            return true;
        }

        public void Logout()
        {
            NavigationService.NavigateAsync("/Login");
        }
    }
}
