using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Inventario;
using System.Linq;

namespace TrackrAPI.Services.Inventario
{
    public class AlmacenValidatorService
    {
        private IAlmacenRepository almacenRepository;

        public AlmacenValidatorService(IAlmacenRepository almacenRepository)
        {
            this.almacenRepository = almacenRepository;
        }

        private readonly string MensajeNumeroRequerido = "El número es requerido";
        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeDescripcionRequerido = "La descripción es requerida";
        private readonly string MensajeFechaAltaRequerido = "La fecha alta es requerida";
        private readonly string MensajeCalleRequerido = "La calle es requerida";
        private readonly string MensajeNumeroERequerido = "El número exterior es requerido";
        private readonly string MensajeColoniaRequerido = "La colonia es requerida";
        private readonly string MensajeLocalidadRequerido = "La localidad es requerida";
        private readonly string MensajeCodigoPRequerido = "El código postal es requerido";
        private readonly string MensajeTelefonoUnoRequerido = "El teléfono uno es requerido";
        private readonly string MensajeTelefonoDosRequerido = "El teléfono dos es requerido";
        private readonly string MensajeEstatusRequerido = "El estatus es requerido";
        private readonly string MensajeUsuarioRequerido = "El usuario es requerido";
        private readonly string MensajeEstadoRequerido = "El estado es requerido";
        private readonly string MensajeExistencia = "El almacén no existe";
        private readonly string MensajeDuplicado = "El almacén ya existe";

        private static readonly int LongitudMaximaNumE = 6;
        private static readonly int LongitudMaximaNumI = 6;
        private static readonly int LongitudMaximaCodigoPostal = 5;
        private static readonly int LongitudMaximaTelefonoUno = 15;
        private static readonly int LongitudMaximaTelefonoDos = 15;

        private readonly string MensajeLongitudNumE = $"La longitud máxima del número exterior son { LongitudMaximaNumE } caracteres";
        private readonly string MensajeLongitudNumI = $"La longitud máxima del número interior son { LongitudMaximaNumI } caracteres";
        private readonly string MensajeLongitudCodigoPostal = $"La longitud máxima del código postal son { LongitudMaximaCodigoPostal } caracteres";
        private readonly string MensajeLongitudTelefonoUno = $"La longitud máxima del teléfono uno son { LongitudMaximaTelefonoUno } caracteres";
        private readonly string MensajeLongitudTelefonoDos = $"La longitud máxima del teléfono dos son { LongitudMaximaTelefonoDos } caracteres";

        private readonly string MensajeDependenciaInventarioFisico = "El almacén esta asociado al menos a un inventario físico y no se puede eliminar";
        private readonly string MensajeDependenciaKardex = "El almacén esta asociado al menos a un kardex y no se puede eliminar";
        private readonly string MensajeDependenciaMovimientoMaterial = "El almacén esta asociado al menos a un movimiento de material y no se puede eliminar";
        private readonly string MensajeDependenciaOrdenCompra = "El almacén esta asociado al menos a una orden de compra y no se puede eliminar";
        private readonly string MensajeDependenciaPuntoVenta = "El almacén esta asociado al menos a un punto de venta y no se puede eliminar";
        private readonly string MensajeDependenciaTraspasoMovimientoMaterialDestino = "El almacén esta asociado al menos a un destino de traspaso de movimiento de material y no se puede eliminar";
        private readonly string MensajeDependenciaTraspasoMovimientoMaterialOrigen = "El almacén esta asociado al menos a un origen de traspaso de movimiento de material y no se puede eliminar";
        private readonly string MensajeDependenciaUbicacion = "El almacén esta asociado al menos a una ubicación y no se puede eliminar";



        public void ValidarAgregar(Almacen almacen)
        {
            ValidarRequerido(almacen);
            ValidarRango(almacen);
            ValidarDuplicado(almacen);
            ValidarFormato(almacen);
        }

        public void ValidarEditar(Almacen almacen)
        {
            ValidarRequerido(almacen);
            ValidarRango(almacen);
            ValidarDuplicado(almacen);
            ValidarExistencia(almacen.IdAlmacen);
            ValidarFormato(almacen);
        }

        public void ValidarEliminar(int idAlmacen)
        {
            ValidarExistencia(idAlmacen);
            ValidarDependencia(idAlmacen);
        }

        public void ValidarRequerido(Almacen almacen)
        {
            Validator.ValidarRequerido(almacen.Numero, MensajeNumeroRequerido);
            Validator.ValidarRequerido(almacen.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(almacen.Descripcion, MensajeDescripcionRequerido);
            Validator.ValidarRequerido(almacen.FechaAlta, MensajeFechaAltaRequerido);
            Validator.ValidarRequerido(almacen.Calle, MensajeCalleRequerido);
            Validator.ValidarRequerido(almacen.NumeroExterior, MensajeNumeroERequerido);
            Validator.ValidarRequerido(almacen.Colonia, MensajeColoniaRequerido);
            Validator.ValidarRequerido(almacen.Localidad, MensajeLocalidadRequerido);
            Validator.ValidarRequerido(almacen.CodigoPostal, MensajeCodigoPRequerido);
            Validator.ValidarRequerido(almacen.TelefonoUno, MensajeTelefonoUnoRequerido);
            Validator.ValidarRequerido(almacen.TelefonoDos, MensajeTelefonoDosRequerido);
            Validator.ValidarRequerido(almacen.IdEstatusAlmacen, MensajeEstatusRequerido);
            Validator.ValidarRequerido(almacen.IdUsuarioResponsable, MensajeUsuarioRequerido);
            Validator.ValidarRequerido(almacen.IdEstado, MensajeEstadoRequerido);
        }

        public void ValidarRango(Almacen almacen)
        {
            Validator.ValidarLongitudMaximaString(almacen.NumeroExterior, LongitudMaximaNumE, MensajeLongitudNumE);
            Validator.ValidarLongitudMaximaString(almacen.NumeroInterior, LongitudMaximaNumI, MensajeLongitudNumI);
            Validator.ValidarLongitudMaximaString(almacen.CodigoPostal, LongitudMaximaCodigoPostal, MensajeLongitudCodigoPostal);
            Validator.ValidarLongitudMaximaString(almacen.TelefonoUno, LongitudMaximaTelefonoUno, MensajeLongitudTelefonoUno);
            Validator.ValidarLongitudMaximaString(almacen.TelefonoDos, LongitudMaximaTelefonoDos, MensajeLongitudTelefonoDos);
        }

        public void ValidarFormato(Almacen almacen)
        {

        }

        public void ValidarExistencia(int idAlmacen)
        {
            Almacen almacen = almacenRepository.Consultar(idAlmacen);

            if (almacen == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Almacen almacen)
        {
            Almacen almacenDuplicado = almacenRepository.Consultar(almacen.IdAlmacen);
            if (almacenDuplicado != null && almacen.IdAlmacen != almacenDuplicado.IdAlmacen)
            {
                throw new CdisException(MensajeDuplicado);
            }

            Almacen almacenDuplicado2 = almacenRepository.ConsultarPorNumero(almacen.Numero, (int)almacen.IdCompania);
            if (almacenDuplicado2 != null && almacen.IdAlmacen != almacenDuplicado2.IdAlmacen)
            {
                throw new CdisException(MensajeDuplicado);
            }

            Almacen almacenDuplicado3 = almacenRepository.ConsultarPorNombre(almacen.Nombre, (int)almacen.IdCompania);
            if (almacenDuplicado3 != null && almacen.IdAlmacen != almacenDuplicado3.IdAlmacen)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }

        public void ValidarDependencia(int idAlmacen)
        {
            var almacen = almacenRepository.ConsultarConDependencias(idAlmacen);

            if (almacen.InventarioFisico.Any())
            {
                throw new CdisException(MensajeDependenciaInventarioFisico);
            }

            if (almacen.Kardex.Any())
            {
                throw new CdisException(MensajeDependenciaKardex);
            }

            if (almacen.MovimientoMaterial.Any())
            {
                throw new CdisException(MensajeDependenciaMovimientoMaterial);
            }

            if (almacen.OrdenCompra.Any())
            {
                throw new CdisException(MensajeDependenciaOrdenCompra);
            }

            if (almacen.PuntoVenta.Any())
            {
                throw new CdisException(MensajeDependenciaPuntoVenta);
            }

            if (almacen.TraspasoMovimientoMaterialIdAlmacenDestinoNavigation.Any())
            {
                throw new CdisException(MensajeDependenciaTraspasoMovimientoMaterialDestino);
            }

            if (almacen.TraspasoMovimientoMaterialIdAlmacenOrigenNavigation.Any())
            {
                throw new CdisException(MensajeDependenciaTraspasoMovimientoMaterialOrigen);
            }

            if (almacen.Ubicacion.Any())
            {
                throw new CdisException(MensajeDependenciaUbicacion);
            }
        }
    }
}
