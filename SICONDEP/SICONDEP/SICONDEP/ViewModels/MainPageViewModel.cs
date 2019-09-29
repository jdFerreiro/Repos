using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SICONDEP.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Página principal";
            

        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void OnNavigateCommandExecuted(string path)
        {
            await NavigationService.NavigateAsync(path);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            UserName = UserLoggedViewModel.Name;
        }
    }
}
