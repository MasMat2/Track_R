using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class PerfilValidatorService
    {
        private IPerfilRepository perfilRepository;

        public PerfilValidatorService(IPerfilRepository perfilRepository)
        {
            this.perfilRepository = perfilRepository;
        }

        private static readonly string MensajeNombreRequerido = "El nombre es requerido.";
        private static readonly string MensajeDuplicado = "El perfil ya existe.";

        private static int LongitudNombre = 100;

        private static string MensajeNombreLongitud = "La longitud máxima del nombre son " + LongitudNombre + " caracteres.";

        public void ValidarAgregar(Perfil perfil)
        {
            ValidarRequerido(perfil);
            ValidarRango(perfil);
            ValidarNombreDuplicado(perfil);
        }

        public void ValidarEditar(Perfil perfil)
        {
            ValidarRequerido(perfil);
            ValidarRango(perfil);
            ValidarExistencia(perfil.IdPerfil);
            ValidarNombreDuplicado(perfil);
        }

        public void ValidarEliminar(int idPerfil)
        {
            Perfil perfil = perfilRepository.Consultar(idPerfil);
            ValidarExistencia(idPerfil);
            ValidarDependencia(perfil);
        }

        public void ValidarRequerido(Perfil perfil)
        {
            Validator.ValidarRequerido(perfil.Nombre, MensajeNombreRequerido);
        }

        public void ValidarRango(Perfil perfil)
        {
            Validator.ValidarLongitudMaximaString(perfil.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        public void ValidarExistencia(int idPerfil)
        {
            Perfil perfil = perfilRepository.Consultar(idPerfil);
            if (perfil == null)
            {
                throw new CdisException("");
            }
        }

        public void ValidarDependencia(Perfil perfil)
        {
            Perfil perfilConsultado = perfilRepository.ConsultarDependencia(perfil.IdPerfil);

            if (perfilConsultado.Usuario.Count > 0)
            {
                throw new CdisException("El perfil tiene asociado al menos un usuario y no se puede eliminar");
            }

        }

        public void ValidarNombreDuplicado(Perfil perfil)
        {
        }

    }
}
