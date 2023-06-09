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
        private object generoRepository;

        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public GeneroDto Consultar(int idGenero)
        {
            var genero = _generoRepository.Consultar(idGenero);

            if (genero == null)
            {
                return null;
            }

            var generoDto = new GeneroDto
            {
                IdGenero = genero.IdGenero,
                Descripcion = genero.Descripcion,
            };

            return generoDto;
        }


        public void Agregar(GeneroDto generoDto, int idGenero)
        {
            using var gen = new TransactionScope();
            //Agregar el genero
            var genero = new Genero()
            {
                IdGenero =idGenero,
                Descripcion = generoDto.Descripcion
            };

            _generoRepository.Agregar(genero);

            generoDto.IdGenero = genero.IdGenero;
            gen.Complete();
        }

        public void Editar(GeneroDto generoDto)
        {
           var genero = _generoRepository.Consultar(generoDto.IdGenero);

           if (genero == null){
            throw new CdisException("El usuario no existe");
           }

           using var gen = new TransactionScope();

           genero.Descripcion = generoDto.Descripcion;

           _generoRepository.Editar(genero);
           gen.Complete();

        }

        public void Eliminar(int idGenero)
        {
            var genero = _generoRepository.Consultar(idGenero);
            
            if( genero == null)
            {
                throw new CdisException("El usuario no existe");
            }

            using var gen = new TransactionScope();

            //Eliminar al usuario

            _generoRepository.Eliminar(genero);
            gen.Complete();
        }

        internal void Agregar(GeneroDto generoDto)
        {
            throw new NotImplementedException();
        }
    }
}