using MyZadERP.Common;
using MyZadERP.Interfaces;
using MyZadERP.ViewModels.DTO;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyZadERP.Models.Facade
{
    public class ImputacionesManager : MyZadManagerInterface
    {

        public async Task<ObservableCollection<ImputacionDTO>> GetImputacionesAsync(DateTime FechaInicio, DateTime FechaFinal, int PageSize, int PageIndex, string Descripcion)
        {
            try
            {
                Dictionary<string, string> requestParameters = new Dictionary<string, string>
                {
                    { "idTecnico", App.UserInfo.TecnicoIdTecnico.ToString() },
                    { "fechaInicio", FechaInicio.ToString("yyyy-MM-dd") },
                    { "fechaFinal", FechaFinal.ToString("yyyy-MM-dd") },
                    { "pageSize", PageSize.ToString() },
                    { "pageIndex", PageIndex.ToString() },
                    { "descripcion", string.IsNullOrEmpty(Descripcion) ? string.Empty : Descripcion }
                };
                //var _ws = new WebServiceRead(App.MyZadBackEndURL, "api/Imputaciones", requestParameters, Method.POST);
                string urlParameters = $"api/Imputaciones?idTecnico={App.UserInfo.TecnicoIdTecnico.ToString()}&fechaInicio={FechaInicio.ToString("yyyy-MM-dd")}&fechaFinal={FechaFinal.ToString("yyyy-MM-dd")}&pageSize={PageSize.ToString()}&pageIndex={PageIndex.ToString()}&descripcion={(string.IsNullOrEmpty(Descripcion) ? string.Empty : Descripcion)}&api-version=1.0";
                var _ws = new WebServiceRead(App.MyZadBackEndURL, urlParameters, null, Method.GET);
                var requestResponse = await _ws.Read();
                return requestResponse == string.Empty ? null : JsonConvert.DeserializeObject<ObservableCollection<ImputacionDTO>>(requestResponse);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ObservableCollection<ItemDTO>> GetHerramientas()
        {
            try
            {
                var _ws = new WebServiceRead(App.MyZadBackEndURL, "api/Imputaciones/Herramientas", null, Method.GET);
                var requestResponse = await _ws.Read();
                return requestResponse == string.Empty ? null : JsonConvert.DeserializeObject<ObservableCollection<ItemDTO>>(requestResponse);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ObservableCollection<ItemDTO>> GetPedidos()
        {
            try
            {
                var _ws = new WebServiceRead(App.MyZadBackEndURL, "api/Imputaciones/Pedidos", null, Method.GET);
                var requestResponse = await _ws.Read();
                return requestResponse == string.Empty ? null : JsonConvert.DeserializeObject<ObservableCollection<ItemDTO>>(requestResponse);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ObservableCollection<ItemDTO>> GetCapitulos(int idPedido)
        {
            try
            {
                var _ws = new WebServiceRead(App.MyZadBackEndURL, $"api/Imputaciones/Capitulos/?idPedido={idPedido}", null, Method.GET);
                var requestResponse = await _ws.Read();
                return requestResponse == string.Empty ? null : JsonConvert.DeserializeObject<ObservableCollection<ItemDTO>>(requestResponse);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ObservableCollection<ItemDTO>> GetPartidas(int idPedido, int idCapitulo)
        {
            try
            {
                var _ws = new WebServiceRead(App.MyZadBackEndURL, $"api/Imputaciones/Partidas/?idPedido={idPedido}&idOfertaCapitulo={idCapitulo}", null, Method.GET);
                var requestResponse = await _ws.Read();
                return requestResponse == string.Empty ? null : JsonConvert.DeserializeObject<ObservableCollection<ItemDTO>>(requestResponse);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ObservableCollection<ItemDTO>> GetFases(int idPedidoPartida)
        {
            try
            {
                var _ws = new WebServiceRead(App.MyZadBackEndURL, $"api/Imputaciones/Fases/?idPedidoPartida={idPedidoPartida}", null, Method.GET);
                var requestResponse = await _ws.Read();
                return requestResponse == string.Empty ? null : JsonConvert.DeserializeObject<ObservableCollection<ItemDTO>>(requestResponse);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ObservableCollection<ItemDTO>> GetMediciones(int idPedidoPartida)
        {
            try
            {
                var _ws = new WebServiceRead(App.MyZadBackEndURL, $"api/Imputaciones/Mediciones/?idPedidoPartida={idPedidoPartida}", null, Method.GET);
                var requestResponse = await _ws.Read();
                return requestResponse == string.Empty ? null : JsonConvert.DeserializeObject<ObservableCollection<ItemDTO>>(requestResponse);
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> CreateImputacion(ImputacionDTO imputacion)
        {
            try
            {
                Dictionary<string, string> requestParameters = new Dictionary<string, string>
                {
                    { "idTecnico", App.UserInfo.TecnicoIdTecnico.ToString(CultureInfo.InvariantCulture ) },
                    { "idPedidoDetalle", imputacion.idPedidoDetalle.ToString(CultureInfo.InvariantCulture ) },
                    { "idPedido", imputacion.idPedido.ToString(CultureInfo.InvariantCulture ) },
                    { "idMedicion", imputacion.idMedicion.ToString(CultureInfo.InvariantCulture ) },
                    { "idFase", imputacion.idFase.ToString() },
                    { "fecha", imputacion.Fecha.ToString("yyyy-MM-dd") },
                    { "horas", imputacion.Horas.ToString(CultureInfo.InvariantCulture ) },
                    { "mediciondia", imputacion.MedicionDia.ToString(CultureInfo.InvariantCulture ) },
                    { "comentario", imputacion.Comentario},
                    { "kilometros", imputacion.Kilometros.ToString(CultureInfo.InvariantCulture ) },
                    { "euros", imputacion.Euros.ToString(CultureInfo.InvariantCulture ) },
                    { "herramienta1", imputacion.Herramienta1.ToString(CultureInfo.InvariantCulture ) },
                    { "herramienta2", imputacion.Herramienta2.ToString(CultureInfo.InvariantCulture ) },
                    { "herramienta3", imputacion.Herramienta3.ToString(CultureInfo.InvariantCulture ) },
                    { "dieta", imputacion.Dieta ? "true": "false" }
                };
                var _ws = new WebServiceRead(App.MyZadBackEndURL, "api/Imputaciones?api-version=1.0", requestParameters, Method.POST);
                var requestResponse = await _ws.Post();
                return requestResponse;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteImputacion(ObservableCollection<ImputacionDTO> imputaciones)
        {
            try
            {
                List<Dictionary<string, string>> requestParameters = new List<Dictionary<string, string>>();
                foreach (ImputacionDTO imputacion in imputaciones)
                {
                    Dictionary<string, string> requestParameter = new Dictionary<string, string>
                    {
                        { "idImputacion", imputacion.idImputacion.ToString(CultureInfo.InvariantCulture) },
                        { "idTecnico", App.UserInfo.TecnicoIdTecnico.ToString(CultureInfo.InvariantCulture) },
                        { "idPedidoDetalle", imputacion.idPedidoDetalle.ToString(CultureInfo.InvariantCulture) },
                        { "idPedido", imputacion.idPedido.ToString(CultureInfo.InvariantCulture) },
                        { "idMedicion", imputacion.idMedicion.ToString(CultureInfo.InvariantCulture) },
                        { "idFase", imputacion.idFase.ToString(CultureInfo.InvariantCulture) },
                        { "fecha", imputacion.Fecha.ToString("yyyy-MM-dd") },
                        { "horas", imputacion.Horas.ToString(CultureInfo.InvariantCulture) },
                        { "mediciondia", imputacion.MedicionDia.ToString(CultureInfo.InvariantCulture) },
                        { "comentario", imputacion.Comentario},
                        { "kilometros", imputacion.Kilometros.ToString(CultureInfo.InvariantCulture) },
                        { "euros", imputacion.Euros.ToString(CultureInfo.InvariantCulture) },
                        { "herramienta1", imputacion.Herramienta1.ToString(CultureInfo.InvariantCulture) },
                        { "herramienta2", imputacion.Herramienta2.ToString(CultureInfo.InvariantCulture) },
                        { "herramienta3", imputacion.Herramienta3.ToString(CultureInfo.InvariantCulture) },
                        { "dieta", imputacion.Dieta ? "true": "false" }
                    };
                    requestParameters.Add(requestParameter);
                }
                var _ws = new WebServiceRead(App.MyZadBackEndURL, "api/Imputaciones?api-version=1.0", requestParameters, Method.DELETE);
                var requestResponse = await _ws.Delete();
                return requestResponse;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateImputacion(ImputacionDTO imputacionNew, ImputacionDTO imputacionOld)
        {
            try
            {
                List<Dictionary<string, string>> requestParameters = new List<Dictionary<string, string>>();
                Dictionary<string, string> requestParametersImputacionNew = new Dictionary<string, string>
                {
                    { "idImputacion", imputacionNew.idImputacion.ToString (CultureInfo.InvariantCulture) },
                    { "idTecnico", App.UserInfo.TecnicoIdTecnico.ToString(CultureInfo.InvariantCulture ) },
                    { "idPedidoDetalle", imputacionNew.idPedidoDetalle.ToString(CultureInfo.InvariantCulture ) },
                    { "idPedido", imputacionNew.idPedido.ToString(CultureInfo.InvariantCulture ) },
                    { "idMedicion", imputacionNew.idMedicion.ToString(CultureInfo.InvariantCulture ) },
                    { "idFase", imputacionNew.idFase.ToString(CultureInfo.InvariantCulture ) },
                    { "fecha", imputacionNew.Fecha.ToString("yyyy-MM-dd") },
                    { "horas", imputacionNew.Horas.ToString(CultureInfo.InvariantCulture ) },
                    { "mediciondia", imputacionNew.MedicionDia.ToString(CultureInfo.InvariantCulture ) },
                    { "descripcion", imputacionNew.Descripcion},
                    { "comentario", imputacionNew.Comentario},
                    { "kilometros", imputacionNew.Kilometros.ToString(CultureInfo.InvariantCulture ) },
                    { "euros", imputacionNew.Euros.ToString(CultureInfo.InvariantCulture ) },
                    { "herramienta1", imputacionNew.Herramienta1.ToString(CultureInfo.InvariantCulture ) },
                    { "herramienta2", imputacionNew.Herramienta2.ToString(CultureInfo.InvariantCulture ) },
                    { "herramienta3", imputacionNew.Herramienta3.ToString(CultureInfo.InvariantCulture ) },
                    { "dieta", imputacionNew.Dieta ? "true": "false" }
                };
                requestParameters.Add(requestParametersImputacionNew);

                Dictionary<string, string> requestParametersImputacionOld = new Dictionary<string, string>
                {
                    { "idImputacion", imputacionOld.idImputacion.ToString (CultureInfo.InvariantCulture) },
                    { "idTecnico", App.UserInfo.TecnicoIdTecnico.ToString(CultureInfo.InvariantCulture ) },
                    { "idPedidoDetalle", imputacionOld.idPedidoDetalle.ToString(CultureInfo.InvariantCulture ) },
                    { "idPedido", imputacionOld.idPedido.ToString(CultureInfo.InvariantCulture ) },
                    { "idMedicion", imputacionOld.idMedicion.ToString(CultureInfo.InvariantCulture ) },
                    { "idFase", imputacionOld.idFase.ToString(CultureInfo.InvariantCulture ) },
                    { "fecha", imputacionOld.Fecha.ToString("yyyy-MM-dd") },
                    { "horas", imputacionOld.Horas.ToString(CultureInfo.InvariantCulture ) },
                    { "mediciondia", imputacionOld.MedicionDia.ToString(CultureInfo.InvariantCulture ) },
                    { "descripcion", imputacionOld.Descripcion},
                    { "comentario", imputacionOld.Comentario},
                    { "kilometros", imputacionOld.Kilometros.ToString(CultureInfo.InvariantCulture ) },
                    { "euros", imputacionOld.Euros.ToString(CultureInfo.InvariantCulture ) },
                    { "herramienta1", imputacionOld.Herramienta1.ToString(CultureInfo.InvariantCulture ) },
                    { "herramienta2", imputacionOld.Herramienta2.ToString(CultureInfo.InvariantCulture ) },
                    { "herramienta3", imputacionOld.Herramienta3.ToString(CultureInfo.InvariantCulture ) },
                    { "dieta", imputacionOld.Dieta ? "true": "false" }
                };
                requestParameters.Add(requestParametersImputacionOld);
                var _ws = new WebServiceRead(App.MyZadBackEndURL, "api/Imputaciones?api-version=1.0", requestParameters, Method.PUT);
                var requestResponse = await _ws.Put();
                return requestResponse;
            }
            catch
            {
                return false;
            }
        }

        #region Pendiente de Implementar
        public void DeleteElement(int id)
        {
            throw new NotImplementedException();
        }

        public List<MyZadObjectInterface> GetAllElements()
        {
            throw new NotImplementedException();
        }

        public MyZadObjectInterface GetElement(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertElement(MyZadObjectInterface itemUpdate)
        {
            throw new NotImplementedException();
        }

        public int UpdateElement<T>(List<T> source, T oldValue, T newValue)
        {
            throw new NotImplementedException();
        }

        public void UploadData(List<MyZadObjectInterface> wsElements)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
