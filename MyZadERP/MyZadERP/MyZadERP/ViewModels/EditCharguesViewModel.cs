using FluentValidation;
using MyZadERP.Models.Facade;
using MyZadERP.ViewModels.DTO;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using System.Linq;

namespace MyZadERP.Views.EditChargues
{
    public class EditCharguesViewModel : NotifyPropertyChangedBase
    {
        private ImputacionesManager imputaciones;
        ObservableCollection<ItemDTO> pedidos;
        ObservableCollection<ItemDTO> ampliacion;
        ObservableCollection<ItemDTO> capitulo;
        ObservableCollection<ItemDTO> partida;
        ObservableCollection<ItemDTO> fase;
        ObservableCollection<ItemDTO> mediciones;
        string comentario;
        DateTime fecha;
        double horasdia;
        double mediciondia;
        int mediciontotalrealejecutada;
        int horasobjetivo;
        int horasacumuladas;
        int horasrestantes;
        int medicionobjetivo;
        int medicionacumuladas;
        int medicionrestantes;
        ObservableCollection<ItemDTO> herramienta1;
        ObservableCollection<ItemDTO> herramienta2;
        ObservableCollection<ItemDTO> herramienta3;
        int kilometros;
        int euros;
        string descripcion;
        bool dieta;
        bool incidencia;
        string materiales;
        string importe;
        ObservableCollection<ItemDTO> proveedor;
        ObservableCollection<ItemDTO> tipoincidencia;
        readonly IValidator _validator;
        string pedidoSelected;
        string partidaSelected;
        string capituloSelected;
        string faseSelected;
        string herramienta1Selected;
        string herramienta2Selected;
        string herramienta3Selected;

        public EditCharguesViewModel()
        {
            Fecha = DateTime.Now;
            _validator = new ImputacionValidator();

            LoadPedidos().ConfigureAwait(true);
            LoadHerramientas().ConfigureAwait(true);

            PedidoSelected = " ";
            CapituloSelected = " ";
            PartidaSelected = " ";
            FaseSelected = " ";
            Comentario = "";
            Herramienta1Selected = " ";
            Herramienta2Selected = " ";
            Herramienta3Selected = " ";
        }

        public EditCharguesViewModel(ImputacionDTO imputacion)
        {
            _validator = new ImputacionValidator();
            LoadPedidos().ConfigureAwait(true);
            LoadHerramientas().ConfigureAwait(true);
            PedidoSelected = imputacion.DescripcionPedido;
            CapituloSelected = imputacion.DescripcionCapitulo;

            LoadCapitulos(imputacion.idPedido).ConfigureAwait(true);

            LoadPartidas(imputacion.idPedido, imputacion.idCapitulo).ConfigureAwait(true);
            PartidaSelected = imputacion.DescripcionPartida;

            LoadFases(imputacion.idPedidoDetalle).ConfigureAwait(true);
            FaseSelected = imputacion.DescripcionFase;

            Herramienta1Selected = imputacion.DescripcionHerramienta1;
            Herramienta2Selected = imputacion.DescripcionHerramienta2;
            Herramienta3Selected = imputacion.DescripcionHerramienta3;
            Fecha = imputacion.Fecha;
            Comentario = imputacion.Comentario; 
            Kilometros =  (int)imputacion.Kilometros;
            Euros = (int)imputacion.Euros;
            Dieta = imputacion.Dieta;
            MedicionDia = imputacion.MedicionDia;
            HorasDia = imputacion.Horas;
        }



        public ObservableCollection<ItemDTO> Pedidos
        {
            get => pedidos;
            set
            {
                if (pedidos == value)
                {
                    return;
                }
                pedidos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemDTO> Ampliacion
        {
            get => ampliacion;
            set
            {
                if (ampliacion == value)
                {
                    return;
                }
                ampliacion = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemDTO> Capitulo
        {
            get => capitulo;
            set
            {
                if (capitulo == value)
                {
                    return;
                }
                capitulo = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemDTO> Partida
        {
            get => partida;
            set
            {
                if (partida == value)
                {
                    return;
                }
                partida = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemDTO> Fase
        {
            get => fase;
            set
            {
                if (fase == value)
                {
                    return;
                }
                fase = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemDTO> Mediciones
        {
            get => mediciones;
            set
            {
                if (mediciones == value)
                {
                    return;
                }
                mediciones = value;
                OnPropertyChanged();
            }
        }

        public string Comentario
        {
            get => comentario;
            set
            {
                if (comentario == value)
                {
                    return;
                }
                comentario = value;
                OnPropertyChanged();
            }
        }

        public DateTime Fecha
        {
            get => fecha;
            set
            {
                if (fecha == value)
                {
                    return;
                }
                fecha = value;
                OnPropertyChanged();
            }
        }

        public double HorasDia
        {
            get => horasdia;
            set
            {
                if (horasdia == value)
                {
                    return;
                }
                horasdia = value;
                OnPropertyChanged();
            }
        }

        public double MedicionDia
        {
            get => mediciondia;
            set
            {
                if (mediciondia == value)
                {
                    return;
                }
                mediciondia = value;
                OnPropertyChanged();
            }
        }

        public int MedicionTotalRealEjecutada
        {
            get => mediciontotalrealejecutada;
            set
            {
                if (mediciontotalrealejecutada == value)
                {
                    return;
                }
                mediciontotalrealejecutada = value;
                OnPropertyChanged();
            }
        }

        public int HorasObjetivo
        {
            get => horasobjetivo;
            set
            {
                if (horasobjetivo == value)
                {
                    return;
                }
                horasobjetivo = value;
                OnPropertyChanged();
            }
        }

        public int HorasAcumuladas
        {
            get => horasacumuladas;
            set
            {
                if (horasacumuladas == value)
                {
                    return;
                }
                horasacumuladas = value;
                OnPropertyChanged();
            }
        }

        public int HorasRestantes
        {
            get => horasrestantes;
            set
            {
                if (horasrestantes == value)
                {
                    return;
                }
                horasrestantes = value;
                OnPropertyChanged();
            }
        }

        public int MedicionObjetivo
        {
            get => medicionobjetivo;
            set
            {
                if (medicionobjetivo == value)
                {
                    return;
                }
                medicionobjetivo = value;
                OnPropertyChanged();
            }
        }

        public int MedicionAcumuladas
        {
            get => medicionacumuladas;
            set
            {
                if (medicionacumuladas == value)
                {
                    return;
                }
                medicionacumuladas = value;
                OnPropertyChanged();
            }
        }

        public int MedicionRestantes
        {
            get => medicionrestantes;
            set
            {
                if (medicionrestantes == value)
                {
                    return;
                }
                medicionrestantes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemDTO> Herramienta1
        {
            get => herramienta1;
            set
            {
                if (herramienta1 == value)
                {
                    return;
                }
                herramienta1 = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemDTO> Herramienta2
        {
            get => herramienta2;
            set
            {
                if (herramienta2 == value)
                {
                    return;
                }
                herramienta2 = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemDTO> Herramienta3
        {
            get => herramienta3;
            set
            {
                if (herramienta3 == value)
                {
                    return;
                }
                herramienta3 = value;
                OnPropertyChanged();
            }
        }

        public int Kilometros
        {
            get => kilometros;
            set
            {
                if (kilometros == value)
                {
                    return;
                }
                kilometros = value;
                OnPropertyChanged();
            }
        }

        public int Euros
        {
            get => euros;
            set
            {
                if (euros == value)
                {
                    return;
                }
                euros = value;
                OnPropertyChanged();
            }
        }

        public string Descripcion
        {
            get => descripcion;
            set
            {
                if (descripcion == value)
                {
                    return;
                }
                descripcion = value;
                OnPropertyChanged();
            }
        }

        public bool Dieta
        {
            get => dieta;
            set
            {
                if (dieta == value)
                {
                    return;
                }
                dieta = value;
                OnPropertyChanged();
            }
        }

        public bool Incidencia
        {
            get => incidencia;
            set
            {
                if (incidencia == value)
                {
                    return;
                }
                incidencia = value;
                OnPropertyChanged();
            }
        }

        public string Materiales
        {
            get => materiales;
            set
            {
                if (materiales == value)
                {
                    return;
                }
                materiales = value;
                OnPropertyChanged();
            }
        }

        public string Importe
        {
            get => importe;
            set
            {
                if (importe == value)
                {
                    return;
                }
                importe = value;
                OnPropertyChanged();
            }
        }

        public string PedidoSelected
        {
            get => pedidoSelected;
            set
            {
                if (pedidoSelected == value)
                {
                    return;
                }
                pedidoSelected = value;
                OnPropertyChanged();
            }
        }
        public string Herramienta1Selected
        {
            get => herramienta1Selected;
            set
            {
                if (herramienta1Selected == value)
                {
                    return;
                }
                herramienta1Selected = value;
                OnPropertyChanged();
            }
        }

        public string Herramienta2Selected
        {
            get => herramienta2Selected;
            set
            {
                if (herramienta2Selected == value)
                {
                    return;
                }
                herramienta2Selected = value;
                OnPropertyChanged();
            }
        }

        public string Herramienta3Selected
        {
            get => herramienta3Selected;
            set
            {
                if (herramienta3Selected == value)
                {
                    return;
                }
                herramienta3Selected = value;
                OnPropertyChanged();
            }
        }

        public string CapituloSelected
        {
            get => capituloSelected;
            set
            {
                if (capituloSelected == value)
                {
                    return;
                }
                capituloSelected = value;
                OnPropertyChanged();
            }
        }

        public string PartidaSelected
        {
            get => partidaSelected;
            set
            {
                if (partidaSelected == value)
                {
                    return;
                }
                partidaSelected = value;
                OnPropertyChanged();
            }
        }

        public string FaseSelected
        {
            get => faseSelected;
            set
            {
                if (faseSelected == value)
                {
                    return;
                }
                faseSelected = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ItemDTO> Proveedor
        {
            get => proveedor;
            set
            {
                if (proveedor == value)
                {
                    return;
                }
                proveedor = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemDTO> TipoIncidencia
        {
            get => tipoincidencia;
            set
            {
                if (tipoincidencia == value)
                {
                    return;
                }
                tipoincidencia = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadHerramientas()
        {
            try
            {
                imputaciones = new ImputacionesManager();
                ObservableCollection<ItemDTO> observable = await imputaciones.GetHerramientas();
                Herramienta1 = observable;
                Herramienta2 = observable;
                Herramienta3 = observable;
            }
            catch
            {
                throw;
            }
        }

        public async Task LoadPedidos()
        {
            try
            {
                imputaciones = new ImputacionesManager();
                ObservableCollection<ItemDTO> observable = await imputaciones.GetPedidos();
                if (observable != null) Pedidos = observable;
            }
            catch
            {
                throw;
            }
        }

        public async Task LoadCapitulos(int idPedido)
        {
            try
            {
                imputaciones = new ImputacionesManager();
                ObservableCollection<ItemDTO> observable = await imputaciones.GetCapitulos(idPedido);
                if (observable != null) Capitulo = observable;
            }
            catch
            {
                throw;
            }
        }

        public async Task LoadPartidas(int idPedido, int idCapitulo)
        {
            try
            {
                imputaciones = new ImputacionesManager();
                ObservableCollection<ItemDTO> observable = await imputaciones.GetPartidas(idPedido, idCapitulo);
                if (observable != null) Partida = observable;
            }
            catch
            {
                throw;
            }
        }

        public async Task LoadFases(int idPedidoPartida)
        {
            try
            {
                imputaciones = new ImputacionesManager();
                ObservableCollection<ItemDTO> observable = await imputaciones.GetFases(idPedidoPartida);
                if (observable != null) Fase = observable;
            }
            catch
            {
                throw;
            }
        }

        public async Task LoadMediciones(int idPedidoPartida)
        {
            try
            {
                imputaciones = new ImputacionesManager();
                ObservableCollection<ItemDTO> observable = await imputaciones.GetMediciones(idPedidoPartida);
                if (observable != null) Mediciones = observable;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> CreateImputacion()
        {
            try
            {
                ImputacionDTO imputacion = InicializarImputacion(0);
                var validationResults = _validator.Validate(imputacion);
                if (!validationResults.IsValid)
                {
                    await App.Current.MainPage.DisplayAlert("Información", validationResults.Errors[0].ErrorMessage, "Ok");
                    return false; 
                }
                imputaciones = new ImputacionesManager();
                bool bRetBol = await imputaciones.CreateImputacion(imputacion);
                if (bRetBol)
                {
                    await App.Current.MainPage.DisplayAlert("Información", "Se ha generado una nueva Imputación", "Ok");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Información", "No se ha generado la Imputación. Intente mas tarde", "Ok");
                }
                return true;
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Información", "No se ha generado la Imputación. Intente mas tarde", "Ok");
                return false;
            }
        }

        public async Task<bool> UpdateImputacion(ImputacionDTO imputacionOld)
        {
            try
            {
                ImputacionDTO imputacionNew = InicializarImputacion(imputacionOld.idImputacion);
                var validationResults = _validator.Validate(imputacionNew);
                if (!validationResults.IsValid)
                {
                    await App.Current.MainPage.DisplayAlert("Información", validationResults.Errors[0].ErrorMessage, "Ok");
                    return false;
                }
                imputaciones = new ImputacionesManager();
                bool bRetBol = await imputaciones.UpdateImputacion(imputacionNew, imputacionOld);
                if (bRetBol)
                {
                    await App.Current.MainPage.DisplayAlert("Información", "Se ha actualizado los datos de la Imputación", "Ok");
                    return true;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Información", "No se ha generado la Imputacion. Intente mas tarde", "Ok");
                    return false;
                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Información", "No se ha actualizado la Imputacion. Intente mas tarde", "Ok");
                return false;
            }
        }

        private ImputacionDTO InicializarImputacion(int idImputacion)
        {
            ItemDTO selectedPedido = null;
            ItemDTO selectedCapitulo = null;
            ItemDTO selectedPartida = null;
            ItemDTO selectedFase = null;
            ItemDTO selectedHerramienta1 = null;
            ItemDTO selectedHerramienta2 = null;
            ItemDTO selectedHerramienta3 = null;

            try
            {
                selectedPedido = string.IsNullOrEmpty(PedidoSelected.Trim()) ? null : Pedidos.First(p => p.Descripcion.Trim() == PedidoSelected.Trim()) ?? null;
                selectedCapitulo = string.IsNullOrEmpty(CapituloSelected.Trim()) ? null : Capitulo.First(p => p.Descripcion.Trim() == CapituloSelected.Trim()) ?? null;
                selectedPartida = string.IsNullOrEmpty(PartidaSelected.Trim()) ? null : Partida.First(p => p.Descripcion.Trim() == PartidaSelected.Trim()) ?? null;
                selectedFase = string.IsNullOrEmpty(FaseSelected.Trim()) ? null : Fase.First(p => p.Descripcion.Trim() == FaseSelected.Trim()) ?? null;

                selectedHerramienta1 = string.IsNullOrEmpty(Herramienta1Selected.Trim()) ? null : Herramienta1.First(p => p.Descripcion.Trim() == Herramienta1Selected.Trim()) ?? null;
                selectedHerramienta2 = string.IsNullOrEmpty(Herramienta2Selected.Trim()) ? null : Herramienta2.First(p => p.Descripcion.Trim() == Herramienta2Selected.Trim()) ?? null;
                selectedHerramienta3 = string.IsNullOrEmpty(Herramienta3Selected.Trim()) ? null : Herramienta3.First(p => p.Descripcion.Trim() == Herramienta3Selected.Trim()) ?? null;

                ImputacionDTO imputacion = new ImputacionDTO()
                {
                    idImputacion = idImputacion,
                    idTecnico = App.UserInfo.TecnicoIdTecnico,
                    idPedidoDetalle = selectedPartida == null ? 0 : selectedPartida.Id,
                    idMedicion = 0,
                    idFase = selectedFase == null ? 0 : selectedFase.Id,
                    idPedido = selectedPedido == null ? 0 : selectedPedido.Id,
                    Comentario = Comentario,
                    Fecha = Fecha,
                    Horas = HorasDia,
                    MedicionDia = MedicionDia,
                    Kilometros = Kilometros,
                    Euros = Euros,
                    Herramienta1 = selectedHerramienta1 == null ? 0 : selectedHerramienta1.Id,
                    Herramienta2 = selectedHerramienta2 == null ? 0 : selectedHerramienta2.Id,
                    Herramienta3 = selectedHerramienta3 == null ? 0 : selectedHerramienta3.Id,
                    Dieta = Dieta
                };
                return imputacion;
            }
            catch
            {
                return new ImputacionDTO()
                {
                    idTecnico = App.UserInfo.TecnicoIdTecnico,
                    idPedido = selectedPedido == null ? 0 : selectedPedido.Id,
                    idFase = selectedFase == null ? 0 : selectedFase.Id,
                    idPedidoDetalle = selectedPartida == null ? 0 : selectedPartida.Id,
                    Fecha = Fecha,
                    Horas = HorasDia,
                    MedicionDia = MedicionDia
                };
            }
        }
    }
}
