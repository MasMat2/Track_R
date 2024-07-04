using System.Transactions;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Archivos;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Services.Sftp;

namespace TrackrAPI.Services.GestionExpediente

{
    public class ExpedienteEstudioService
    {
        private readonly IExpedienteEstudioRepository _expedienteEstudioRepository;
        private readonly SftpService _sftpService;
        private readonly IArchivoRepository _archivoRepository;

        public ExpedienteEstudioService(
            IExpedienteEstudioRepository expedienteEstudioRepository,
            SftpService sftpService,
            IArchivoRepository archivoRepository
        ){
            _expedienteEstudioRepository = expedienteEstudioRepository;
            _sftpService = sftpService;
            _archivoRepository = archivoRepository;
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
                expediente.Archivo = Convert.FromBase64String(_sftpService.DownloadFileAsBase64(expediente.ArchivoUrl));
            }

            return expediente;
            
        }

        public void Agregar(ExpedienteEstudioFormularioCapturaDTO expedienteEstudioDTO, int idUsuario)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });
            
            int idExpediente = _expedienteEstudioRepository.ConsultarIdExpediente(idUsuario);

            var expedienteEstudio = new ExpedienteEstudio()
            {
                IdExpediente = idExpediente,
                Nombre = expedienteEstudioDTO.Nombre,
                FechaRealizacion = expedienteEstudioDTO.FechaRealizacion,
                ArchivoTipoMime = expedienteEstudioDTO.ArchivoTipoMime,
                ArchivoNombre = expedienteEstudioDTO.ArchivoNombre,
                ArchivoUrl = ""
            };
            var expedienteEstudioAgregado = _expedienteEstudioRepository.Agregar(expedienteEstudio);

            var path = this.GuardarArchivo(expedienteEstudioDTO.Archivo, expedienteEstudioDTO.ArchivoNombre, expedienteEstudioDTO.ArchivoTipoMime, idUsuario, expedienteEstudioAgregado.IdExpedienteEstudio);

            expedienteEstudioAgregado.ArchivoUrl = path;
            _expedienteEstudioRepository.Editar(expedienteEstudioAgregado);

            scope.Complete();

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

        public string GuardarArchivo(byte[] archivo, string nombre, string tipoMime, int idUsuario, int idExpedienteEstudio)
        {
            string nombreArchivo = $" {idExpedienteEstudio}_{nombre}"; //idExpedienteEstudio al inicio para diferenciar aunque sea la misma imagen
            string path = Path.Combine("Archivos", "Expediente", nombreArchivo);
            var archivoBase64 = Convert.ToBase64String(archivo);

            _sftpService.UploadBytesFile(path, archivoBase64);

            //return path;
            var archivoMensaje = new Archivo
            {
                Nombre = nombreArchivo,
                ArchivoNombre = nombreArchivo,
                ArchivoTipoMime = tipoMime,
                ArchivoUrl = path,
                FechaRealizacion = DateTime.Now,
                IdUsuario = idUsuario
            };


            var fileUploaded = _archivoRepository.Agregar(archivoMensaje);

            return fileUploaded.ArchivoUrl;

        }

    }
}
