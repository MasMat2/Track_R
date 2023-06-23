using System.Transactions;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class GeneroService
    {
       private readonly IGeneroRepository _generoRepository;

       private readonly GeneroService _generoService;

       public GeneroService(IGeneroRepository generoRepository)
       {
        _generoRepository = generoRepository;
       }

       public GeneroDto? Consultar(int idGenero)
       {
        var genero = _generoRepository.Consultar(idGenero);

        if(genero == null)
        {
            return null;
        }

        var generoDto = new GeneroDto
        {
            IdGenero = genero.IdGenero,
            Descripcion = genero.Descripcion
        };

        return generoDto;
       }

        public IEnumerable<GeneroDto> Consultar()
        {
            var generos = _generoRepository.Consultar();
            var generosDto = new List<GeneroDto>();
            foreach(var genero in generos)
            {
                var generoDto = new GeneroDto
            {
                IdGenero = genero.IdGenero,
                Descripcion = genero.Descripcion
            };
                generosDto.Add(generoDto);
            }
                return generosDto;
            }
        public void Agregar(GeneroDto generoDto)
        {
            var genero = new Genero
            {
                Descripcion = generoDto.Descripcion
            };
            _generoRepository.Agregar(genero);

        }    


        public void Editar(GeneroDto generoDto)
        {
            var genero = _generoRepository.Consultar(generoDto.IdGenero);
            if( genero == null)
            {
                throw new CdisException("El usuario no existe");
            }
                _generoRepository.Editar(genero);
        }
        public void Eliminar(int idGenero)
        {
            var genero = _generoRepository.Consultar(idGenero);
            if( genero == null)
            {
                throw new CdisException("El usuario no existe");
            }
            _generoRepository.Eliminar(genero);
            }


            
    }
}