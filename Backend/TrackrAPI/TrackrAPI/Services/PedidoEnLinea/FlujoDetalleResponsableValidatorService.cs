using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Repositorys.PedidoEnLinea;

namespace TrackrAPI.Services.PedidoEnLinea
{
    public class FlujoDetalleResponsableValidatorService
    {
        private IFlujoDetalleResponsableRepository flujoDetalleResponsableRepository;
        private IFlujoDetalleRepository flujoDetalleRepository;
        private IUsuarioRepository usuarioRepository;
        public FlujoDetalleResponsableValidatorService(
            IFlujoDetalleResponsableRepository flujoDetalleResponsableRepository,
            IFlujoDetalleRepository flujoDetalleRepository,
            IUsuarioRepository usuarioRepository)
        {
            this.flujoDetalleResponsableRepository = flujoDetalleResponsableRepository;
            this.flujoDetalleRepository = flujoDetalleRepository;
            this.usuarioRepository = usuarioRepository;
        }

        private readonly string MensajeFlujoDetalleRequerido = "El flujo detalle es requerido";
        private readonly string MensajeUsuarioRequerido = "El usuario es requerido";
        private readonly string MensajeExistencia = "El flujo detalle responsable no existe";

        public void ValidarAgregar(FlujoDetalleResponsable flujoDetalleResponsable)
        {
            ValidarRequerido(flujoDetalleResponsable);
            ValidarDuplicado(flujoDetalleResponsable);
        }

        public void ValidarEditar(FlujoDetalleResponsable flujoDetalleResponsable)
        {
            ValidarExistencia(flujoDetalleResponsable.IdFlujoDetalleResponsable);
            ValidarRequerido(flujoDetalleResponsable);
            ValidarDuplicado(flujoDetalleResponsable);
        }

        public void ValidarEliminar(int idFlujoDetalleResponsable)
        {
            ValidarExistencia(idFlujoDetalleResponsable);
        }

        public void ValidarRequerido(FlujoDetalleResponsable flujoDetalleResponsable)
        {
            Validator.ValidarRequerido(flujoDetalleResponsable.IdFlujoDetalle, MensajeFlujoDetalleRequerido);
            Validator.ValidarRequerido(flujoDetalleResponsable.IdUsuario, MensajeUsuarioRequerido);
        }

        public void ValidarDuplicado(FlujoDetalleResponsable flujoDetalleResponsable)
        {
            List<FlujoDetalleResponsable> responsablesActuales = flujoDetalleResponsableRepository.ConsultarPorFlujoDetalle(flujoDetalleResponsable.IdFlujoDetalle);

            if (responsablesActuales.Any(r => r.IdUsuario == flujoDetalleResponsable.IdUsuario && r.IdFlujoDetalleResponsable != flujoDetalleResponsable.IdFlujoDetalleResponsable))
            {
                Usuario responsable = usuarioRepository.Consultar(flujoDetalleResponsable.IdUsuario);

                throw new CdisException($"El flujo detalle ya cuenta con el responsable { responsable.ObtenerNombreCompleto() }");
            }
        }

        public void ValidarExistencia(int idFlujoDetalleResponsable)
        {
            FlujoDetalleResponsable flujoDetalleResponsable = flujoDetalleResponsableRepository.Consultar(idFlujoDetalleResponsable);

            if (flujoDetalleResponsable == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }
    }
}
