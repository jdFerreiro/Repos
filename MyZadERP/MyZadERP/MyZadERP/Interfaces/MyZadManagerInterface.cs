using System;
using System.Collections.Generic;
using System.Text;

namespace MyZadERP.Interfaces
{
    public interface MyZadManagerInterface
    {
        void InsertElement(MyZadObjectInterface itemUpdate);

        int UpdateElement<T>(List<T> source, T oldValue, T newValue);

        void DeleteElement(int id);

        List<MyZadObjectInterface> GetAllElements();

        MyZadObjectInterface GetElement(int id);

        void UploadData(List<MyZadObjectInterface> wsElements);
    }
}
