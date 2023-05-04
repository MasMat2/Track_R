using TrackrAPI.Dtos;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Inventario;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class PuntoVentaValidatorService
    {
        private IPuntoVentaRepository puntoVentaRepository;
        private IAlmacenRepository almacenRepository;

        public PuntoVentaValidatorService(IPuntoVentaRepository puntoVentaRepository,
            IAlmacenRepository almacenRepository)
        {
            this.puntoVentaRepository = puntoVentaRepository;
            this.almacenRepository = almacenRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeClaveRequerida = "El clave es requerida";
        private readonly string MensajeExistencia = "El punto de venta no existe";

        private readonly string MensajeDuplicadoNombre = "El nombre del punto de venta ya existe";
        private readonly string MensajeDuplicadoClave = "La clave del punto de venta ya existe";

        private static readonly int LongitudNombre = 50;
        private static readonly int LongitudClave = 20;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";
        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave } caracteres";

        private readonly string MensajeNotaVentaDependencia = "El punto de venta esta asociado al menos a una nota de venta y no se puede eliminar";
        private readonly string MensajeUbicacionVentaDependencia = "El punto de venta esta asociado al menos a una ubicación de venta y no se puede eliminar";
        private readonly string MensajeUsuarioDependencia = "El punto de venta esta asociado al menos a un usuario y no se puede eliminar";


        public void ValidarAgregar(PuntoVenta puntoVenta)
        {
            ValidarRequerido(puntoVenta);
            ValidarRango(puntoVenta);
            ValidarDuplicado(puntoVenta);
        }

        public void ValidarEditar(PuntoVenta puntoVenta)
        {
            ValidarRequerido(puntoVenta);
            ValidarRango(puntoVenta);
            ValidarExistencia(puntoVenta.IdPuntoVenta);
            ValidarDuplicado(puntoVenta);
        }

        public void ValidarEliminar(int idPuntoVenta)
        {
            var puntoVenta = puntoVentaRepository.Consultar(idPuntoVenta);

            ValidarExistencia(idPuntoVenta);
            ValidarDependencia(idPuntoVenta);
        }


        public void ValidarRequerido(PuntoVenta puntoVenta)
        {
            Validator.ValidarRequerido(puntoVenta.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(puntoVenta.Clave, MensajeClaveRequerida);

        }

        public void ValidarRango(PuntoVenta puntoVenta)
        {
            Validator.ValidarLongitudRangoString(puntoVenta.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(puntoVenta.Clave, LongitudClave, MensajeClaveLongitud);
        }

        public void ValidarExistencia(PuntoVentaDto puntoVenta)
        {
            if (puntoVenta == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idPuntoVenta)
        {
            var puntoVenta = puntoVentaRepository.Consultar(idPuntoVenta);

            if (puntoVenta == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(PuntoVenta puntoVenta)
        {
            int idCompania = almacenRepository.Consultar(puntoVenta.IdAlmacen).IdCompania;

            PuntoVenta puntoVentaDuplicadoClave = puntoVentaRepository.ConsultarPorClave(puntoVenta.Clave, idCompania);
            PuntoVenta puntoVentaDuplicadoNombre = puntoVentaRepository.ConsultarPorNombre(puntoVenta.Nombre, idCompania);

            if (puntoVentaDuplicadoClave != null && puntoVenta.IdPuntoVenta != puntoVentaDuplicadoClave.IdPuntoVenta)
            {
                throw new CdisException(MensajeDuplicadoClave);
            }

            if (puntoVentaDuplicadoNombre != null && puntoVenta.IdPuntoVenta != puntoVentaDuplicadoNombre.IdPuntoVenta)
            {
                throw new CdisException(MensajeDuplicadoNombre);
            }
        }

        public void ValidarDependencia(int idPuntoVenta)
        {
            PuntoVenta puntoVenta = puntoVentaRepository.ConsultarDependencias(idPuntoVenta);

            if (puntoVenta.NotaVenta.Any())
            {
                throw new CdisException(MensajeNotaVentaDependencia);
            }

            if (puntoVenta.UbicacionVenta.Any())
            {
                throw new CdisException(MensajeUbicacionVentaDependencia);
            }

            if (puntoVenta.Usuario.Any())
            {
                throw new CdisException(MensajeUsuarioDependencia);
            }
        }
    }
}
