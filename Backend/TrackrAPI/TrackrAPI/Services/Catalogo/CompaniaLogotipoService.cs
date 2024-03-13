using Microsoft.AspNetCore.Hosting;
using MimeTypes;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.IO;

namespace TrackrAPI.Services.Catalogo
{
    public class CompaniaLogotipoService
    {
        private readonly ICompaniaLogotipoRepository companiaLogotipoRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public CompaniaLogotipoService(
            ICompaniaLogotipoRepository companiaLogotipoRepository,
            IWebHostEnvironment hostingEnvironment)
        {
            this.companiaLogotipoRepository = companiaLogotipoRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public CompaniaLogotipoDto ConsultarDto(int idCompaniaLogotipo)
        {
            return companiaLogotipoRepository.ConsultarDto(idCompaniaLogotipo);
        }

        public CompaniaLogotipoDto ConsultarPorCompania(int idCompania)
        {
            return companiaLogotipoRepository.ConsultarPorCompania(idCompania);
        }

        public void Agregar(CompaniaLogotipo companiaLogotipo)
        {
            var companiaLogotipoExist = companiaLogotipoRepository.ConsultarPorCompania(companiaLogotipo.IdCompania);

            if (companiaLogotipoExist == null)
            {
                companiaLogotipoRepository.Agregar(companiaLogotipo);
            }
            else
            {
                throw new CdisException("Ya existe una imagen asociada al Compania");
            }
        }

        public void Eliminar(int idCompaniaLogotipo)
        {
            var companiaLogotipo = companiaLogotipoRepository.Consultar(idCompaniaLogotipo);

            companiaLogotipoRepository.Eliminar(companiaLogotipo);

            string fileName = companiaLogotipo.Imagen + MimeTypeMap.GetExtension(companiaLogotipo.TipoMime);
            string directoryPath = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "CompaniaLogotipo");
            string filePath = Path.Combine(directoryPath, fileName);

            File.Delete(filePath);
        }
    }
}
