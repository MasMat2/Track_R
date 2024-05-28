using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Services.Sftp;

namespace TrackrAPI.Services.GestionExpediente

{
    public class ExpedienteEstudioService
    {
        private readonly IExpedienteEstudioRepository _expedienteEstudioRepository;
        private readonly SftpService _sftpService;

        public ExpedienteEstudioService(IExpedienteEstudioRepository expedienteEstudioRepository,
                                        SftpService sftpService)
        {
            _expedienteEstudioRepository = expedienteEstudioRepository;
            _sftpService = sftpService;
        }

        /// <summary>
        /// Consulta el ExpedienteEstudio de un Usuario para ser desplegado en el Grid
        /// </summary>
        /// <param name="idUsuario">IdUsuario que filtra los Estudios</param>
        /// <returns>Lista de Estudios</returns>
        public IEnumerable<ExpedienteEstudioGridDTO> ConsultarPorUsuario(int idUsuario)
        {
            return _expedienteEstudioRepository.ConsultarPorUsuario(idUsuario);
        }

        /// <summary>
        /// Consulta un ExpedienteEstudio por su id
        /// </summary>
        /// <param name="idExpedienteEstudio">IdEstudio a consultar</param>
        /// <returns>Estudio Consultado</returns>
        public ExpedienteEstudio Consultar(int idExpedienteEstudio)
        {
            var expediente = _expedienteEstudioRepository.Consultar(idExpedienteEstudio);

            expediente.Archivo = new Byte[0];

            if(expediente.ArchivoUrl != null   || expediente.ArchivoNombre != "")
            {
                expediente.Archivo = Convert.FromBase64String(_sftpService.DownloadFile(expediente.ArchivoUrl));
            }

            return expediente;
            
        }

        public void Agregar(ExpedienteEstudioFormularioCapturaDTO expedienteEstudioDTO, int idUsuario)
        {
            int idExpediente = _expedienteEstudioRepository.ConsultarIdExpediente(idUsuario);

            var path = this.GuardarArchivo(expedienteEstudioDTO.Archivo, expedienteEstudioDTO.ArchivoNombre, expedienteEstudioDTO.ArchivoTipoMime);

            var expedienteEstudio = new ExpedienteEstudio()
            {
                IdExpediente = idExpediente,
                Nombre = expedienteEstudioDTO.Nombre,
                FechaRealizacion = DateTime.Now,
                ArchivoTipoMime = expedienteEstudioDTO.ArchivoTipoMime,
                ArchivoNombre = expedienteEstudioDTO.ArchivoNombre,
                ArchivoUrl = path
            };
            _expedienteEstudioRepository.Agregar(expedienteEstudio);
        }

        public void Eliminar(int idExpedienteEstudio)
        {
            var expedienteEstudio = _expedienteEstudioRepository.Consultar(idExpedienteEstudio);
            _expedienteEstudioRepository.Eliminar(expedienteEstudio);
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

        public string GuardarArchivo(byte[] archivo, string nombre, string tipoMime)
        {


            string nombreArchivo = $"{nombre}";
            string path = Path.Combine("Archivos", "Expediente", nombreArchivo);
            var archivoBase64 = Convert.ToBase64String(archivo);

            this._sftpService.UploadFile(path, archivoBase64);


            return path;

        }
    }
}
