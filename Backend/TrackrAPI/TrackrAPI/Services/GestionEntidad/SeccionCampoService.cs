using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;
using System.Collections.Generic;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.GestionEntidad
{
    public class SeccionCampoService
    {
        private readonly SeccionCampoValidatorService seccionCampoValidatorService;
        private readonly ISeccionCampoRepository seccionCampoRepository;
        private readonly IExpedientePadecimientoRepository expedientePadecimientoRepository;

        public SeccionCampoService(ISeccionCampoRepository seccionCampoRepository,
            SeccionCampoValidatorService seccionCampoValidatorService,
            IExpedientePadecimientoRepository expedientePadecimientoRepository)
        {
            this.seccionCampoRepository = seccionCampoRepository;
            this.seccionCampoValidatorService = seccionCampoValidatorService;
            this.expedientePadecimientoRepository = expedientePadecimientoRepository;
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
                    IdSeccionVariable = seccion.IdSeccionVariable,
                    Variable = seccion.Parametro,
                });
            }
            
            // Agrupar por la Clave y seleccionar la primera instancia de cada grupo
            return seccionesUnicas;
        }

        public IEnumerable<PadecimientoMuestraDTO> ConsultarSeccionesPadecimientosGeneral(int idUsuario)
        {
            List<PadecimientoMuestraDTO> padecimientoMuestras = new List<PadecimientoMuestraDTO>();
            // Obtener todos los padecimientos que tiene el Usuario
            List<ExpedientePadecimientoDTO> padecimientos = expedientePadecimientoRepository.ConsultarPorUsuario(idUsuario).ToList();
            foreach(var padecimiento in padecimientos)
            {
                var padecimientoMuestra = new PadecimientoMuestraDTO();
                padecimientoMuestra.IdPadecimiento = padecimiento.IdPadecimiento;
                padecimientoMuestra.NombrePadecimiento = padecimiento.NombrePadecimiento;
                // Obtener cada sección del padecimiento actual
                List<ExpedienteColumnaDTO> secciones = seccionCampoRepository.ConsultarSeccionesPadecimientos(padecimiento.IdPadecimiento).ToList();
                List <SeccionMuestraDTO> seccionesList = new List<SeccionMuestraDTO>();
                // Agrupar las secciones por su ClaveSeccion
                var seccionesAgrupadas = secciones.GroupBy(x => x.ClaveSeccion);
                foreach (var seccion in seccionesAgrupadas)
                {
                    var seccionMuestraDTO = new SeccionMuestraDTO();
                    seccionMuestraDTO.ClaveCampo = seccion.Key;
                    // Ponerle nombre a la seccion que agrupó
                    seccionMuestraDTO.NombreSeccionCampo = seccion.Where(x => x.ClaveSeccion == seccion.Key).FirstOrDefault().Variable;
                    List<SeccionCampo> seccionesCampoList = new List<SeccionCampo>();
                    foreach (var seccionCampoDTO in seccion)
                    {
                        var seccionCampo = seccionCampoRepository.ConsultarPorClaveConDependencia(seccionCampoDTO.IdSeccionVariable);
                        if(seccionCampo == null)
                        {
                            continue;
                        }
                        seccionesCampoList.Add(seccionCampo); //Cambiar a SeccionCampo
                    }
                    seccionMuestraDTO.SeccionesCampo = seccionesCampoList;
                    seccionesList.Add(seccionMuestraDTO);
                }
                padecimientoMuestra.SeccionMuestraDTOs = seccionesList;
                padecimientoMuestras.Add(padecimientoMuestra);
            }
            return padecimientoMuestras;
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
