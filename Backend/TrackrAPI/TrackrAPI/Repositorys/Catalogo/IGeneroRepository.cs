using System.Data;
using TrackrAPI.Models;
using TrackrAPI.Repositorys;

namespace TrackrAPI.Dtos.Catalogo
{
    public interface IGeneroRepository : IRepository<Genero>
    {
        public Genero? Consultar(int idGenero);

        public IEnumerable<Genero> Consultar();
       
    }
}