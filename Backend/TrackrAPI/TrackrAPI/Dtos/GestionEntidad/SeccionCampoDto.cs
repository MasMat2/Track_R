using TrackrAPI.Models;
using System;
using System.Collections.Generic;

namespace TrackrAPI.Dtos.GestionEntidad
{
    public class SeccionCampoDto
    {
        public int IdSeccionCampo { get; set; }
        public int IdSeccion { get; set; }
        public string TipoCampo { get; set; }
        public string TipoDato { get; set; }
        public string Descripcion { get; set; }
        public int TamanoColumna { get; set; }
        public int? LongitudMaxima { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public bool Requerido { get; set; }
        public DateTime? FechaMinima { get; set; }
        public DateTime? FechaMaxima { get; set; }
        public ICollection<DominioDetalle> ListaOpciones { get; set; }
        public string Clave { get; set; }
        public int IdDominio { get; set; }
        public int Orden { get; set; }
        public bool? Deshabilitado { get; set; }
        public string ClaveSeccion { get; set; }
        public string NombreDominio { get; set; }
    }
}
