using System;

namespace TrackrAPI.Dtos.Catalogo
{
    public class GeneroDto
    {
        public int IdUsuario { get; set; }
        public string? TipoDeGenero { get; set; }

        /*   public GeneroDto(int idUsuario, string tipoDeGenero)
           {
               this.IdUsuario = idUsuario;
               this.TipoDeGenero = tipoDeGenero;
           }

           public GeneroDto() {}*/
    }
}