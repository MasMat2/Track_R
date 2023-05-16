using System;
using TrackrAPI.Models;

namespace TrackrAPI.Dtos.Catalogo
{
    public class DominioDto
    {
        public int IdDominio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string TipoDato { get; set; }
        public string TipoCampo { get; set; }
        public int? LongitudMinima { get; set; }
        public int? LongitudMaxima { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public DateTime? FechaMinima { get; set; }
        public DateTime? FechaMaxima { get; set; }

        public DominioDto()
        {
        }

        public DominioDto(int IdDominio, string Nombre, string Descripcion, string TipoDato, string TipoCampo, int? LongitudMinima, int? LongitudMaxima, decimal? ValorMinimo, decimal? ValorMaximo, DateTime? FechaMinima, DateTime? FechaMaxima)
        {
            this.IdDominio = IdDominio;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.TipoDato = TipoDato;
            this.TipoCampo = TipoCampo;
            this.LongitudMinima = LongitudMinima;
            this.LongitudMaxima = LongitudMaxima;
            this.ValorMinimo = ValorMinimo;
            this.ValorMaximo = ValorMaximo;
            this.FechaMinima = FechaMinima;
            this.FechaMaxima = FechaMaxima;
        }
    }
}
