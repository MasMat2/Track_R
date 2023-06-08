using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Genero;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Genero;

namespace TrackrAPI.Services.Catalogo{
    public class GeneroService
    {
        private readonly GeneroRepository generoRepository;

        public GeneroService(GeneroRepository generoRepository){
            this.generoRepository = generoRepository;
        }

        public GeneroDto ConsultarDto(int idUsuario)
        {
            var genero = generoRepository.ConsultarDto(idUsuario);
             return (GeneroDto)genero;
        }
        public IEnumerable<GeneroDto>ConsultarPorTipoDeGenero(int idUsuario, string tipoDeGenero)
        {
            return generoRepository.ConsultarPorTipoDeGenero(tipoDeGenero);
        }

        
        public void Agregar(GeneroDto generoDto)
        {
           generoRepository.Agregar(generoDto);
        }

        public void Editar(GeneroDto generoDto)
        {
            generoRepository.Editar(generoDto);
        }

        public void Eliminar(int idUsuario)
        {
             var genero = generoRepository.ConsultarDto(idUsuario);
             generoRepository.Eliminar(genero);
        }

    }
}