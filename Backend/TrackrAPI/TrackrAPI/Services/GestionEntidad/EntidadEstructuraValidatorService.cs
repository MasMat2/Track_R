using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Services.GestionEntidad
{
    public class EntidadEstructuraValidatorService
    {
        private readonly IEntidadEstructuraRepository entidadEstructuraRepository;

        public EntidadEstructuraValidatorService(IEntidadEstructuraRepository entidadEstructuraRepository)
        {
            this.entidadEstructuraRepository = entidadEstructuraRepository;
        }

        private readonly string MensajeExistencia = "La estructura de entidad no existe";
        private readonly string MensajeEntidadRequerida = "La entidad es requerida";
        private readonly string MensajePestanaDuplicada = "La pestaña ya existe";
        private readonly string MensajeSeccionDuplicada = "La sección ya existe en la pestaña actual";

        private static readonly int LongitudNombre = 100;
        private static readonly int LongitudClave = 10;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";
        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave } caracteres";

        public void ValidarAgregar(EntidadEstructura entidadEstructura)
        {
            ValidarRequerido(entidadEstructura);
            ValidarRango(entidadEstructura);
            ValidarDuplicado(entidadEstructura);
        }

        public void ValidarEditar(EntidadEstructura entidadEstructura)
        {
            ValidarRequerido(entidadEstructura);
            ValidarRango(entidadEstructura);
            ValidarExistencia(entidadEstructura.IdEntidadEstructura);
            ValidarDuplicado(entidadEstructura);
        }

        public void ValidarEliminar(int idEntidadEstructura)
        {
            ValidarExistencia(idEntidadEstructura);
        }

        private void ValidarRequerido(EntidadEstructura entidadEstructura)
        {
            Validator.ValidarRequerido(entidadEstructura.IdEntidad, MensajeEntidadRequerida);
        }

        private void ValidarRango(EntidadEstructura entidadEstructura)
        {
            Validator.ValidarLongitudRangoString(entidadEstructura.Clave, LongitudClave, MensajeClaveLongitud);
            Validator.ValidarLongitudRangoString(entidadEstructura.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        private void ValidarExistencia(int idEntidadEstructura)
        {
            var entidadEstructura = entidadEstructuraRepository.Consultar(idEntidadEstructura);

            if (entidadEstructura == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        private void ValidarDuplicado(EntidadEstructura entidadEstructura)
        {
            if (entidadEstructura.Tabulacion == true)
            {
                EntidadEstructura pestanaExistente = entidadEstructuraRepository.ConsultarTabulacionDuplicada(entidadEstructura.Clave, entidadEstructura.Nombre, entidadEstructura.IdEntidad);

                if (pestanaExistente != null && pestanaExistente.IdEntidadEstructura != entidadEstructura.IdEntidadEstructura)
                    throw new CdisException(MensajePestanaDuplicada);
            }
            else
            {
                List<EntidadEstructura> estructurasExistentes = entidadEstructuraRepository.ConsultarPorEntidadSeccion(entidadEstructura.IdEntidad, (int)entidadEstructura.IdSeccion).ToList();

                if (estructurasExistentes.Any(e =>
                    e.IdEntidadEstructuraPadre == entidadEstructura.IdEntidadEstructuraPadre &&
                    e.IdEntidadEstructura != entidadEstructura.IdEntidadEstructura))
                {
                    throw new CdisException(MensajeSeccionDuplicada);
                }
            }
        }
    }
}
