using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class MonedaValidatorService
    {
        private IMonedaRepository monedaRepository;

        public MonedaValidatorService(IMonedaRepository monedaRepository)
        {
            this.monedaRepository = monedaRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeClaveRequerida = "La clave es requerida";
        private readonly string MensajeSimboloRequerido = "El símbolo es requerido";
        private readonly string MensajeExistencia = "La moneda no existe";
        private readonly string MensajeDuplicadoNombre = "El nombre de la moneda ya existe";
        private readonly string MensajeDuplicadoClave = "La clave de la moneda ya existe";
        private readonly string MensajeMoneda = "La moneda no puede pertenecer a sí misma";
        private static readonly int LongitudNombre = 50;

        private static readonly int LongitudClave = 20;
        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave } caracteres";

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";
        private readonly string MensajeDependencia2 = "La moneda esta asociado al menos a un artículo y no se puede eliminar";
        public void ValidarAgregar(Moneda moneda)
        {
            ValidarRequerido(moneda);
            ValidarRango(moneda);
            ValidarDuplicado(moneda);
        }

        public void ValidarEditar(Moneda moneda)
        {
            ValidarRequerido(moneda);
            ValidarRango(moneda);
            ValidarExistencia(moneda.IdMoneda);
            ValidarDuplicado(moneda);
        }

        public void ValidarEliminar(int idMoneda)
        {
            ValidarExistencia(idMoneda);
        }

        public void ValidarRequerido(Moneda moneda)
        {
            Validator.ValidarRequerido(moneda.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(moneda.Clave, MensajeClaveRequerida);
            Validator.ValidarRequerido(moneda.Simbolo, MensajeSimboloRequerido);
        }

        public void ValidarRango(Moneda moneda)
        {
            Validator.ValidarLongitudRangoString(moneda.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(moneda.Clave, LongitudClave, MensajeClaveLongitud);
        }

        public void ValidarExistencia(MonedaDto moneda)
        {
            if (moneda == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idMoneda)
        {
            var moneda = monedaRepository.ConsultarPorId(idMoneda);

            if (moneda == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Moneda moneda)
        {
            Moneda monedaDuplicadoPorNombre = monedaRepository.ConsultarPorNombre(moneda.Nombre);
            Moneda monedaDuplicadoPorClave = monedaRepository.ConsultarPorClave(moneda.Clave);
            if (monedaDuplicadoPorNombre != null && moneda.IdMoneda != monedaDuplicadoPorNombre.IdMoneda)
            {
                throw new CdisException(MensajeDuplicadoNombre);
            }

            if (monedaDuplicadoPorClave != null && moneda.IdMoneda != monedaDuplicadoPorClave.IdMoneda)
            {
                throw new CdisException(MensajeDuplicadoClave);
            }
        }
    }
}
