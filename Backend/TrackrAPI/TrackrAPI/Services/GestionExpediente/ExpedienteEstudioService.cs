using System.Transactions;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Archivos;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Services.Sftp;
using TrackrAPI.Helpers;

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
        ) {
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

            //expediente.Archivo = new Byte[0];

            //if (expediente.ArchivoUrl != null || expediente.ArchivoNombre != "")
            //{
            //    expediente.Archivo = Convert.FromBase64String(_sftpService.DownloadFileAsBase64(expediente.ArchivoUrl));
            //}

            return expediente;

        }

        public void Agregar(ExpedienteEstudioFormularioCapturaDTO expedienteEstudioDTO, int idUsuario)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

            if (expedienteEstudioDTO.Archivo is null)
            {
                throw new CdisException("Debe agregar un adjunto");
            }

            int idExpediente = _expedienteEstudioRepository.ConsultarIdExpediente(idUsuario);

            var archivo = this.GuardarArchivo(expedienteEstudioDTO.Archivo, expedienteEstudioDTO.ArchivoNombre, expedienteEstudioDTO.ArchivoTipoMime, idUsuario);

            var expedienteEstudio = new ExpedienteEstudio()
            {
                IdExpediente = idExpediente,
                Nombre = expedienteEstudioDTO.Nombre,
                FechaRealizacion = expedienteEstudioDTO.FechaRealizacion,
                IdArchivo = archivo.id,
                ArchivoUrl = archivo.url
            };

            _expedienteEstudioRepository.Agregar(expedienteEstudio);

            scope.Complete();

        }

        public void Eliminar(int idExpedienteEstudio)
        {
            var expedienteEstudio = _expedienteEstudioRepository.Consultar(idExpedienteEstudio);
            _expedienteEstudioRepository.Eliminar(expedienteEstudio);
        }

        public (int id, string url) GuardarArchivo(byte[] archivo, string nombre, string tipoMime, int idUsuario)
        {
            string nombreArchivo = $"{nombre}";
            string path = Path.Combine("Archivos", "Expediente", $"{Guid.NewGuid()}.{tipoMime.Split('/')[1]}");
            var archivoBase64 = Convert.ToBase64String(archivo);

            _sftpService.UploadBytesFile(path, archivoBase64);

            var archivoEstudio = new Archivo
            {
                Nombre = nombreArchivo,
                ArchivoNombre = nombreArchivo,
                ArchivoTipoMime = tipoMime,
                ArchivoUrl = path,
                FechaRealizacion = DateTime.Now,
                IdUsuario = idUsuario
            };


            var fileUploaded = _archivoRepository.Agregar(archivoEstudio);

            return (fileUploaded.IdArchivo, fileUploaded.ArchivoUrl);

        }

    }
}
