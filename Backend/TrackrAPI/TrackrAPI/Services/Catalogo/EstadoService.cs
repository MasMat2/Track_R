using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Services.Catalogo
{
    public class EstadoService
    {
        private IEstadoRepository estadoRepository;
        private EstadoValidatorService estadoValidatorService;

        public EstadoService(IEstadoRepository estadoRepository,
            EstadoValidatorService estadoValidatorService)
        {
            this.estadoRepository = estadoRepository;
            this.estadoValidatorService = estadoValidatorService;
        }

        public EstadoDto ConsultarDto(int idEstado)
        {
            var estado = estadoRepository.ConsultarDto(idEstado);
            estadoValidatorService.ValidarExistencia(estado);
            return estado;
        }

        public IEnumerable<EstadoGridDto> ConsultarTodosParaGrid()
        {
            return estadoRepository.ConsultarTodosParaGrid();
        }

        public IEnumerable<EstadoSelectorDto> ConsultarPorPaisParaSelector(int idPais)
        {
            return estadoRepository.ConsultarPorPaisParaSelector(idPais);
        }

        public void Agregar(Estado estado)
        {
            estadoValidatorService.ValidarAgregar(estado);
            estadoRepository.Agregar(estado);
        }

        public void Editar(Estado estado)
        {
            estadoValidatorService.ValidarEditar(estado);
            estadoRepository.Editar(estado);
        }

        public void Eliminar(int idEstado)
        {
            var  estado = estadoRepository.Consultar(idEstado);
            estadoValidatorService.ValidarEliminar(idEstado);
            estadoRepository.Eliminar(estado);
        }

        public IEnumerable<EstadoSelectorDto> ConsultarGeneral()
        {
            return estadoRepository.ConsultarGeneral();
        }
    }
}
