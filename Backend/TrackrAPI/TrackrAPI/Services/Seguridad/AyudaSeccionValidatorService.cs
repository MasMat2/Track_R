using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class AyudaSeccionValidatorService
    {
        private IAyudaSeccionRepository ayudaSeccionRepository;

        public AyudaSeccionValidatorService(IAyudaSeccionRepository ayudaSeccionRepository)
        {
            this.ayudaSeccionRepository = ayudaSeccionRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeClaveRequerido = "La clave es requerida";
        private readonly string MensajeExistencia = "La seccion ayuda clase no existe";

        private readonly string MensajeDuplicado = "La seccion ayuda ya existe";
        private readonly string MensajeDuplicadoNombre = "El nombre de la seccion ayuda ya se encuentra registrado.";
        private readonly string MensajeDuplicadoClave = "La clave de la seccion ayuda ya se encuentra registrada.";
        //private readonly string MensajeDuplicadoOrden = "El nombre de la seccion ayuda ya se encuentra registrado.";

        private static readonly int LongitudNombre = 50;
        private static readonly int LongitudClave = 10;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre de la clase de artículo son {LongitudNombre} caracteres";
        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave} caracteres";


        public void ValidarAgregar(AyudaSeccion ayudaSeccion)
        {
            ValidarRequerido(ayudaSeccion);
            ValidarRango(ayudaSeccion);
            ValidarDuplicado(ayudaSeccion);
        }

        public void ValidarEditar(AyudaSeccion ayudaSeccion)
        {
            ValidarRequerido(ayudaSeccion);
            ValidarRango(ayudaSeccion);
            ValidarExistencia(ayudaSeccion.IdAyudaSeccion);
            ValidarDuplicado(ayudaSeccion);
        }

        public void ValidarEliminar(int idAyudaSeccion)
        {
            AyudaSeccion ayudaSeccion = ayudaSeccionRepository.Consultar(idAyudaSeccion);
            ValidarExistencia(idAyudaSeccion);
        }


        public void ValidarRequerido(AyudaSeccion ayudaSeccion)
        {
            Validator.ValidarRequerido(ayudaSeccion.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(ayudaSeccion.Clave, MensajeClaveRequerido);
        }

        public void ValidarRango(AyudaSeccion ayudaSeccion)
        {
            Validator.ValidarLongitudRangoString(ayudaSeccion.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(ayudaSeccion.Clave, LongitudClave, MensajeClaveLongitud);
        }

        public void ValidarDuplicado(AyudaSeccion ayudaSeccion)
        {
            var ayudaSeccionDuplicado = ayudaSeccionRepository.Consultar(ayudaSeccion.IdAyudaSeccion);

            if (ayudaSeccionDuplicado != null && ayudaSeccion.IdAyudaSeccion != ayudaSeccionDuplicado.IdAyudaSeccion)
            {
                throw new CdisException(MensajeDuplicado);
            }

            var ayudaSeccionDuplicadoNombre = ayudaSeccionRepository.ConsultarPorNombre(ayudaSeccion.Nombre);

            if (ayudaSeccionDuplicadoNombre != null && ayudaSeccion.IdAyudaSeccion != ayudaSeccionDuplicadoNombre.IdAyudaSeccion)
            {
                throw new CdisException(MensajeDuplicadoNombre);
            }

            var ayudaSeccionDuplicadoClave = ayudaSeccionRepository.ConsultarPorClave(ayudaSeccion.Clave);

            if (ayudaSeccionDuplicadoClave != null && ayudaSeccion.IdAyudaSeccion != ayudaSeccionDuplicadoClave.IdAyudaSeccion)
            {
                throw new CdisException(MensajeDuplicadoClave);
            }
        }

        public void ValidarExistencia(int idAyudaSeccion)
        {
            AyudaSeccion ayudaSeccion = ayudaSeccionRepository.Consultar(idAyudaSeccion);

            if (ayudaSeccion == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }
    }
}
