using Android.Util;
using Android.Widget;
using MyZadERP.Models.Facade;
using System;
using System.Threading.Tasks;


namespace MyZadERP.Services
{
    public class GeoServices 
    {
        private readonly Xamarin.Essentials.Location _location;
        private readonly GeolocationManager _geolocationManager;

        public GeoServices(Xamarin.Essentials.Location location)
        {
            _geolocationManager = new GeolocationManager();
            _location = location;
        }

        public async Task UpdateGeolocation()
        {
            try
            {
                _ = await _geolocationManager.UpdateGeolocation(_location);
            }
            catch (Exception fail)
            {
                string TAG = "X:" + typeof(GeoServices).Name;
                Log.Debug(TAG, $"Excepcion: {fail.Message}");
            }
        }

    }
}
