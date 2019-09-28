using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SICONDEP.Helpers
{
    public class Settings : INotifyPropertyChanged
    {
        private static Lazy<Settings> SettingsInstance = new Lazy<Settings>(() => new Settings());
        public static Settings Current => SettingsInstance.Value;
        
        private Settings() { }
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        
        public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetProperty<T>(T value, T defaultValue = default(T), [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return false;

            T appSettingsValue = AppSettings.GetValueOrDefault<T>(propertyName, defaultValue);

            if (Equals(appSettingsValue, value)) return false;
        }
        #endregion
    }
}
