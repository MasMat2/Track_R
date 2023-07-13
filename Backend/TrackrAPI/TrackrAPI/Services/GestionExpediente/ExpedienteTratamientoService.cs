using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.GestionExpediente
{
    public class ExpedienteTratamientoService
    {
        private readonly IExpedienteTratamientoRepository expedienteTratamientoRepository;
        
        public ExpedienteTratamientoService(IExpedienteTratamientoRepository expedienteTratamientoRepository)
        {
            this.expedienteTratamientoRepository = expedienteTratamientoRepository;
        }

        /// <summary>
        /// Consulta el ExpedienteTratamiento de un Usuario para ser desplegado en el Grid
        /// </summary>
        /// <param name="idUsuario">IdUsuario que filtra los Tratamientos</param>
        /// <returns>Lista de Tratamientos</returns>
        public IEnumerable<ExpedienteTratamientoGridDTO> ConsultarPorUsuario(int idUsuario)
        {
            return expedienteTratamientoRepository.ConsultarPorUsuario(idUsuario);
        }


        /// <summary>
        /// Consulta un ExpedienteTratamiento por su id
        /// </summary>
        /// <param name="idExpedienteTratamiento">IdTratamiento a consultar</param>
        /// <returns>Tratamiento Consultado</returns>
        public ExpedienteTratamiento Consultar(int idExpedienteTratamiento)
        {
            return expedienteTratamientoRepository.Consultar(idExpedienteTratamiento);
        }

    }
}
