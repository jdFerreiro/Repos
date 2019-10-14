using Android.App.Job;
using Android.Util;
using Android.Widget;
using MyZadERP.Interfaces;
using MyZadERP.Services;
using MyZadERP.Views;
using System;
using System.Threading.Tasks;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace MyZadERP.Views.LoginScreen
{
    public class LoginPageViewModel : NotifyPropertyChangedBase
    {
        private string username;
        private string email;
        private string password;
        private bool savepassword;
        private bool isBusy;
        
        public LoginPageViewModel()
        {
            this.GoToViewCommand = new Command(this.LoadView);
            this.LoginCommand = new Command(async() => await ExecuteLogin(LoginType.Normal));
            this.ResetPasswordCommand = new Command(this.SubmitPasswordReset);
        }

        public string Username
        {
            get => this.username;
            set
            {
                if (this.username == value)
                {
                    return;
                }

                this.username = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => this.email;
            set
            {
                if (this.email == value)
                {
                    return;
                }

                this.email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => this.password;
            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                OnPropertyChanged();
            }
        }

        public bool SavePassword
        {
            get => this.savepassword;
            set
            {
                if (this.savepassword == value)
                {
                    return;
                }

                this.savepassword = value;
                OnPropertyChanged();
            }
        }


        public bool IsBusy
        {
            get => this.isBusy;
            set
            {
                if (this.isBusy == value)
                {
                    return;
                }

                this.isBusy = value;
                OnPropertyChanged();
            }
        }

        public Command GoToViewCommand { get; set; }

        public Command LoginCommand { get; set; }

        public Command ResetPasswordCommand { get; set; }

        public INavigationHandler NavigationHandler { private get; set; }

        private void LoadView(object viewType)
        {
            this.NavigationHandler.LoadView((ViewType)viewType);
        }

        private async Task ExecuteLogin(object obj)
        {
            switch ((LoginType)obj)
            {
                case LoginType.Normal:
                    IsBusy = true;
                    if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                    {
                        IsBusy = false;
                        await Application.Current.MainPage.DisplayAlert("Información","Se debe especificar su Usuario y Contraseña","Ok");
                        return;
                    }
                    var userManager = new Models.Facade.AccountManager();
                    App.UserInfo = await userManager.Autenticate(Username, Password);
                    IsBusy = false;
                    if (App.UserInfo == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Información", "Por favor, verifique su Usuario y Contraseña", "Ok");
                        return;
                    }

                    if (savepassword) {
                        _ = App.Database.SaveUserAsync(App.UserInfo);
                    }
                    Application.Current.MainPage = new MainPage();

                    int scheduleResult = App.jobScheduler.Schedule(App.jobInfo);

                    if (scheduleResult != JobScheduler.ResultSuccess)
                    {
                        string TAG = "GeoService";
                        DateTime startTime = DateTime.Now;

                        Log.Debug(TAG, $"Inicia Servicio GeoLocation ejecutado a las {startTime}");
                        
                    }
                    App.StartTime = DateTime.Now;
                    break;
            }
        }

        private void SubmitPasswordReset()
        {
            // Use the Email property
            Application.Current.MainPage.DisplayAlert("Password Reset", $"You have requested a password reset for: {Email}", "ok");
        }
    }
}