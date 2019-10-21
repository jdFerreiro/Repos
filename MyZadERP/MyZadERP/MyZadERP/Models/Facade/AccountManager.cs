using MyZadERP.Common;
using MyZadERP.Interfaces;
using MyZadERP.ViewModels.DTO;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyZadERP.Models.Facade
{
    public class AccountManager : MyZadManagerInterface
    {
        public AccountManager()
        {
        }

        public UserDTO GetUserInfo()
        {
            var dataUser = App.Database.GetUserAsync();
            UserDTO userInfo = (UserDTO)dataUser.Result;
            return userInfo;
        }

        public async Task<UserDTO> Autenticate(string UserName, string Password)
        {
            Dictionary<string, string> requestParameters = new Dictionary<string, string>() {
                { "userName", UserName },
                { "password", Password }
            };
            var _ws = new WebServiceRead(App.MyZadBackEndURL, "api/Token", requestParameters, Method.POST);
            var requestResponse = await _ws.Read();
            return requestResponse == string.Empty ? null : JsonConvert.DeserializeObject<UserDTO>(requestResponse);
        }

        public void SaveUserInfo(UserDTO userInfo) 
        {
            _ = App.Database.SaveUserAsync(userInfo);
        }

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


    }
}
