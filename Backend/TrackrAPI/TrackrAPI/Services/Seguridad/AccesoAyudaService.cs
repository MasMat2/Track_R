using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class AccesoAyudaService
    {
        private IAccesoAyudaRepository accesoAyudaRepository;
        private AccesoAyudaValidatorService accesoAyudaValidatorService;

        public AccesoAyudaService(IAccesoAyudaRepository accesoAyudaRepository,
            AccesoAyudaValidatorService accesoAyudaValidatorService)
        {
            this.accesoAyudaRepository = accesoAyudaRepository;
            this.accesoAyudaValidatorService = accesoAyudaValidatorService;
        }

        public AccesoAyuda Consultar(int idAccesoAyuda)
        {
            return accesoAyudaRepository.Consultar(idAccesoAyuda);
        }
        public IEnumerable<AccesoAyudaDto> ConsultarPorAcceso(int idAcceso)
        {
            return accesoAyudaRepository.ConsultarPorAcceso(idAcceso);
        }

        public AccesoAyudaDto ConsultarDto(int idAccesoAyuda)
        {
            return accesoAyudaRepository.ConsultarDto(idAccesoAyuda);
        }
        public IEnumerable<AccesoAyudaSeccionDto> ConsultarPorAccesoPorSeccion(int idAcceso)
        {
            IEnumerable<AccesoAyudaDto> accesoAyudas;
            accesoAyudas = accesoAyudaRepository.ConsultarPorAcceso(idAcceso);

            IEnumerable<int?> idSecciones;
            idSecciones = accesoAyudas.Select(a => a.IdAyudaSeccion).Distinct();

            List<AccesoAyudaSeccionDto> secciones = new();
            foreach (int? idSeccion in idSecciones)
            {
                IEnumerable<AccesoAyudaDto> accesosSeccion = accesoAyudas.Where(a => a.IdAyudaSeccion == idSeccion);

                AccesoAyudaSeccionDto seccion = new();
                seccion.IdAyudaSeccion = idSeccion;
                if (idSeccion == null)
                {
                    seccion.NombreAyudaSeccion = "Demás ayudas";
                }
                else
                {
                    seccion.NombreAyudaSeccion = accesosSeccion.FirstOrDefault().NombreAyudaSeccion;
                }
                seccion.AccesoAyudas = accesosSeccion;
                secciones.Add(seccion);
            }
            return secciones.OrderBy(a => a.IdAyudaSeccion == null).ThenBy(a => a.IdAyudaSeccion);
        }

        public AccesoAyuda Agregar(AccesoAyuda accesoAyuda)
        {
            accesoAyudaValidatorService.ValidarAgregar(accesoAyuda);
            accesoAyudaRepository.Agregar(accesoAyuda);
            return accesoAyuda;
        }

        public void Editar(AccesoAyuda accesoAyuda)
        {
            accesoAyudaValidatorService.ValidarEditar(accesoAyuda);
            accesoAyudaRepository.Editar(accesoAyuda);
        }

        public void Eliminar(int idAccesoAyuda)
        {
            AccesoAyuda accesoAyuda = accesoAyudaRepository.Consultar(idAccesoAyuda);
            accesoAyudaValidatorService.ValidarEliminar(idAccesoAyuda);
            accesoAyudaRepository.Eliminar(accesoAyuda);
        }

    }
}
