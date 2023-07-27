using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeTypes;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalLogotipoController : ControllerBase
    {
        private HospitalLogotipoService hospitalLogotipoService;
        private IWebHostEnvironment hostingEnvironment;

        public HospitalLogotipoController(HospitalLogotipoService hospitalLogotipoService, IWebHostEnvironment hostingEnvironment)
        {
            this.hospitalLogotipoService = hospitalLogotipoService;
            this.hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("consultarPorHospital/{idHospital}")]
        public HospitalLogotipoDto ConsultarPorHospital(int idHospital)
        {
            return hospitalLogotipoService.ConsultarPorHospital(idHospital);
        }

        [HttpGet]
        [Route("consultar/{idHospitalLogotipo}")]
        public HospitalLogotipoDto Consultar(int idHospitalLogotipo)
        {
            return hospitalLogotipoService.ConsultarDto(idHospitalLogotipo);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(HospitalLogotipoDto hospitalLogotipoBase64)
        {
            hospitalLogotipoBase64.Src = Path.GetFileNameWithoutExtension(hospitalLogotipoBase64.NombreImagen);

            HospitalLogotipo hospitalLogotipo = new()
            {
                IdHospital = hospitalLogotipoBase64.IdHospital,
                Imagen = hospitalLogotipoBase64.Src,
                TipoMime = hospitalLogotipoBase64.TipoMime
            };

            string fileExtension = MimeTypeMap.GetExtension(hospitalLogotipoBase64.TipoMime);
            string directoryPath = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "HospitalLogotipo");
            string filePath = Path.Combine(directoryPath, hospitalLogotipo.Imagen + fileExtension);

            var fileCount = 1;

            while (System.IO.File.Exists(filePath))
            {
                var newFileName = hospitalLogotipoBase64.Src + "-" + fileCount.ToString();

                filePath = Path.Combine(directoryPath, newFileName + fileExtension);
                hospitalLogotipo.Imagen = newFileName;

                fileCount++;
            }

            System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(hospitalLogotipoBase64.ImagenBase64));

            hospitalLogotipoService.Agregar(hospitalLogotipo);
        }

        [HttpDelete]
        [Route("eliminar/{idHospitalLogotipo}")]
        public void Eliminar(int idHospitalLogotipo)
        {
            hospitalLogotipoService.Eliminar(idHospitalLogotipo);
        }
    }
}
