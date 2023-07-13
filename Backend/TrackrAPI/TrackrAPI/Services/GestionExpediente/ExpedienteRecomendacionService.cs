using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.GestionExpediente
{
    public class ExpedienteRecomendacionService
    {
        private readonly IExpedienteRecomendacionRepository expedienteRecomendacionRepository;

        public ExpedienteRecomendacionService(IExpedienteRecomendacionRepository expedienteRecomendacionRepository)
        {
            this.expedienteRecomendacionRepository = expedienteRecomendacionRepository;
        }

        public IEnumerable<ExpedienteRecomendacionGridDTO> Consultar(int idUsuario)
        {
            return expedienteRecomendacionRepository.Consultar(idUsuario);
        }

        public void Agregar(ExpedienteRecomendacionDTO expedienteRecomendacionDTO)
        {
            var recomendacion = new ExpedienteRecomendaciones
            {
                Descripcion = expedienteRecomendacionDTO.Recomendacion,
                FechaRealizacion = expedienteRecomendacionDTO.Fecha,
                IdExpediente = expedienteRecomendacionDTO.ExpedienteId,
                IdUsuarioDoctor = expedienteRecomendacionDTO.DoctorId
            };

            expedienteRecomendacionRepository.Agregar(recomendacion);
        }

        public void Editar(int idExpedienteRecomendacion, ExpedienteRecomendacionDTO expedienteRecomendacionDTO)
        {
            var recomendacion = expedienteRecomendacionRepository.ConsultarPorId(idExpedienteRecomendacion);
            recomendacion.Descripcion = expedienteRecomendacionDTO.Recomendacion;
            Console.WriteLine(recomendacion);
            expedienteRecomendacionRepository.Editar(recomendacion);
        }

        public void Eliminar(int idExpedienteRecomendacion)
        {
            var recomendacion = expedienteRecomendacionRepository.ConsultarPorId(idExpedienteRecomendacion);
            expedienteRecomendacionRepository.Eliminar(recomendacion);
        }
    }
}