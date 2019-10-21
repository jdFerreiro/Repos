using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using System.Linq;
using MyZadERP.ViewModels.DTO;
using System;

namespace MyZadERP.Views.EditChargues
{
    public partial class EditChargues : ContentPage
    {
        private readonly EditCharguesViewModel _editCharguesViewModel;
        private readonly ImputacionDTO _imputacion;
        private ItemDTO _selectedPedido;
        private ItemDTO _selectedCapitulo;
        private ItemDTO _selectedPartida;
        private ItemDTO _selectedFase;

        public EditChargues()
        {
            InitializeComponent();
            _imputacion = null;
            _editCharguesViewModel = new EditCharguesViewModel();
            this.BindingContext = _editCharguesViewModel;
        }

        public EditChargues(ImputacionDTO imputacion)
        {
            InitializeComponent();
            _editCharguesViewModel = new EditCharguesViewModel(imputacion);
            this.BindingContext = _editCharguesViewModel;
            _imputacion = imputacion;
            btnGuardar.Text = "GUARDAR IMPUTACION";
        }
        #region Pedido
        private async void Pedido_SuggestionItemSelected(object sender, Telerik.XamarinForms.Input.AutoComplete.SuggestionItemSelectedEventArgs e)
        {
            _selectedPedido = (ItemDTO)e.DataItem; 
            await _editCharguesViewModel.LoadCapitulos(_selectedPedido.Id).ConfigureAwait(false);
        }

        private void Pedido_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_editCharguesViewModel.PedidoSelected == " ") return;
            if (_editCharguesViewModel.PedidoSelected.Trim().Length == 0)
            {
                _selectedPedido = null;
                _editCharguesViewModel.PedidoSelected = string.Empty;
                _editCharguesViewModel.CapituloSelected = string.Empty;
                _editCharguesViewModel.PartidaSelected = string.Empty;
                _editCharguesViewModel.FaseSelected = string.Empty;

                _editCharguesViewModel.Capitulo = new System.Collections.ObjectModel.ObservableCollection<ItemDTO>();
                _editCharguesViewModel.Partida = new System.Collections.ObjectModel.ObservableCollection<ItemDTO>();
                _editCharguesViewModel.Fase = new System.Collections.ObjectModel.ObservableCollection<ItemDTO>();
                _editCharguesViewModel.PedidoSelected = " ";
            }

        }

        private void Pedido_Focused(object sender, FocusEventArgs e)
        {
            if (_editCharguesViewModel.PedidoSelected.Length == 0) _editCharguesViewModel.PedidoSelected = " ";
        }
        #endregion

        #region Capitulo
        private async void Capitulo_SuggestionItemSelected(object sender, Telerik.XamarinForms.Input.AutoComplete.SuggestionItemSelectedEventArgs e)
        {
            try
            {
                if (_selectedPedido == null)
                {
                    _selectedPedido = string.IsNullOrEmpty(_editCharguesViewModel.PedidoSelected.Trim()) ? null : _editCharguesViewModel.Pedidos.First(p => p.Descripcion.Trim() == _editCharguesViewModel.PedidoSelected.Trim()) ?? null;
                }
                _selectedCapitulo = (ItemDTO)e.DataItem;
                await _editCharguesViewModel.LoadPartidas(_selectedPedido.Id, _selectedCapitulo.Id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void Capitulo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (_editCharguesViewModel.CapituloSelected.StartsWith(" ")) return;
                if (_editCharguesViewModel.CapituloSelected.Trim().Length == 0)
                {
                    _selectedCapitulo = null;
                    _editCharguesViewModel.PartidaSelected = string.Empty;
                    _editCharguesViewModel.FaseSelected = string.Empty;

                    _editCharguesViewModel.Partida = new System.Collections.ObjectModel.ObservableCollection<ItemDTO>();
                    _editCharguesViewModel.Fase = new System.Collections.ObjectModel.ObservableCollection<ItemDTO>();
                    _editCharguesViewModel.CapituloSelected = " ";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Capitulo_Focused(object sender, FocusEventArgs e)
        {
            if (_editCharguesViewModel.CapituloSelected.Trim().Length == 0) _editCharguesViewModel.CapituloSelected = " ";
        }
        #endregion

        #region Partida
        private async void Partida_SuggestionItemSelected(object sender, Telerik.XamarinForms.Input.AutoComplete.SuggestionItemSelectedEventArgs e)
        {
            _selectedPartida = (ItemDTO)e.DataItem;
            await _editCharguesViewModel.LoadFases(_selectedPartida.Id).ConfigureAwait(false); 
        }

        private void Partida_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_editCharguesViewModel.PartidaSelected.StartsWith(" ")) return;
            if (_editCharguesViewModel.PartidaSelected.Trim().Length == 0)
            {
                _selectedPartida = null;
                _editCharguesViewModel.PartidaSelected = string.Empty;
                _editCharguesViewModel.FaseSelected = string.Empty;

                _editCharguesViewModel.Fase = new System.Collections.ObjectModel.ObservableCollection<ItemDTO>();
                _editCharguesViewModel.PartidaSelected = " ";
            }
        }

        private void Partida_Focused(object sender, FocusEventArgs e)
        {
            if (_editCharguesViewModel.PartidaSelected.Trim().Length == 0) _editCharguesViewModel.PartidaSelected = " ";
        }
        #endregion

        #region Fase
        private void Fase_SuggestionItemSelected(object sender, Telerik.XamarinForms.Input.AutoComplete.SuggestionItemSelectedEventArgs e)
        {
            _selectedFase = (ItemDTO)e.DataItem;
        }
        private void Fase_Focused(object sender, FocusEventArgs e)
        {
            if (_editCharguesViewModel.FaseSelected.Trim().Length == 0) _editCharguesViewModel.FaseSelected = " ";
        }

        private void Fase_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_editCharguesViewModel.FaseSelected.StartsWith(" ")) return;
            if (_editCharguesViewModel.FaseSelected.Trim().Length == 0)
            {
                _selectedFase = null;
                _editCharguesViewModel.FaseSelected = string.Empty;
                _editCharguesViewModel.FaseSelected = " ";
            }
        }
        #endregion

        private async void BtnGuardar_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                bool bRetBol = false;
                if (_imputacion == null)
                {
                    bRetBol = await _editCharguesViewModel.CreateImputacion().ConfigureAwait(false); ;
                } else
                {
                    bRetBol = await _editCharguesViewModel.UpdateImputacion(_imputacion).ConfigureAwait(false); 
                }
                if (bRetBol) await App.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception fail)
            {
                await DisplayAlert("Error", fail.InnerException.Message, "Ok").ConfigureAwait(false);
            }
        }

        private void HorasDia_ValueChanged(object sender, Telerik.XamarinForms.Input.NumericInput.ValueChangedEventArgs e)
        {
            _editCharguesViewModel.HorasDia = e.NewValue ?? 0;
        }

        private void MedicionDia_ValueChanged(object sender, Telerik.XamarinForms.Input.NumericInput.ValueChangedEventArgs e)
        {
            _editCharguesViewModel.MedicionDia = e.NewValue ?? 0;
        }

        private void BtnCancelar_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Herramienta1_Focused(object sender, FocusEventArgs e)
        {
            if (Herramienta1.Text.Trim().Length == 0) Herramienta1.Text = " ";
        }

        private void Herramienta2_Focused(object sender, FocusEventArgs e)
        {
            if (Herramienta2.Text.Trim().Length == 0) Herramienta2.Text = " ";
        }

        private void Herramienta3_Focused(object sender, FocusEventArgs e)
        {
            if (Herramienta3.Text.Trim().Length == 0) Herramienta3.Text = " ";
        }
                     
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }


    }
}
