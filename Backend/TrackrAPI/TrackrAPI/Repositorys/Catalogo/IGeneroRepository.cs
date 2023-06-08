using TrackrAPI.Dtos.Genero;
using TrackrAPI.Models;
using TrackrAPI.Repositorys;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Dtos.Catalogo
{
    public interface IGeneroRepository
    {
       public GeneroDto? Consultar(string tipoDeGenero);

       public GeneroDto? ConsultarConId(int idUsuario);
    }
}