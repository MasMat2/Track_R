using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class TituloAcademicoService
    {
        private ITituloAcademicoRepository tituloAcademicoRepository;
        private TituloAcademicoValidatorService tituloAcademicoValidatorService;

        public TituloAcademicoService(ITituloAcademicoRepository tituloAcademicoRepository,
            TituloAcademicoValidatorService tituloAcademicoValidatorService)
        {
            this.tituloAcademicoRepository = tituloAcademicoRepository;
            this.tituloAcademicoValidatorService = tituloAcademicoValidatorService;
        }

        public IEnumerable<TituloAcademicoSelectorDto> ConsultarTodosParaSelector()
        {
            return tituloAcademicoRepository.ConsultarTodosParaSelector();
        }

        public TituloAcademicoDto ConsultarDto(int idTituloAcademico)
        {
            var tituloAcademico = tituloAcademicoRepository.ConsultarDto(idTituloAcademico);
            tituloAcademicoValidatorService.ValidarExistencia(tituloAcademico);
            return tituloAcademico;
        }

        public IEnumerable<TituloAcademicoGridDto> ConsultarTodosParaGrid()
        {
            return tituloAcademicoRepository.ConsultarTodosParaGrid();
        }

        public void Agregar(TituloAcademico tituloAcademico)
        {
            tituloAcademicoValidatorService.ValidarAgregar(tituloAcademico);
            tituloAcademicoRepository.Agregar(tituloAcademico);
        }

        public void Editar(TituloAcademico tituloAcademico)
        {
            tituloAcademicoValidatorService.ValidarEditar(tituloAcademico);
            tituloAcademicoRepository.Editar(tituloAcademico);
        }

        public void Eliminar(int idTituloAcademico)
        {
            var tituloAcademico = tituloAcademicoRepository.Consultar(idTituloAcademico);
            tituloAcademicoValidatorService.ValidarEliminar(idTituloAcademico);
            tituloAcademicoRepository.Eliminar(tituloAcademico);
        }

    }
}
