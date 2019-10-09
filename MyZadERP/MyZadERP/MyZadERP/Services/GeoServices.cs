using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyZadERP.Models.Facade;


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
            _ = await _geolocationManager.UpdateGeolocation(_location);
        }
        
    }
}
