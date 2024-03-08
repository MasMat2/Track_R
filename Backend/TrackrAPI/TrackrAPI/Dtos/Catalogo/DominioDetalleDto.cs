using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Catalogo
{
    public class DominioDetalleDto
    {
        public int IdDominioDetalle { get; set; }
        public string Valor { get; set; }
        public int IdDominio { get; set; }
        public int? IdCompania { get; set; }

        public DominioDetalleDto(int idDominioDetalle, string Valor, int idDominio, int? idCompania)
        {
            this.IdDominioDetalle = idDominioDetalle;
            this.Valor = Valor;
            this.IdDominio = idDominio;
            this.IdCompania = idCompania;
        }
    }
}
