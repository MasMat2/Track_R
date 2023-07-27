using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Services.Catalogo
{
    public class DominioDetalleService
    {
        private IDominioDetalleRepository dominioDetalleRepository;
        private DominioDetalleValidatorService dominioDetalleValidatorService;

        public DominioDetalleService(IDominioDetalleRepository dominioDetalleRepository, DominioDetalleValidatorService dominioDetalleValidatorService)
        {
            this.dominioDetalleRepository = dominioDetalleRepository;
            this.dominioDetalleValidatorService = dominioDetalleValidatorService;
        }

        public DominioDetalleDto ConsultarDto(int idDominioDetalle)
        {
            var dominio = dominioDetalleRepository.ConsultarDto(idDominioDetalle);
            dominioDetalleValidatorService.ValidarExistencia(dominio);
            return dominio;
        }

        public IEnumerable<DominioDetalleGridDto> ConsultarPorDominio(int idDominio, int idCompania)
        {
            return dominioDetalleRepository.ConsultarPorDominio(idDominio, idCompania);
        }

        public void Agregar(DominioDetalle dominioDetalle)
        {
            dominioDetalleValidatorService.ValidarAgregar(dominioDetalle);
            dominioDetalle.Habilitado = true;
            dominioDetalleRepository.Agregar(dominioDetalle);
        }

        public void Editar(DominioDetalle dominioDetalle)
        {
            dominioDetalle.Habilitado = true;
            dominioDetalleValidatorService.ValidarEditar(dominioDetalle);
            dominioDetalleRepository.Editar(dominioDetalle);
        }

        public void Eliminar(int idDominioDetalle)
        {
            var dominioDetalle = dominioDetalleRepository.Consultar(idDominioDetalle);
            dominioDetalleValidatorService.ValidarEliminar(idDominioDetalle);

            // dominioDetalleRepository.Eliminar(dominioDetalle);

            dominioDetalle.Habilitado = false;
            dominioDetalleRepository.Editar(dominioDetalle);
        }
    }
}
