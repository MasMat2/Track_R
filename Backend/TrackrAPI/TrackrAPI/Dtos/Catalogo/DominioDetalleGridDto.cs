using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Catalogo
{
    public class DominioDetalleGridDto
    {
        public int IdDominioDetalle { get; set; }
        public string Valor { get; set; }
        public int IdDominio { get; set; }

        public DominioDetalleGridDto(int idDominioDetalle, string Valor, int idDominio)
        {
            this.IdDominioDetalle = idDominioDetalle;
            this.Valor = Valor;
            this.IdDominio = idDominio;
        }
    }
}
