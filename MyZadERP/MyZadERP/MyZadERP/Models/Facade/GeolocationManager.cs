using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using MyZadERP.Common;
using MyZadERP.ViewModels.DTO;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Essentials;

namespace MyZadERP.Models.Facade
{
    public class GeolocationManager
    {

        public GeolocationManager()
        {

        }

        public async Task<bool> UpdateGeolocation(Location location)
        {
            if (App.UserInfo == null) return false;
            var dispositivoModelo = DeviceInfo.Model;
            var dispositivoFabricante = DeviceInfo.Manufacturer;
            var dispositivoNombre = DeviceInfo.Name;
            var dispositivoSistema = DeviceInfo.VersionString;
            var dispositivoPlataforma = DeviceInfo.Platform;
            var dispositivoIdioma = DeviceInfo.Idiom;
            var dispositivoTipo = DeviceInfo.DeviceType;

            Dictionary<string, string> requestParameters = new Dictionary<string, string>() {
                { "idTecnico", App.UserInfo.TecnicoIdTecnico.ToString() },
                { "latitude", location.Latitude.ToString(CultureInfo.InvariantCulture)},
                { "logitude", location.Longitude.ToString(CultureInfo.InvariantCulture)},
                { "altitude", location.Altitude != null ? location.Altitude.ToString() : string.Empty},
                { "speed", location.Speed != null ? location.Speed.ToString() : string.Empty},
                { "accuracy", location.Accuracy != null ? location.Accuracy.ToString() :string.Empty },
                { "modelo",dispositivoModelo },
                { "fabricante",dispositivoFabricante },
                { "nombre",dispositivoNombre },
                { "sistema", dispositivoSistema},
                { "plataforma" , dispositivoPlataforma.ToString() },
                { "idioma", dispositivoIdioma.ToString() },
                { "tipo", dispositivoTipo.ToString()  }
            };

            var _ws = new WebServiceRead(App.MyZadBackEndURL, "api/GeoLocation?api-version=1.0", requestParameters, Method.POST);
            var requestResponse = await _ws.Read();
            return requestResponse != string.Empty && requestResponse == "true";
        }
    }
}
