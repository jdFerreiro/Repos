using BCMWeb.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace BCMWeb.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Properties
        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string passw;
        public string Password
        {
            get { return passw; }
            set { SetProperty(ref passw, value); }
        }

        public ICommand LoginCommand { get; set; }


        #endregion

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IBatteryService batteryService)
            : base(navigationService, pageDialogService, batteryService)
        {
            LoginCommand = new DelegateCommand(ValidateLoginUser).ObservesCanExecute(() => AllFieldsAreValid);
        }

        private async void ValidateLoginUser()
        {
            throw new NotImplementedException();
        }
    }
}
