using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeTypes;
using TrackrAPI.Helpers;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Services.Seguridad;
using System.IO;
using TrackrAPI.Services.Inventario;
using TrackrAPI.Services.Archivos;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly HospitalLogotipoService hospitalLogotipoService;
        private readonly CompaniaLogotipoService companiaLogotipoService;
        private readonly UsuarioService usuarioService;
        private readonly ArchivoService archivoService;

        public ArchivoController(
            IWebHostEnvironment hostingEnvironment,
            HospitalLogotipoService hospitalLogotipoService,
            CompaniaLogotipoService companiaLogotipoService,
            UsuarioService usuarioService,
            ArchivoService archivoService
            )
        {
            this.hostingEnvironment = hostingEnvironment;
            this.hospitalLogotipoService = hospitalLogotipoService;
            this.companiaLogotipoService = companiaLogotipoService;
            this.usuarioService = usuarioService;
            this.archivoService = archivoService;
        }

        [HttpGet("HospitalLogotipo/{idHospitalLogotipo}")]
        public IActionResult ObtenerHospitalLogotipo(int idHospitalLogotipo)
        {
            var hospital = hospitalLogotipoService.ConsultarDto(idHospitalLogotipo);
            string directoryPath = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "HospitalLogotipo");
            string path = Path.Combine(directoryPath, $"{hospital.Src}{MimeTypeMap.GetExtension(hospital.TipoMime)}");

            string pathDefault = Path.Combine(directoryPath, GeneralConstant.NombreImagenAtiDefault);
            string tipoMimeDefault = GeneralConstant.TipoMimeAtiDefault;

            if (!System.IO.File.Exists(path))
            {
                path = pathDefault;
                hospital.TipoMime = tipoMimeDefault;

                if (!System.IO.File.Exists(pathDefault))
                    throw new CdisException("No se encontró la imagen");
            }

            var imageFileStream = System.IO.File.OpenRead(path);

            return File(imageFileStream, hospital.TipoMime);
        }

        [HttpGet("CompaniaLogotipo/{idCompaniaLogotipo}")]
        public IActionResult ObtenerCompaniaLogotipo(int idCompaniaLogotipo)
        {
            var companiaLogotipo = companiaLogotipoService.ConsultarDto(idCompaniaLogotipo);
            string directoryPath = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "CompaniaLogotipo");
            string filename = companiaLogotipo.NombreImagen + MimeTypeMap.GetExtension(companiaLogotipo.TipoMime);
            string path = Path.Combine(directoryPath, filename);

            string tipoMimeDefault = GeneralConstant.TipoMimeAtiDefault;
            string pathDefault = Path.Combine(directoryPath, GeneralConstant.NombreImagenAtiDefault);

            if (!System.IO.File.Exists(path))
            {
                path = pathDefault;
                companiaLogotipo.TipoMime = tipoMimeDefault;

                if (!System.IO.File.Exists(pathDefault))
                    throw new CdisException("No se encontró la imagen");
            }

            var imageFileStream = System.IO.File.OpenRead(path);

            return File(imageFileStream, companiaLogotipo.TipoMime);
        }

        [HttpGet("usuario/{idUsuario}")]
        public IActionResult ObtenerUsuarioImagen(int idUsuario)
        {
            var usuario = usuarioService.Consultar(idUsuario);
            string path;
            string tipoMime;

            if (string.IsNullOrWhiteSpace(usuario.ImagenTipoMime))
            {
                path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "Usuario", $"default.svg");
                tipoMime = "image/svg+xml";
            }
            else
            {
                var archivo = archivoService.ObtenerImagenUsuario(idUsuario);
                if (archivo == null)
                {
                    path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "Usuario", $"default.svg");
                    tipoMime = "image/svg+xml";
                }
                else
                {
                    MemoryStream ms = new MemoryStream();

                    BinaryFormatter binForm = new BinaryFormatter();
                    ms.Write(archivo.Archivo1, 0, archivo.Archivo1.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    return File(ms, archivo.ArchivoTipoMime);

                }


                // path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "Usuario", $"{usuario.IdUsuario}{ MimeTypeMap.GetExtension(usuario.ImagenTipoMime)}");
                // tipoMime = usuario.ImagenTipoMime;

                // if (!System.IO.File.Exists(path))
                // {
                //     path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "Usuario", $"default.svg");
                //     tipoMime = "image/svg+xml";
                // }
            }

            var imageFileStream = System.IO.File.OpenRead(path);

            return File(imageFileStream, tipoMime);
        }
        [HttpGet("usuarioEnSesionImagen/")]
        public IActionResult ObtenerUsuarioEnSesionImagen()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return ObtenerUsuarioImagen(usuario.IdUsuario);
        }

        [HttpGet("descargarPlantillaArticulos")]
        public IActionResult DescargarPlantillaArticulos()
        {
            string path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "PlantillasCargaMasiva", $"Plantilla Articulos.xlsx");
            string tipoMime = "application/vnd.ms-excel";
            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, tipoMime);
        }

        [HttpGet("descargarPlantillaCargaMasivaPoliza")]
        public IActionResult DescargarPlantillaCargaMasivaPoliza()
        {
            string path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "PlantillasCargaMasiva", $"Plantilla Polizas.xlsx");
            string tipoMime = "application/vnd.ms-excel";
            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, tipoMime);
        }

        [HttpGet("descargarPlantillaCargaMasivaCuentas")]
        public IActionResult DescargarPlantillaCargaMasivaCuentas()
        {
            string path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "PlantillasCargaMasiva", $"Plantilla Cuentas.xlsx");
            string tipoMime = "application/vnd.ms-excel";
            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, tipoMime);
        }

        [HttpGet("descargarPlantillaCargaMasivaAuxiliares")]
        public IActionResult DescargarPlantillaCargaMasivaAuxiliares()
        {
            string path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "PlantillasCargaMasiva", $"Plantilla Auxiliares.xlsx");
            string tipoMime = "application/vnd.ms-excel";
            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, tipoMime);
        }

        [HttpGet("descargarPlantillaCargaMasivaMovimientos")]
        public IActionResult DescargarPlantillaCargaMasivaMovimientos()
        {
            string path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "PlantillasCargaMasiva", $"Plantilla Carga Masiva Movimientos.xlsx");
            string tipoMime = "application/vnd.ms-excel";
            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, tipoMime);
        }

        [HttpGet("descargarPlantillaCargaMasivaPresentaciones")]
        public IActionResult DescargarPlantillaCargaMasivaPresentaciones()
        {
            string path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "PlantillasCargaMasiva", $"Plantilla Carga Masiva Presentaciones.xlsx");
            string tipoMime = "application/vnd.ms-excel";
            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, tipoMime);
        }
    }
}
