using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Inventario;
using TrackrAPI.Repositorys.Seguridad;
using System.Linq;

namespace TrackrAPI.Services.Inventario
{
    public class DomicilioValidatorService
    {
        private ICompaniaRepository companiaRepository;
        private ITipoCompaniaRepository tipoCompaniaRepository;
        private IDomicilioRepository domicilioRepository;
        private IUsuarioRepository usuarioRepository;
        private IRolRepository rolRepository;
        private IColoniaRepository coloniaRepository;
        private ICodigoPostalRepository codigoPostalRepository;

        public DomicilioValidatorService(
            ICompaniaRepository companiaRepository,
            ITipoCompaniaRepository tipoCompaniaRepository,
            IDomicilioRepository domicilioRepository,
            IUsuarioRepository usuarioRepository,
            IRolRepository rolRepository,
            IColoniaRepository coloniaRepository,
            ICodigoPostalRepository codigoPostalRepository
        )
        {
            this.companiaRepository = companiaRepository;
            this.tipoCompaniaRepository = tipoCompaniaRepository;
            this.domicilioRepository = domicilioRepository;
            this.usuarioRepository = usuarioRepository;
            this.rolRepository = rolRepository;
            this.coloniaRepository = coloniaRepository;
            this.codigoPostalRepository = codigoPostalRepository;
        }

        private readonly string MensajeCalleRequerido = "La calle es requerida";
        private readonly string MensajeNumeroERequerido = "El número exterior es requerido";
        private readonly string MensajeCodigoPRequerido = "El código postal es requerido";
        private readonly string MensajePaisRequerido = "El estado es requerido";
        private readonly string MensajeEstadoRequerido = "El estado es requerido";
        private readonly string MensajeMunicipioRequerido = "El estado es requerido";
        private readonly string MensajeColoniaRequerido = "La colonia es requerida";
        private readonly string MensajeNombreSucursalRequerido = "El nombre de la sucrusal es requerido";
        private readonly string MensajeMetodoPagoRequerido = "El método de pago es requerido";
        private readonly string MensajeRepartidorRequerido = "El repartidor es requerido";
        private readonly string MensajeUsuarioRequerido = "El usuario es requerido";

        private readonly string MensajeExistencia = "El domicilio no existe";
        private readonly string MensajeDuplicado = "El domicilio ya existe";
        private readonly string MensajeSucursalDuplicado = "La sucursal ya existe";

        private static readonly int LongitudMaximaNumE = 6;
        private static readonly int LongitudMaximaNumI = 6;
        private static readonly int LongitudMaximaCodigoPostal = 5;
        private static readonly int LongitudMaximaCalle = 100;
        private static readonly int LongitudMaximaNombreSucursal = 100;
        private static readonly int LongitudMaximaEntreCalles = 150;
        private static readonly int LongitudMaximaOtraReferencia = 150;

        private readonly string MensajeLongitudNumE = $"La longitud máxima del número exterior son { LongitudMaximaNumE } caracteres";
        private readonly string MensajeLongitudNumI = $"La longitud máxima del número interior son { LongitudMaximaNumI } caracteres";
        private readonly string MensajeLongitudCodigoP = $"La longitud máxima del código postal son { LongitudMaximaCodigoPostal } caracteres";
        private readonly string MensajeLongitudCalle = $"La longitud máxima de la calle son { LongitudMaximaCalle } caracteres";
        private readonly string MensajeLongitudNombreSucursal = $"La longitud máxima del nombre de la sucursal son { LongitudMaximaNombreSucursal } caracteres";
        private readonly string MensajeLongitudEntreCalles = $"La longitud máxima para las entre calles son { LongitudMaximaNombreSucursal } caracteres";
        private readonly string MensajeLongitudOtraReferencia = $"La longitud máxima para otras referencias son { LongitudMaximaNombreSucursal } caracteres";

        private readonly string MensajeDependenciaOrdenCompra = "El domicilio está asociado al menos a una orden de compra y no se puede eliminar";
        private readonly string MensajeDependenciaPedido = "El domicilio está asociado al menos a un pedido y no se puede eliminar";
        private readonly string MensajeDependenciaExpedienteMercancia = "El domicilio está asociado al menos a un expediente administrativo de mercancía y no se puede eliminar";
        private readonly string MensajeDependenciaExpedienteViaje = "El domicilio está asociado al menos a un expediente administrativo de viaje y no se puede eliminar";

        private readonly string MensajeCalleFormato = "La calle es de formato alfanumérico";
        private readonly string MensajeNumEFormato = "El número exterior es de formato alfanumérico";
        private readonly string MensajeNumIFormato = "El número interior es de formato alfanumérico";
        private readonly string MensajeCodigoPFormato = "El código postal es de formato alfanumérico";


        public void ValidarAgregar(Domicilio domicilio, bool validarSucursal)
        {
            ValidarRequerido(domicilio, validarSucursal);
            ValidarRango(domicilio);
            ValidarDuplicado(domicilio);
            ValidarFormato(domicilio);
        }

        public void ValidarEditar(Domicilio domicilio, bool validarSucursal)
        {
            ValidarRequerido(domicilio, validarSucursal);
            ValidarRango(domicilio);
            ValidarDuplicado(domicilio);
            ValidarExistencia(domicilio.IdDomicilio);
            ValidarFormato(domicilio);
        }

        public void ValidarEliminar(int idDomicilio)
        {
            ValidarExistencia(idDomicilio);
            //ValidarDependencia(idDomicilio);
        }

        public void ValidarRequerido(Domicilio domicilio, bool validarSucursal)
        {
            Validator.ValidarRequerido(domicilio.IdEstado, MensajeEstadoRequerido);
            Validator.ValidarRequerido(domicilio.IdMunicipio, MensajeMunicipioRequerido);
            Validator.ValidarRequerido(domicilio.CodigoPostal, MensajeCodigoPRequerido);
            Validator.ValidarRequerido(domicilio.IdColonia, MensajeColoniaRequerido);
            Validator.ValidarRequerido(domicilio.Calle, MensajeCalleRequerido);
            Validator.ValidarRequerido(domicilio.NumeroExterior, MensajeNumeroERequerido);
            Validator.ValidarRequerido(domicilio.IdUsuario, MensajeUsuarioRequerido);

            // Validación de los campos de sucursal
            if (validarSucursal)
            {
                Usuario usuario = usuarioRepository.Consultar((int)domicilio.IdUsuario);
                bool esCliente = usuario.UsuarioRol.Any(ur =>
                {
                    Rol rol = rolRepository.Consultar(ur.IdRol);
                    return rol.Clave == GeneralConstant.ClaveRolCliente;
                });

                Compania compania = companiaRepository.Consultar((int)domicilio.IdCompania);
                TipoCompania tipoCompaniaDistribucion = tipoCompaniaRepository.ConsultarPorClave(GeneralConstant.ClaveTipoCompaniaDistribucion);

                bool esCompaniaDistribucion = compania.IdTipoCompania == tipoCompaniaDistribucion.IdTipoCompania;

                if (esCliente && esCompaniaDistribucion)
                {
                    Validator.ValidarRequerido(domicilio.NombreSucursal, MensajeNombreSucursalRequerido);
                    Validator.ValidarRequerido(domicilio.IdMetodoPago, MensajeMetodoPagoRequerido);
                    Validator.ValidarRequerido(domicilio.IdUsuarioRepartidor, MensajeRepartidorRequerido);
                }
            }

            // Valida que la colonia pertenezca al municipio seleccionado
            Colonia colonia = coloniaRepository.Consultar((int)domicilio.IdColonia);
            CodigoPostalDto codigoPostal = codigoPostalRepository.ConsultarPorCodigoPostal(domicilio.CodigoPostal).FirstOrDefault();
            if (codigoPostal.IdMunicipio != domicilio.IdMunicipio)
            {
                throw new CdisException("El código postal no corresponde al municipio seleccionado");
            }

            if (colonia.IdCodigoPostalNavigation.CodigoPostal1 != domicilio.CodigoPostal)
            {
                throw new CdisException("La colonia seleccionada no corresponde al código postal ingresado");
            }
        }

        public void ValidarRango(Domicilio domicilio)
        {
            Validator.ValidarLongitudMaximaString(domicilio.Calle, LongitudMaximaCalle, MensajeLongitudCalle);
            Validator.ValidarLongitudMaximaString(domicilio.NumeroExterior, LongitudMaximaNumE, MensajeLongitudNumE);
            Validator.ValidarLongitudMaximaString(domicilio.NumeroInterior, LongitudMaximaNumI, MensajeLongitudNumI);
            Validator.ValidarLongitudMaximaString(domicilio.CodigoPostal, LongitudMaximaCodigoPostal, MensajeLongitudCodigoP);
            Validator.ValidarLongitudMaximaString(domicilio.NombreSucursal, LongitudMaximaNombreSucursal, MensajeLongitudNombreSucursal);
            Validator.ValidarLongitudMaximaString(domicilio.EntreCalles, LongitudMaximaEntreCalles, MensajeLongitudEntreCalles);
            Validator.ValidarLongitudMaximaString(domicilio.OtraReferencia, LongitudMaximaOtraReferencia, MensajeLongitudOtraReferencia);
        }

        public void ValidarFormato(Domicilio domicilio)
        {
            Validator.ValidarAlfanumerico(domicilio.Calle, MensajeCalleFormato);
            Validator.ValidarAlfanumerico(domicilio.NumeroInterior, MensajeNumIFormato);
            Validator.ValidarAlfanumerico(domicilio.NumeroExterior, MensajeNumEFormato);
            Validator.ValidarAlfanumerico(domicilio.CodigoPostal, MensajeCodigoPFormato);
        }

        public void ValidarExistencia(int idDomicilio)
        {
            Domicilio domicilio = domicilioRepository.Consultar(idDomicilio);

            if (domicilio == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Domicilio domicilio)
        {
            Domicilio domicilioDuplicado = domicilioRepository.ConsultarPorDomicilio(domicilio);
            if (domicilioDuplicado != null && domicilio.IdDomicilio != domicilioDuplicado.IdDomicilio)
            {
                throw new CdisException(MensajeDuplicado);
            }

            if (!string.IsNullOrEmpty(domicilio.NombreSucursal))
            {
                var sucursales = domicilioRepository.ConsultarTodos((int)domicilio.IdCompania)
                    .Where(d => d.IdDomicilio != domicilio.IdDomicilio
                        && !string.IsNullOrEmpty(d.NombreSucursal)
                        && domicilio.NombreSucursal.ToLower().Equals(d.NombreSucursal.ToLower())
                    );

                if (sucursales.Any())
                {
                    throw new CdisException(MensajeSucursalDuplicado);
                }
            }
        }

        //public void ValidarDependencia(int idDomicilio)
        //{
        //    var domicilio = domicilioRepository.ConsultarDependencias(idDomicilio);

        //    if (domicilio.OrdenCompra.Any())
        //    {
        //        throw new CdisException(MensajeDependenciaOrdenCompra);
        //    }

        //    if (domicilio.Pedido.Any())
        //    {
        //        throw new CdisException(MensajeDependenciaPedido);
        //    }

        //    if (domicilio.ExpedienteAdministrativoMercancia.Any())
        //    {
        //        throw new CdisException(MensajeDependenciaExpedienteMercancia);
        //    }

        //    if (domicilio.ExpedienteAdministrativoViaje.Any())
        //    {
        //        throw new CdisException(MensajeDependenciaExpedienteViaje);
        //    }
        //}
    }
}
