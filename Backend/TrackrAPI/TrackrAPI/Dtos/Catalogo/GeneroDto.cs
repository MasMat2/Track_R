using System;

namespace TrackrAPI.Dtos.Catalogo
{
    public class GeneroDto
    {
        public int IdGenero { get; set; }
        public string? Descripcion { get; set; }

        /*   public GeneroDto(int idUsuario, string tipoDeGenero)
           {
               this.IdUsuario = idUsuario;
               this.TipoDeGenero = tipoDeGenero;
           }

           public GeneroDto() {}*/
    }
}