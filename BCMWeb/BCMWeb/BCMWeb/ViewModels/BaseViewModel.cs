using BCMWeb.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCMWeb.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible, IConfirmNavigation
    { 
        public INavigationService NavigationService { get; private set; }
        public IPageDialogService PageDialogService { get; private set; }
        public IBatteryService BatteryService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public bool NotIsBusy
        {
            get { return !IsBusy; }
        }

        private bool allFieldsAreValid;
        public bool AllFieldsAreValid
        {
            get { return allFieldsAreValid; }
            set { SetProperty(ref allFieldsAreValid, value); }
        }

        public BaseViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IBatteryService batteryService)
        {
            NavigationService = navigationService;
            PageDialogService = pageDialogService;
            BatteryService = batteryService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
    }
}
