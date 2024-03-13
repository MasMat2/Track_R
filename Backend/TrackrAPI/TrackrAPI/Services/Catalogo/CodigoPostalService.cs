using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class CodigoPostalService
    {
        private ICodigoPostalRepository codigoPostalRepository;
        private CodigoPostalValidatorService codigoPostalValidatorService;

        public CodigoPostalService(ICodigoPostalRepository codigoPostalRepository,
             CodigoPostalValidatorService codigoPostalValidatorService)
        {
            this.codigoPostalRepository = codigoPostalRepository;
            this.codigoPostalValidatorService = codigoPostalValidatorService;
        }

        public CodigoPostalDto ConsultarDto(int idCodigoPostal)
        {
            var codigoPostal = codigoPostalRepository.ConsultarDto(idCodigoPostal);
            codigoPostalValidatorService.ValidarExistencia(codigoPostal);
            return codigoPostal;
        }
        public IEnumerable<CodigoPostalGridDto> ConsultarTodosParaGrid()
        {
            return codigoPostalRepository.ConsultarTodosParaGrid();
        }

        public IEnumerable<CodigoPostalDto> ConsultarPorCodigoPostal(string codigoPostal)
        {
            return codigoPostalRepository.ConsultarPorCodigoPostal(codigoPostal);
        }

        public IEnumerable<CodigoPostalDto> ConsultarPorMunicipio(int idMunicipio)
        {
            return codigoPostalRepository.ConsultarPorMunicipio(idMunicipio);

        }

        public IEnumerable<CodigoPostalDto> ConsultarPorPaisBusqueda(string codigoPostal, int idPais)
        {
            return codigoPostalRepository.ConsultarPorPaisBusqueda(codigoPostal, idPais);
        }

        public void Agregar(CodigoPostal codigoPostal)
        {
            codigoPostalValidatorService.ValidarAgregar(codigoPostal);
            codigoPostalRepository.Agregar(codigoPostal);
        }

        public void Editar(CodigoPostal codigoPostal)
        {
            codigoPostalValidatorService.ValidarEditar(codigoPostal);
            codigoPostalRepository.Editar(codigoPostal);
        }

        public void Eliminar(int idCodigoPostal)
        {
            var codigoPostal = codigoPostalRepository.Consultar(idCodigoPostal);
            codigoPostalValidatorService.ValidarEliminar(idCodigoPostal);
            codigoPostalRepository.Eliminar(codigoPostal);
        }



    }
}
