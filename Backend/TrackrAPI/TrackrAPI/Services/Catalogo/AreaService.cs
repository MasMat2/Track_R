using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Services.Catalogo
{
    public class AreaService
    {
        private IAreaRepository areaRepository;
        private AreaValidatorService areaValidatorService;

        public AreaService(IAreaRepository areaRepository, AreaValidatorService areaValidatorService)
        {
            this.areaRepository = areaRepository;
            this.areaValidatorService = areaValidatorService;
        }

        public AreaDto ConsultarDto(int idArea)
        {
            var area = areaRepository.ConsultarDto(idArea);
            return area;
        }

        public Area Consultar(string nombre)
        {
            return areaRepository.Consultar(nombre);
        }

        public IEnumerable<AreaDto> ConsultarParaSelector(int idCompania)
        {
            return areaRepository.ConsultarParaSelector(idCompania);
        }

        public void Agregar(Area area)
        {
            areaValidatorService.ValidarAgregar(area);

            areaRepository.Agregar(area);
        }

        public void Editar(Area area)
        {
            areaValidatorService.ValidarEditar(area);

            areaRepository.Editar(area);
        }

        public void Eliminar(int idArea)
        {
            var area = areaRepository.Consultar(idArea);

            areaValidatorService.ValidarEliminar(area);

            areaRepository.Eliminar(area);
        }

    }
}
