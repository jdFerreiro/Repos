using System;
using System.Windows.Input;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace MyZadERP.ViewModels
{
    public class AboutViewModel : NotifyPropertyChangedBase
    {
        private string appName;
        private string packageName;
        private string version;
        private string build;

        public AboutViewModel()
        {
            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("http://zadesoft.com/")));
            AppName = AppInfo.Name;
            PackageName = AppInfo.PackageName;
            Version = AppInfo.VersionString;
            Build = AppInfo.BuildString;
        }

        public ICommand OpenWebCommand { get; }


        public string AppName
        {
            get => appName;
            set
            {
                if (appName == value)
                {
                    return;
                }
                appName = value;
                OnPropertyChanged();
            }
        }

        public string PackageName
        {
            get => packageName;
            set
            {
                if (packageName == value)
                {
                    return;
                }
                packageName = value;
                OnPropertyChanged();
            }
        }

        public string Version
        {
            get => version;
            set
            {
                if (version == value)
                {
                    return;
                }
                version = value;
                OnPropertyChanged();
            }
        }

        public string Build
        {
            get => build;
            set
            {
                if (build == value)
                {
                    return;
                }
                build = value;
                OnPropertyChanged();
            }
        }
    }
}