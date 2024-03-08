using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Contabilidad;
using System.Collections.Generic;

namespace TrackrAPI.Services.Contabilidad
{
    public class AgrupadorCuentaContableService
    {
        private IAgrupadorCuentaContableRepository agrupadorCuentaContableRepository;
        private AgrupadorCuentaContableValidatorService agrupadorCuentaContableValidatorService;

        public AgrupadorCuentaContableService(
            IAgrupadorCuentaContableRepository agrupadorCuentaContableRepository,
            AgrupadorCuentaContableValidatorService agrupadorCuentaContableValidatorService
        )
        {
            this.agrupadorCuentaContableRepository = agrupadorCuentaContableRepository;
            this.agrupadorCuentaContableValidatorService = agrupadorCuentaContableValidatorService;
        }

        public AgrupadorCuentaContableDto ConsultarDto(int idAgrupadorCuentaContable)
        {
            return agrupadorCuentaContableRepository.ConsultarDto(idAgrupadorCuentaContable);
        }

        public IEnumerable<AgrupadorCuentaContableDto> ConsultarParaGrid()
        {
            return agrupadorCuentaContableRepository.ConsultarParaGrid();
        }

        public IEnumerable<AgrupadorCuentaContableDto> ConsultarParaSelector()
        {
            return agrupadorCuentaContableRepository.ConsultarParaSelector();
        }

        public void Agregar(AgrupadorCuentaContableDto agrupadorDto)
        {
            AgrupadorCuentaContable agrupador = new AgrupadorCuentaContable()
            {
                Clave = agrupadorDto.Clave,
                Descripcion = agrupadorDto.Descripcion,
                IdCuentaContableCapital = agrupadorDto.IdCuentaContableCapital,
                IdCuentaContableResultado = agrupadorDto.IdCuentaContableResultado
            };

            agrupadorCuentaContableValidatorService.ValidarAgregar(agrupador);
            agrupadorCuentaContableRepository.Agregar(agrupador);
        }

        public void Editar(AgrupadorCuentaContableDto agrupadorDto)
        {
            AgrupadorCuentaContable agrupador = new AgrupadorCuentaContable()
            {
                IdAgrupadorCuentaContable = agrupadorDto.IdAgrupadorCuentaContable,
                Clave = agrupadorDto.Clave,
                Descripcion = agrupadorDto.Descripcion,
                IdCuentaContableCapital = agrupadorDto.IdCuentaContableCapital,
                IdCuentaContableResultado = agrupadorDto.IdCuentaContableResultado
            };

            agrupadorCuentaContableValidatorService.ValidarEditar(agrupador);
            agrupadorCuentaContableRepository.Editar(agrupador);
        }

        public void Eliminar(int idAgrupadorCuentaContable)
        {
            agrupadorCuentaContableValidatorService.ValidarEliminar(idAgrupadorCuentaContable);

            AgrupadorCuentaContable agrupador = agrupadorCuentaContableRepository.Consultar(idAgrupadorCuentaContable);
            agrupadorCuentaContableRepository.Eliminar(agrupador);
        }
    }
}
