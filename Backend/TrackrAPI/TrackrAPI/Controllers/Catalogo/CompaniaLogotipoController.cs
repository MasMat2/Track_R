using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeTypes;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using System;
using System.IO;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniaLogotipoController : ControllerBase
    {
        private readonly CompaniaLogotipoService companiaLogotipoService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public CompaniaLogotipoController(
            CompaniaLogotipoService companiaLogotipoService,
            IWebHostEnvironment hostingEnvironment)
        {
            this.companiaLogotipoService = companiaLogotipoService;
            this.hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("consultarPorCompania/{idCompania}")]
        public CompaniaLogotipoDto ConsultarPorCompania(int idCompania)
        {
            return companiaLogotipoService.ConsultarPorCompania(idCompania);
        }

        [HttpGet]
        [Route("consultar/{idCompaniaLogotipo}")]
        public CompaniaLogotipoDto Consultar(int idCompaniaLogotipo)
        {
            return companiaLogotipoService.ConsultarDto(idCompaniaLogotipo);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(CompaniaLogotipoDto companiaLogotipoBase64)
        {
            companiaLogotipoBase64.Src = Path.GetFileNameWithoutExtension(companiaLogotipoBase64.NombreImagen);

            CompaniaLogotipo companiaLogotipo = new()
            {
                IdCompania = companiaLogotipoBase64.IdCompania,
                Imagen = companiaLogotipoBase64.Src,
                TipoMime = companiaLogotipoBase64.TipoMime,
                Archivo = Convert.FromBase64String(companiaLogotipoBase64.ImagenBase64)
            };

            string fileExtension = MimeTypeMap.GetExtension(companiaLogotipoBase64.TipoMime);
            string directoryPath = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "CompaniaLogotipo");
            string filePath = Path.Combine(directoryPath, companiaLogotipo.Imagen + fileExtension);

            var fileCount = 1;

            while (System.IO.File.Exists(filePath))
            {
                var newFileName = companiaLogotipoBase64.Src + "-" + fileCount.ToString();

                filePath = Path.Combine(directoryPath, newFileName + fileExtension);
                companiaLogotipo.Imagen = newFileName;

                fileCount++;
            }

            //System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(companiaLogotipoBase64.ImagenBase64));

            companiaLogotipoService.Agregar(companiaLogotipo);
        }

        [HttpDelete]
        [Route("eliminar/{idCompaniaLogotipo}")]
        public void Eliminar(int idCompaniaLogotipo)
        {
            companiaLogotipoService.Eliminar(idCompaniaLogotipo);
        }
    }
}
