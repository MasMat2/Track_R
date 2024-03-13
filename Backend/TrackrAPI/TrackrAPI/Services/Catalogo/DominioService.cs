using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Helpers;

namespace TrackrAPI.Services.Catalogo
{
    public class DominioService
    {
        private readonly IDominioRepository dominioRepository;
        private readonly DominioValidatorService dominioValidatorService;

        public DominioService(
            IDominioRepository dominioRepository,
            DominioValidatorService dominioValidatorService)
        {
            this.dominioRepository = dominioRepository;
            this.dominioValidatorService = dominioValidatorService;
        }
        public DominioDto ConsultarDto(int idDominio)
        {
            var dominio = dominioRepository.ConsultarDto(idDominio);
            dominioValidatorService.ValidarExistencia(dominio);
            return dominio!;
        }

        public Dominio? Consultar(int idDominio)
        {
            return dominioRepository.Consultar(idDominio);
        }

        public Dominio? Consultar(string nombre)
        {
            return dominioRepository.Consultar(nombre);
        }

        public IEnumerable<DominioGridDto> ConsultarTodosParaGrid(int idUsuarioSesion)
        {
            return dominioRepository.ConsultarTodosParaGrid(idUsuarioSesion);
        }

        public IEnumerable<DominioDto> ConsultarTodosParaSelector()
        {
            return dominioRepository.ConsultarTodosParaSelector();
        }

        public int Agregar(Dominio dominio)
        {
            dominioValidatorService.ValidarAgregar(dominio);
            dominioRepository.Agregar(dominio);
            return dominio.IdDominio;
        }

        public void Editar(Dominio dominio)
        {
            dominioValidatorService.ValidarEditar(dominio);
            dominioRepository.Editar(dominio);
        }

        public void Eliminar(int idDominio)
        {
            var dominio = dominioRepository.Consultar(idDominio);

            if (dominio is null)
            {
                throw new CdisException("El dominio no existe");
            }

            dominioValidatorService.ValidarEliminar(idDominio);
            dominioRepository.Eliminar(dominio);
        }
    }
}
