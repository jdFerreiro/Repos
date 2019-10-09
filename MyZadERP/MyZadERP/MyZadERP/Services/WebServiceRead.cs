using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using static System.Net.HttpStatusCode;

namespace MyZadERP.Common
{
    public enum WebServiceErrorCode
    {
        TokenNotCreated = 1,
        TokenInvalid = 2,
        InvalidEmailPassword = 3,
        NotAuthorizedForThisPanel = 4,
        UserNotExist = 5,
        TokenNotRemoved = 6,
        LoginUserIsRequered = 7,
        InvalidCorporateSelection = 8,
        EmailAlreadyExist = 9,
        UserNoSaved = 10,
        UserNoUpdated = 11,
        Ok = 12,
        ErrorGeneral = 13
    }

    public enum Role
    {
        CorporateAdmin = 3,
        Admin = 4,
        SubCorporateAdmin = 6
    }
    
    public class WebServiceRead
    {
        private IRestResponse _response;
        private readonly RestClient _restClient;
        private readonly RestRequest _request;
        public string UrlBase { get; set; }
        public string RequestResource { get; set; }
        public WebServiceErrorCode ServiceErrorCode { get; set; }
        public string WebServiceMessage { get; set; }
        public Dictionary<string, string> RequestParameters;
        public List<Dictionary<string, string>> ListRequestParameters;

        public WebServiceRead(string baseUrl, string requestResource, object requestParameters, Method method)
        {
            UrlBase = baseUrl;
            RequestResource = requestResource;
            //RequestParameters = requestParameters;
            _restClient = new RestClient(baseUrl);
            _request = new RestRequest(requestResource, method);
            _request.AddJsonBody(requestParameters);
        }

        //public WebServiceRead(string baseUrl, string requestResource, List<Dictionary<string, string>> requestParameters, Method method)
        //{
        //    UrlBase = baseUrl;
        //    RequestResource = requestResource;
        //    //RequestParameters = requestParameters;
        //    _restClient = new RestClient(baseUrl);
        //    _request = new RestRequest(requestResource, method);
        //    _request.AddJsonBody(requestParameters);
        //}
        public async Task<string> Read()
        {
            try
            {
                _response = await _restClient.ExecuteTaskAsync(_request);
                return _response.StatusCode != OK ? string.Empty : _response.Content;
            }
            catch (Exception)
            {
                ServiceErrorCode = WebServiceErrorCode.ErrorGeneral;
                return String.Empty;
            }
        }

        public async Task<bool> Post()
        {
            try
            {
                _response = await _restClient.ExecuteTaskAsync(_request);
                return _response.StatusCode == Created;
            }
            catch (Exception)
            {
                ServiceErrorCode = WebServiceErrorCode.ErrorGeneral;
                return false;
            }
        }

        public async Task<bool> Put()
        {
            try
            {
                _response = await _restClient.ExecuteTaskAsync(_request);
                return _response.StatusCode == OK;
            }
            catch (Exception)
            {
                ServiceErrorCode = WebServiceErrorCode.ErrorGeneral;
                return false;
            }
        }

        public async Task<bool> Delete()
        {
            try
            {
                _response = await _restClient.ExecuteTaskAsync(_request);
                return _response.StatusCode == OK;
            }
            catch (Exception)
            {
                ServiceErrorCode = WebServiceErrorCode.ErrorGeneral;
                return false;
            }
        }
    }

    public class WebServiceReadFromUrl
    {
        private IRestResponse _response;
        private readonly RestClient _restClient;
        private readonly RestRequest _request;
        public string UrlBase { get; set; }
        public string RequestResource { get; set; }
        public WebServiceErrorCode ServiceErrorCode { get; set; }
        public string WebServiceMessage { get; set; }
        public Dictionary<string, string> RequestParameters;

        public WebServiceReadFromUrl(string baseUrl, string requestResource, Method method)
        {
            UrlBase = baseUrl;
            RequestResource = requestResource;
            _restClient = new RestClient(baseUrl);
            _request = new RestRequest(requestResource, method);
        }

        public string Read()
        {
            try
            {
                _response = _restClient.Execute(_request);
                return _response.StatusCode != OK ? string.Empty :_response.Content;
            }
            catch
            {
                ServiceErrorCode = WebServiceErrorCode.ErrorGeneral;
                return String.Empty;
            }
        }

    }
}
