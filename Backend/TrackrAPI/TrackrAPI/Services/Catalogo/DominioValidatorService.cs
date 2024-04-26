using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class DominioValidatorService
    {
        private IDominioRepository dominioRepository;

        public DominioValidatorService(IDominioRepository dominioRepository)
        {
            this.dominioRepository = dominioRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeTipoDatoRequerido = "El tipo dato es requerido";
        private readonly string MensajeTipoCampoRequerido = "El tipo campo es requerido";
        private readonly string MensajeExistencia = "El dominio no existe";
        private readonly string MensajeDuplicado = "El dominio ya existe";

        private readonly string MensajeDominioDetalleDependecia = "El dominio esta asociado al menos a un dominio detalle y no se puede eliminar";
        private readonly string MensajeExpedienteCampoDependecia = "El dominio esta asociado al menos a un expediente detalle y no se puede eliminar";

        private static readonly int LongitudNombre = 50;
        private static readonly int LongitudDescripcion = 500;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son { LongitudNombre } caracteres";
        private readonly string MensajeDescripcionLongitud = $"La longitud máxima de la descripcion son { LongitudDescripcion } caracteres";

        public void ValidarAgregar(Dominio dominio)
        {
            ValidarRequerido(dominio);
            ValidarRango(dominio);
            ValidarDuplicado(dominio);
        }

        public void ValidarEditar(Dominio dominio)
        {
            ValidarRequerido(dominio);
            ValidarRango(dominio);
            ValidarExistencia(dominio.IdDominio);
            ValidarDuplicado(dominio);
        }

        public void ValidarEliminar(int idDominio)
        {
            var dominio = dominioRepository.Consultar(idDominio);

            ValidarExistencia(idDominio);
            ValidarDependencia(idDominio);
        }

        public void ValidarRequerido(Dominio dominio)
        {
            Validator.ValidarRequerido(dominio.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(dominio.TipoDato, MensajeTipoDatoRequerido);
            Validator.ValidarRequerido(dominio.TipoCampo, MensajeTipoCampoRequerido);
        }

        public void ValidarRango(Dominio dominio)
        {
            Validator.ValidarLongitudRangoString(dominio.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(dominio.Descripcion, LongitudDescripcion, MensajeDescripcionLongitud);
        }

        public void ValidarExistencia(DominioDto? dominio)
        {
            if (dominio == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idDominio)
        {
            var dominio = dominioRepository.Consultar(idDominio);

            if (dominio == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Dominio dominio)
        {
            var dominioDuplicado = dominioRepository.Consultar(dominio.Nombre);

            if (dominioDuplicado != null && dominio.IdDominio != dominioDuplicado.IdDominio)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }

        public void ValidarDependencia(int idDominio)
        {
            var dominio = dominioRepository.ConsultarDependencias(idDominio);

            if (dominio == null)
            {
                throw new CdisException(MensajeExistencia);
            }

            if (dominio.DominioDetalle.Any())
            {
                throw new CdisException(MensajeDominioDetalleDependecia);
            }

            //if (dominio.ExpedienteCampo.Any())
            //{
            //    throw new CdisException(MensajeExpedienteCampoDependecia);
            //}
        }
    }
}
