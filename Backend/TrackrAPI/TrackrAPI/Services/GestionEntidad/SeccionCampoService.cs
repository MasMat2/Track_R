using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;
using System.Collections.Generic;

namespace TrackrAPI.Services.GestionEntidad
{
    public class SeccionCampoService
    {
        private readonly ISeccionCampoRepository seccionCampoRepository;
        private readonly SeccionCampoValidatorService seccionCampoValidatorService;

        public SeccionCampoService(ISeccionCampoRepository seccionCampoRepository,
            SeccionCampoValidatorService seccionCampoValidatorService)
        {
            this.seccionCampoRepository = seccionCampoRepository;
            this.seccionCampoValidatorService = seccionCampoValidatorService;
        }
        public SeccionCampo Consultar(int idSeccionCampo)
        {
            var seccionCampo = seccionCampoRepository.Consultar(idSeccionCampo);
            seccionCampoValidatorService.ValidarExistencia(seccionCampo);
            return seccionCampo;
        }
        public IEnumerable<SeccionCampoDto> ConsultarPorSeccion(int idSeccion)
        {
            return seccionCampoRepository.ConsultarPorSeccion(idSeccion);
        }

        public void Agregar(SeccionCampo seccionCampo)
        {
            seccionCampoValidatorService.ValidarAgregar(seccionCampo);
            seccionCampoRepository.Agregar(seccionCampo);
        }
        public void Editar(SeccionCampo seccionCampo)
        {
            seccionCampoValidatorService.ValidarEditar(seccionCampo);
            seccionCampoRepository.Editar(seccionCampo);
        }

        public void Eliminar(int idSeccionCampo)
        {
            var seccionCampo = seccionCampoRepository.Consultar(idSeccionCampo);

            seccionCampoValidatorService.ValidarEliminar(idSeccionCampo);
            seccionCampoRepository.Eliminar(seccionCampo);
        }
    }
}
