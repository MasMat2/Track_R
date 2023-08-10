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

        public IEnumerable<ExpedienteColumnaSelectorDTO> ConsultarSeccionesPadecimientos(int idPadecimiento)
        {
            // TODO: Conocer si se va a filtrar por la clave, porque hay distintas variables con distintas claves que tienen el mismo nombre
            IEnumerable<ExpedienteColumnaDTO> secciones = seccionCampoRepository.ConsultarSeccionesPadecimientos(idPadecimiento);
            var seccionesUnicas = new List<ExpedienteColumnaSelectorDTO>();
            foreach (var seccion in secciones)
            {
                seccionesUnicas.Add(new ExpedienteColumnaSelectorDTO
                {
                    Clave = seccion.Clave,
                    Variable = seccion.Parametro,
                });
            }
            
            // Agrupar por la Clave y seleccionar la primera instancia de cada grupo
            return seccionesUnicas;
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
