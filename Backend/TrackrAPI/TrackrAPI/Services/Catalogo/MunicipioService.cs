using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Services.Catalogo
{
    public class MunicipioService
    {
        private IMunicipioRepository municipioRepository;
        private MunicipioValidatorService municipioValidatorService;

        public MunicipioService(IMunicipioRepository municipioRepository,
            MunicipioValidatorService municipioValidatorService) {
            this.municipioRepository = municipioRepository;
            this.municipioValidatorService = municipioValidatorService;
        }
        public IEnumerable<MunicipioDto> ConsultarTodosParaSelector()
        {
            return municipioRepository.ConsultarTodosParaSelector();
        }

        public MunicipioDto ConsultarDto(int idMunicipio)
        {
            var municipio = municipioRepository.ConsultarDto(idMunicipio);
            municipioValidatorService.ValidarExistencia(municipio);
            return municipio;
        }

        public IEnumerable<MunicipioGridDto> ConsultarTodosParaGrid()
        {
            return municipioRepository.ConsultarTodosParaGrid();
        }

        public IEnumerable<EstadoSelectorDto> ConsultarPorPaisParaSelector(int idPais)
        {
            return municipioRepository.ConsultarPorPaisParaSelector(idPais);
        }

        public IEnumerable<MunicipioDto> ConsultarPorEstadoParaSelector(int idEstado)
        {
            return municipioRepository.ConsultarPorEstadoParaSelector(idEstado);
        }

        public void Agregar(Municipio municipio)
        {
            municipioValidatorService.ValidarAgregar(municipio);
            municipioRepository.Agregar(municipio);
        }

        public void Editar(Municipio municipio)
        {
            municipioValidatorService.ValidarEditar(municipio);
            municipioRepository.Editar(municipio);
        }

        public void Eliminar(int idMunicipio)
        {
            var municipio = municipioRepository.Consultar(idMunicipio);
            municipioValidatorService.ValidarEliminar(idMunicipio);
            municipioRepository.Eliminar(municipio);
        }
    }
}
