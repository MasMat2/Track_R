using TrackrAPI.Repositorys;
using TrackrAPI.Models;
using System.Collections.Generic;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Dtos.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class PaisService
    {
        private IPaisRepository paisRepository;
        private PaisValidatorService paisValidatorService;

        public PaisService(IPaisRepository paisRepository, PaisValidatorService paisValidatorService)
        {
            this.paisRepository = paisRepository;
            this.paisValidatorService = paisValidatorService;
        }

        public IEnumerable<PaisDto> ConsultarTodosParaSelector()
        {
            return paisRepository.ConsultarTodosParaSelector();
        }

        public PaisDto ConsultarDto(string clave)
        {
            return paisRepository.ConsultarDto(clave);
        }

        public PaisDto ConsultarDto(int idPais)
        {
            return paisRepository.ConsultarDto(idPais);
        }

        public IEnumerable<PaisGridDto> ConsultarGeneral()
        {
            return paisRepository.ConsultarGeneral();
        }

        public void Agregar(Pais pais)
        {
            paisValidatorService.ValidarAgregar(pais);
            paisRepository.Agregar(pais);
        }

        public void Editar(Pais pais)
        {
            paisValidatorService.ValidarEditar(pais);
            paisRepository.Editar(pais);
        }

        public void Eliminar(int idPais)
        {
            Pais pais = paisRepository.Consultar(idPais);
            paisValidatorService.ValidarEliminar(idPais);
            paisRepository.Eliminar(pais);
        }

    }
}
