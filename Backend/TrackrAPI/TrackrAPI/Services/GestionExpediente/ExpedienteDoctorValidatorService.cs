using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.GestionExpediente
{
    public class ExpedienteDoctorValidatorService
    {
        private readonly IExpedienteDoctorRepository expedienteDoctorRepository;

        public ExpedienteDoctorValidatorService(IExpedienteDoctorRepository expedienteDoctorRepository)
        {
            this.expedienteDoctorRepository = expedienteDoctorRepository;
        }

        private readonly string MensajeExistencia = "El doctor no existe";
        private readonly string MensajeDoctorDuplicado = "El doctor ya está registrado";

        public void ValidarAgregar(int idExpediente, int idUsuarioDoctor, int idCompania)
        {
            if (idExpediente == null)
            {
                throw new CdisException(MensajeExistencia);
            }
            ValidarExistencia(idUsuarioDoctor);
            ValidarDuplicado(idExpediente, idUsuarioDoctor);

        }


        public void ValidarEliminar(int idUsuarioDoctor)
        {
            ValidarExistencia(idUsuarioDoctor);
        }

        public void ValidarExistencia(int idUsuarioDoctor)
        {
            var doctor = this.expedienteDoctorRepository.ConsultarDoctores().FirstOrDefault(dc => dc.IdUsuarioDoctor == idUsuarioDoctor);

            if (doctor == null)
            {
                throw new CdisException(MensajeExistencia);
            }

        }

        public void ValidarDuplicado(int idExpediente, int idUsuarioDoctor)
        {
            var doctores = this.expedienteDoctorRepository.ConsultarExpediente(idExpediente);

            if (doctores.Any(dc => dc.IdUsuarioDoctor == idUsuarioDoctor))
            {
                throw new CdisException(MensajeDoctorDuplicado);
            }

        }
    }
}
