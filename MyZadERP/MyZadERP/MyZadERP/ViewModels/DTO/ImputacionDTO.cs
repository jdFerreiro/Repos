using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyZadERP.ViewModels.DTO
{
    public class ImputacionDTO
    {
        [JsonProperty("idImputacion")]
        public int idImputacion { get; set; }
        [JsonProperty("idPedido")]
        public int idPedido { get; set; }
        [JsonProperty("pedidoDetalle")]
        public string PedidoDetalle { get; set; }
        [JsonProperty("pedido")]
        public string Pedido { get; set; }
        [JsonProperty("descripcionPedido")]
        public string DescripcionPedido { get; set; }
        [JsonProperty("fecha")]
        public DateTime Fecha { get; set; }
        [JsonProperty("horas")]
        public double Horas { get; set; }
        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }
        [JsonProperty("incidencia")]
        public int Incidencia { get; set; }
        [JsonProperty("idMovimientoIncidencia")]
        public int IdMovimientoIncidencia { get; set; }
        [JsonProperty("importeIncidencia")]
        public decimal ImporteIncidencia { get; set; }
        [JsonProperty("fueraDePedido")]
        public int FueraDePedido { get; set; }
        [JsonProperty("idMovimientoFueraPedido")]
        public int IdMovimientoFueraPedido { get; set; }
        [JsonProperty("importeFueraPedido")]
        public double ImporteFueraPedido { get; set; }
        [JsonProperty("coste")]
        public double Coste { get; set; }
        [JsonProperty("kilometros")]
        public double Kilometros { get; set; }
        [JsonProperty("euros")]
        public double Euros { get; set; }
        [JsonProperty("herramienta1")]
        public int Herramienta1 { get; set; }
        [JsonProperty("herramienta2")]
        public int Herramienta2 { get; set; }
        [JsonProperty("herramienta3")]
        public int Herramienta3 { get; set; }
        public int idMedicion { get; set; }
        public int idFase { get; set; }
        public int idPedidoDetalle { get; set; }
        public int idTecnico { get; set; }
        public double MedicionDia { get; set; }
        public bool Dieta { get; set; }

        public string CodigoPedido => PedidoDetalle.Split('-')[0].Trim();
        [JsonProperty("Descripcion_Herramienta_1")]
        public string DescripcionHerramienta1 { get; set; }
        [JsonProperty("Descripcion_Herramienta_2")]
        public string DescripcionHerramienta2 { get; set; }
        [JsonProperty("Descripcion_Herramienta_3")]
        public string DescripcionHerramienta3 { get; set; }
        public int idPartida { get; set; }
        public int idCapitulo { get; set; }
        [JsonProperty("Descripcion_Capitulo")]
        public string DescripcionCapitulo { get; set; }
        [JsonProperty("Descripcion_Partida")]
        public string DescripcionPartida { get; set; }
        [JsonProperty("Descripcion_Fase")]
        public string DescripcionFase { get; set; }
        public string Comentario { get; set; }
    }

    public class ImputacionValidator : AbstractValidator<ImputacionDTO>
    {
        public ImputacionValidator ()
        {
            RuleFor(x => x.idTecnico).GreaterThan(0).WithMessage("Debe seleccionar el Tecnico");
            RuleFor(x => x.idPedido).GreaterThan(0).WithMessage("Debe seleccionar el Pedido");
            RuleFor(x => x.idPedidoDetalle).GreaterThan(0).WithMessage("Debe seleccionar la Partida");
            RuleFor(x => x.Horas).GreaterThan(0).WithMessage("Determine la Cantidad de Horas");
            RuleFor(x => x.MedicionDia).GreaterThan(0).WithMessage("Determine la Medición del Día");
            //RuleFor(x => x.idMedicion).LessThanOrEqualTo(0).WithMessage("Debe seleccionar la Medición");
            //RuleFor(x => x.idFase).GreaterThan(0).WithMessage("Debe seleccionar la Fase");
            //RuleFor(x => x.Kilometros).GreaterThan(0).WithMessage("Determine los Kilometros");
            //RuleFor(x => x.Euros).GreaterThan(0).WithMessage("Determine el valor en Euros");
            //RuleFor(x => x.Herramienta1).GreaterThan(0).WithMessage("Seleccione la primera Herramienta");
            //RuleFor(x => x.Herramienta2).GreaterThan(0).WithMessage("Seleccione la segunda Herramienta");
            //RuleFor(x => x.Herramienta3).GreaterThan(0).WithMessage("Seleccione la tercera Herramienta");
        }

    }
}
