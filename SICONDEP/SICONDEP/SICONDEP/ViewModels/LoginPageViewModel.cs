using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SICONDEP.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SICONDEP.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private IAuthenticationService AuthenticationService { get; }
        private IPageDialogService PageDialogService { get; }

        public LoginPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            AuthenticationService = authenticationService;
            PageDialogService = pageDialogService;

            Title = "Ingreso";

            LoginCommand = new DelegateCommand(OnLoginCommandExecuted, CommandCanExecute)
                .ObservesProperty(() => UserCode)
                .ObservesProperty(() => UserPassword);

            ClearCommand = new DelegateCommand(OnClearCommandExecuted, CommandCanExecute);
        }

        private string userCode;
        public string UserCode
        {
            get { return userCode; }
            set { SetProperty(ref userCode, value); }
        }

        private string userPassword;
        public string UserPassword
        {
            get { return userPassword; }
            set { SetProperty(ref userPassword, value); }
        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand ClearCommand { get; }

        private bool CommandCanExecute() =>
            !string.IsNullOrEmpty(UserCode.Trim()) && !string.IsNullOrEmpty(UserPassword.Trim()) && IsNotBusy;

        private async void OnLoginCommandExecuted()
        {
            IsBusy = true;
            if (AuthenticationService.Login(UserCode, UserPassword))
            {
                await NavigationService.NavigateAsync("/PrincipalPage");
            }
            else
            {
                await PageDialogService.DisplayAlertAsync("Validando Usuario", "Las credenciales indicadas no son válidas, por favor verifique.", "OK");
            }

            IsBusy = false;
        }
        private void OnClearCommandExecuted()
        {
            IsBusy = true;
            UserCode = string.Empty;
            UserPassword = string.Empty;
            IsBusy = false;
        }

    }
}
