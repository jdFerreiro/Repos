using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MyZadERP.ViewModels;
using MyZadERP.ViewModels.DTO;
using MyZadERP.Views;
using Telerik.XamarinForms.Common;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace MyZadERP.Views.ChargesListView
{
    public enum TabSelection
    {
        ANTERIOR = 0,
        ACTUAL = 1,
        BUSCAR =2
    }

    public partial class ChargesListView : ContentPage
    {
        private readonly ChargesListViewModel _chargesListViewModel;
        private DateTime FechaInicio;
        private DateTime FechaFinal;

        public ChargesListView()
        {
            InitializeComponent();
            FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            FechaFinal = new DateTime(FechaInicio.Year, FechaInicio.Month, DateTime.DaysInMonth(FechaInicio.Year, FechaInicio.Month));
            _chargesListViewModel = new ChargesListViewModel(FechaInicio, FechaFinal);
            this.BindingContext = _chargesListViewModel;
        }

        private async void ItemNew_OnClicked(object sender, EventArgs e)
        {
            var page = new EditChargues.EditChargues();
            await Navigation.PushModalAsync(page, true);
        }

        private void CloseSearchParametersPopup(object sender, EventArgs e)
        {
            try
            {
                popCustomSearch.IsOpen = false;
                scPeriod.SelectedIndex = TabSelection.ACTUAL.GetHashCode();
            }
            catch
            {
                throw;
            }
        }

        private async void ScPeriod_OnSelectionChanged(object sender, ValueChangedEventArgs<int> e)
        {
            popCustomSearch.IsOpen = scPeriod.SelectedIndex == TabSelection.BUSCAR.GetHashCode();
            if (scPeriod.SelectedIndex == TabSelection.ACTUAL.GetHashCode())
            {
                FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); 
                FechaFinal = new DateTime(FechaInicio.Year, FechaInicio.Month, DateTime.DaysInMonth(FechaInicio.Year, FechaInicio.Month));
                _chargesListViewModel.FechaInicio = FechaInicio;
                _chargesListViewModel.FechaFinal = FechaFinal;
                _chargesListViewModel.FiltroAplicado = false;
                _chargesListViewModel.Clear();
                await _chargesListViewModel.FechData();
            } else if (scPeriod.SelectedIndex == TabSelection.ANTERIOR.GetHashCode())
            {
                if (!_chargesListViewModel.FiltroAplicado)
                {
                    FechaInicio = new DateTime(FechaInicio.Month == 1 ? FechaInicio.Year - 1 : FechaInicio.Year, FechaInicio.Month == 1 ? 12 : FechaInicio.Month - 1, 1);
                    FechaFinal = new DateTime(FechaInicio.Year, FechaInicio.Month, DateTime.DaysInMonth(FechaInicio.Year, FechaInicio.Month));
                    _chargesListViewModel.FechaInicio = FechaInicio;
                    _chargesListViewModel.FechaFinal = FechaFinal;
                } else
                {
                    _chargesListViewModel.FechaInicio = _chargesListViewModel.FechaInicioFiltro;
                    _chargesListViewModel.FechaFinal = _chargesListViewModel.FechaFinalFiltro;
                }
                _chargesListViewModel.Clear();
                await _chargesListViewModel.FechData();
            }
           
        }

        private async void ListView_LoadOnDemand(object sender, EventArgs e)
        {
            await _chargesListViewModel.FechData();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            _chargesListViewModel.Clear();
            await _chargesListViewModel.FechData();
            if (_chargesListViewModel.FechaInicio.Year == DateTime.Now.Year && _chargesListViewModel.FechaInicio.Month == DateTime.Now.Year)
            {
                scPeriod.SelectedIndex = TabSelection.ACTUAL.GetHashCode();
            } else if (_chargesListViewModel.FiltroAplicado)
            {
                scPeriod.SelectedIndex = TabSelection.ANTERIOR.GetHashCode();
            }
        }

        private void ConfigurateToolBar()
        {
            if (ToolbarItems.Count == 1 && ListView.SelectedItems.Count > 0)
            {
                ToolbarItems.Insert(0, new ToolbarItem()
                {
                    IconImageSource = "ic_delete_white_18dp",
                    Command = new Command( async () => {
                                    if (ListView.SelectedItems.Count == 0)
                                    {
                                        await DisplayAlert("Información","No existen elementos a Eliminar","Ok");
                                        return;
                                    }
                                    bool bRetbol = await DisplayAlert("Eliminacion", "Seguro de Eliminar las Imputaciones seleccionada(s)?", "Si", "No");
                                    if (!bRetbol) return;
                        
                                    ObservableCollection<ImputacionDTO> _aux = new ObservableCollection<ImputacionDTO>();
                                    foreach (var item in ListView.SelectedItems)
                                    {
                                        _aux.Add((ImputacionDTO)item);
                                    }
                                    await _chargesListViewModel.DeleteImputacion(_aux);
                                    //NOTE: Eliminar de la coleccion y no realizar llamada de API nuevamente.
                                    _chargesListViewModel.Clear();
                                    await _chargesListViewModel.FechData();
                    })
                });

                
            }

            if (ListView.SelectedItems.Count == 1)
            {
                bool bRetExisteEdit = ToolbarItems.Any(item => item.IconImageSource.ToString() == "ic_mode_edit_white_18dp");
                if (!bRetExisteEdit)
                {
                    ToolbarItems.Insert(0, new ToolbarItem()
                    {
                        IconImageSource = "ic_mode_edit_white_18dp",
                        Command = new Command(async () =>
                        {
                            ImputacionDTO _aux = new ImputacionDTO();
                            _aux = (ImputacionDTO)ListView.SelectedItems[0];
                            var page = new EditChargues.EditChargues(_aux);
                            await Navigation.PushModalAsync(page, true);
                        })
                    });
                }
            }


            if (ListView.SelectedItems.Count == 0)
            {
                foreach (ToolbarItem item in ToolbarItems)
                {
                    if (item.IconImageSource.ToString() == "ic_delete_white_18dp" || item.IconImageSource.ToString() == "ic_mode_edit_white_18dp")
                    {
                        ToolbarItems.Remove(item);
                        break;
                    }
                }
            }

            ////NOTE: Eliminar el boton EDITAR si existen mas de un Item Seleccionado.
            if (ListView.SelectedItems.Count > 1)
            {
                foreach (ToolbarItem item in ToolbarItems)
                {
                    if (item.IconImageSource.ToString() == "ic_mode_edit_white_18dp")
                    {
                        ToolbarItems.Remove(item);
                        break;
                    }
                }
            }

        }

        private async void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (_chargesListViewModel.FechaInicioFiltro > _chargesListViewModel.FechaFinalFiltro)
                {
                    await DisplayAlert("Información", "Rango de Fechas no Válido", "Ok");
                    return;
                }

                if (_chargesListViewModel.FechaInicio != _chargesListViewModel.FechaInicioFiltro)
                {
                    _chargesListViewModel.FechaInicio = _chargesListViewModel.FechaInicioFiltro;
                    _chargesListViewModel.FiltroAplicado = true;
                }

                if (_chargesListViewModel.FechaFinal != _chargesListViewModel.FechaFinalFiltro)
                {
                    _chargesListViewModel.FechaFinal = _chargesListViewModel.FechaFinalFiltro;
                    _chargesListViewModel.FiltroAplicado = true;
                }

                if (_chargesListViewModel.Descripcion != string.Empty)
                {
                    _chargesListViewModel.FiltroAplicado = true;
                }
                popCustomSearch.IsOpen = false;
                scPeriod.SelectedIndex = TabSelection.ANTERIOR.GetHashCode();
            }
            catch
            {
                throw;
            }
        }

        private void ListView_SelectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ConfigurateToolBar();
        }
    }
}
