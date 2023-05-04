using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class CompaniaContactoValidatorService
    {

        private readonly string MensajeRequeridoCompania = "La compañía es requerida";
        private readonly string MensajeRequeridoNombre = "El nombre es requerido";
        private readonly string MensajeRequeridoApellidoPaterno = "El apellido paterno es requerido";
        private readonly string MensajeRequeridoApellidoMaterno = "El apellido materno es requerido";
        private readonly string MensajeRequeridoTelefono = "El teléfono es requerido";
        private readonly string MensajeRequeridoCorreo = "El correo es requerido";

        private readonly string MensajeFormatoNombre = "El nombre es de formato alfanumérico";
        private readonly string MensajeFormatoApellidoPaterno = "El apellido paterno es de formato alfanumérico";
        private readonly string MensajeFormatoApellidoMaterno = "El apellido materno es de formato alfanumérico";
        private readonly string MensajeFormatoCorreo = "El correo no tiene el formato correcto";
        private readonly string MensajeFormatoTelefono = "El teléfono no tiene el formato correcto";

        private const int LongitudMaximaNombre = 200;
        private const int LongitudMaximaApellidoPaterno = 200;
        private const int LongitudMaximaApellidoMaterno = 200;
        private const int LongitudMaximaCorreo = 50;

        private readonly string MensajeLongitudNombre = $"La longitud máxima del nombre son {LongitudMaximaNombre} caracteres";
        private readonly string MensajeLongitudApellidoPaterno = $"La longitud máxima del apellido paterno son {LongitudMaximaApellidoPaterno} caracteres";
        private readonly string MensajeLongitudApellidoMaterno = $"La longitud máxima del apellido materno son {LongitudMaximaApellidoMaterno} caracteres";
        private readonly string MensajeLongitudCorreo = $"La longitud máxima del correo son {LongitudMaximaCorreo} caracteres";

        private readonly string MensajeExistencia = "El contacto de la compañía no existe";

        private readonly ICompaniaContactoRepository companiaContactoRepository;

        public CompaniaContactoValidatorService(ICompaniaContactoRepository companiaContactoRepository)
        {
            this.companiaContactoRepository = companiaContactoRepository;
        }

        public void ValidarAgregar(CompaniaContacto companiaContacto)
        {
            ValidarRequerido(companiaContacto);
            ValidarFormato(companiaContacto);
        }

        public void ValidarEditar(CompaniaContacto companiaContacto)
        {
            ValidarExistencia(companiaContacto.IdCompaniaContacto);
            ValidarRequerido(companiaContacto);
            ValidarFormato(companiaContacto);
        }

        public void ValidarEliminar(int idCompaniaContacto)
        {
            ValidarExistencia(idCompaniaContacto);
            ValidarDependencias(idCompaniaContacto);
        }

        private void ValidarRequerido(CompaniaContacto companiaContacto)
        {
            Validator.ValidarRequerido(companiaContacto.IdCompania, MensajeRequeridoCompania);
            Validator.ValidarRequerido(companiaContacto.Nombre, MensajeRequeridoNombre);
            Validator.ValidarRequerido(companiaContacto.ApellidoPaterno, MensajeRequeridoApellidoPaterno);
            Validator.ValidarRequerido(companiaContacto.ApellidoMaterno, MensajeRequeridoApellidoMaterno);
            Validator.ValidarRequerido(companiaContacto.TelefonoMovil, MensajeRequeridoTelefono);
            Validator.ValidarRequerido(companiaContacto.Correo, MensajeRequeridoCorreo);
        }

        private void ValidarFormato(CompaniaContacto companiaContacto)
        {
            Validator.ValidarAlfanumerico(companiaContacto.Nombre, MensajeFormatoNombre);
            Validator.ValidarAlfanumerico(companiaContacto.ApellidoPaterno, MensajeFormatoApellidoPaterno);
            Validator.ValidarAlfanumerico(companiaContacto.ApellidoMaterno, MensajeFormatoApellidoMaterno);
            Validator.ValidarCorreo(companiaContacto.Correo, MensajeFormatoCorreo);
            Validator.ValidarTelefono(companiaContacto.TelefonoMovil, MensajeFormatoTelefono);

            Validator.ValidarLongitudMaximaString(companiaContacto.Nombre, LongitudMaximaNombre, MensajeLongitudNombre);
            Validator.ValidarLongitudMaximaString(companiaContacto.ApellidoPaterno, LongitudMaximaApellidoPaterno, MensajeLongitudApellidoPaterno);
            Validator.ValidarLongitudMaximaString(companiaContacto.ApellidoMaterno, LongitudMaximaApellidoMaterno, MensajeLongitudApellidoMaterno);
            Validator.ValidarLongitudMaximaString(companiaContacto.Correo, LongitudMaximaCorreo, MensajeLongitudCorreo);
        }

        private void ValidarExistencia(int idCompaniaContacto)
        {
            CompaniaContacto companiaContacto = companiaContactoRepository.Consultar(idCompaniaContacto);

            if (companiaContacto == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        private void ValidarDependencias(int idCompaniaContacto)
        {
        }
    }
}