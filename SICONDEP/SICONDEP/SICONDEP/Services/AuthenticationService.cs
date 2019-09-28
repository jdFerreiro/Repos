using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SICONDEP.Services
{
    public class AuthenticationService : IAutheticationService
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

            return true;
        }

        public void Logout()
        {
            NavigationService.NavigateAsync("/Login");
        }
    }
}
