using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyZadERP.Models.Facade;
using MyZadERP.Util;
using MyZadERP.ViewModels.DTO;
using Telerik.XamarinForms.Common;
using System.Linq;
using System.Collections.Generic;

namespace MyZadERP.ViewModels
{
    public class ChargesListViewModel : NotifyPropertyChangedBase
    {
        private ImputacionesManager imputacionManager;
        private ObservableCollection<ImputacionDTO> items;
        private ObservableCollection<string> actionIcons;
        private int pageIndex;
        private int pageSize = 10; //TODO: Guardar en una variable a nivel del Servidor.
        private DateTime fechaInicio;
        private DateTime fechaFinal;
        private string description;
        private DateTime fechaInicioFiltro;
        private DateTime fechaFinalFiltro;
        private bool filtroAplicado;
        private bool nodata;
        private bool isbusy;
        private string totalhoras;
        private bool showFooter;
        private bool showMoreData;
  
        public ChargesListViewModel(DateTime FechaInicio, DateTime FechaFinal)
        {
            ActionIcons = new ObservableCollection<string>() { "Anteriores", "Actual", "Buscar" };
            fechaInicio = FechaInicio;
            fechaFinal = FechaFinal;
            fechaInicioFiltro = FechaInicio;
            fechaFinalFiltro = FechaFinal;
            pageIndex = 1;
            filtroAplicado = false;
            Items = new ObservableCollection<ImputacionDTO>();
            //FechData();
        }

        public ObservableCollection<ImputacionDTO> Items
        {
            get => items;
            set
            {
                if (items == value)
                {
                    return;
                }

                items = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> ActionIcons
        {
            get => actionIcons;
            set
            {
                if (actionIcons == value)
                {
                    return;
                }
                actionIcons = value;
                OnPropertyChanged();
            }
        }

        public string TotalHours
        {
            get => totalhoras;
            set
            {
                if (totalhoras == value)
                {
                    return;
                }
                totalhoras = value;
                OnPropertyChanged();
            }
        }

        public int PageIndex
        {
            get => pageIndex;
            set
            {
                if (pageIndex == value)
                {
                    return;
                }
                pageIndex = value;
                OnPropertyChanged();
            }
        }

        public int PageSize
        {
            get => pageSize;
            set
            {
                if (pageSize == value)
                {
                    return;
                }
                pageSize = value;
                OnPropertyChanged();
            }
        }

        public DateTime FechaFinal
        {
            get => fechaFinal;
            set
            {
                if (fechaFinal == value)
                {
                    return;
                }
                fechaFinal = value;
                OnPropertyChanged();
            }
        }

        public DateTime FechaInicio
        {
            get => fechaInicio;
            set
            {
                if (fechaInicio == value)
                {
                    return;
                }
                fechaInicio = value;
                OnPropertyChanged();
            }
        }

        public string Descripcion {
            get => description;
            set
            {
                if (description == value)
                {
                    return;
                }
                description = value;
                OnPropertyChanged();
            }
        }

        public DateTime FechaInicioFiltro
        {
            get => fechaInicioFiltro;
            set
            {
                if (fechaInicioFiltro == value)
                {
                    return;
                }
                fechaInicioFiltro = value;
                OnPropertyChanged();
            }
        }

        public DateTime FechaFinalFiltro
        {
            get => fechaFinalFiltro;
            set
            {
                if (fechaFinalFiltro == value)
                {
                    return;
                }
                fechaFinalFiltro = value;
                OnPropertyChanged();
            }
        }

        public bool FiltroAplicado
        {
            get => filtroAplicado;
            set
            {
                if (filtroAplicado == value)
                {
                    return;
                }
                filtroAplicado = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => isbusy;
            set
            {
                if (isbusy == value)
                {
                    return;
                }
                isbusy = value;
                OnPropertyChanged();
            }
        }

        public bool ShowFooter
        {
            get => showFooter;
            set
            {
                if (showFooter == value)
                {
                    return;
                }
                showFooter = value;
                OnPropertyChanged();
            }
        }

        public bool ShowMoreData
        {
            get => showMoreData;
            set
            {
                if (showMoreData == value)
                {
                    return;
                }
                showMoreData = value;
                OnPropertyChanged();
            }
        }

        public async Task FechData()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                imputacionManager = new ImputacionesManager();
                ObservableCollection<ImputacionDTO> observable = await imputacionManager.GetImputacionesAsync(FechaInicio, FechaFinal,PageSize, PageIndex, Descripcion);
                if (Items == null)
                {
                    Items = observable;
                    IsBusy = false;
                    return;
                }
                else
                {
                    if (observable != null) foreach (ImputacionDTO item in observable) Items.Add(item);
                }
                if (observable != null)
                {
                    PageIndex++;
                }
                if (observable.Count == 0 || observable == null)
                {
                    ShowMoreData = false;
                    ShowFooter = false;
                    TotalHours = "0.0 Hrs.";
                    NoData = true;
                }
                else
                {
                    ShowMoreData = (observable.Any() && observable.Count < PageSize) ? false : true;
                    TotalHours = Items.Sum(e => e.Horas).ToString() + " Hrs.";
                    ShowFooter = true;
                    NoData = false;
                }
                IsBusy = false;
            }
            catch 
            {
                IsBusy = false;
            }
        }

        public async Task<bool> DeleteImputacion(ObservableCollection<ImputacionDTO> imputaciones)
        {
            try
            {
                imputacionManager = new ImputacionesManager();
                await imputacionManager.DeleteImputacion(imputaciones);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Clear()
        {
            PageIndex = 1;
            Items = new ObservableCollection<ImputacionDTO>();
            //Items.Clear();
        }

        public bool NoData
        {
            get => nodata;
            set
            {
                if (nodata == value)
                {
                    return;
                }
                nodata = value;
                OnPropertyChanged();
            }
        }
    }
}
