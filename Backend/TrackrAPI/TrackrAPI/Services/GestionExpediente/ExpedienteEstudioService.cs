using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.GestionExpediente
{
    public class ExpedienteEstudioService
    {
        private readonly IExpedienteEstudioRepository expedienteEstudioRepository;
        
        public ExpedienteEstudioService(IExpedienteEstudioRepository expedienteEstudioRepository)
        {
            this.expedienteEstudioRepository = expedienteEstudioRepository;
        }

        /// <summary>
        /// Consulta el ExpedienteEstudio de un Usuario para ser desplegado en el Grid
        /// </summary>
        /// <param name="idUsuario">IdUsuario que filtra los Estudios</param>
        /// <returns>Lista de Estudios</returns>
        public IEnumerable<ExpedienteEstudioGridDTO> ConsultarPorUsuario(int idUsuario)
        {
            return expedienteEstudioRepository.ConsultarPorUsuario(idUsuario);
        }

        /// <summary>
        /// Consulta un ExpedienteEstudio por su id
        /// </summary>
        /// <param name="idExpedienteEstudio">IdEstudio a consultar</param>
        /// <returns>Estudio Consultado</returns>
        public ExpedienteEstudio Consultar(int idExpedienteEstudio)
        {
            return expedienteEstudioRepository.Consultar(idExpedienteEstudio);
        }


        public void Agregar(ExpedienteEstudioFormularioCapturaDTO expedienteEstudioDTO, int idUsuario)

        {
            int idExpediente = expedienteEstudioRepository.ConsultarIdExpediente(idUsuario);

            var expedienteEstudio = new ExpedienteEstudio()
            {
                IdExpediente = idExpediente,
                Nombre = expedienteEstudioDTO.Nombre,
                FechaRealizacion = DateTime.Now,
                Archivo = expedienteEstudioDTO.Archivo,
                ArchivoTipoMime = expedienteEstudioDTO.ArchivoTipoMime,
                ArchivoNombre = expedienteEstudioDTO.ArchivoNombre
            };
            expedienteEstudioRepository.Agregar(expedienteEstudio);
        }
        public void Eliminar(int idExpedienteEstudio)
        {
            var expedienteEstudio = expedienteEstudioRepository.Consultar(idExpedienteEstudio);

                expedienteEstudioRepository.Eliminar(expedienteEstudio);
            
        }

        /*public int Agregar()
        {
            ExpedienteEstudio expedienteEstudio = new ExpedienteEstudio();
            byte[] pdfData = File.ReadAllBytes("D:/Users/osval/Downloads/mock.pdf");
            expedienteEstudio.Archivo = pdfData;
            expedienteEstudio.Nombre = "Rayos X Tobillo";
            expedienteEstudio.ArchivoNombre = "Rayos X Tobillo";
            expedienteEstudio.FechaRealizacion = DateTime.UtcNow;
            expedienteEstudio.IdExpediente = 9;
            expedienteEstudio.ArchivoTipoMime = "application/pdf";

            expedienteEstudio = expedienteEstudioRepository.Agregar(expedienteEstudio);
            return expedienteEstudio.IdExpedienteEstudio;
        }*/
    }
}
