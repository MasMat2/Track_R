using Microsoft.AspNetCore.Hosting;
using MimeTypes;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Services.Catalogo
{
    public class HospitalLogotipoService
    {
        private IHospitalLogotipoRepository hospitalLogotipoRepository;
        private IWebHostEnvironment hostingEnvironment;

        public HospitalLogotipoService(IHospitalLogotipoRepository hospitalLogotipoRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.hospitalLogotipoRepository = hospitalLogotipoRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public HospitalLogotipoDto ConsultarDto(int idHospitalLogotipo)
        {
            return hospitalLogotipoRepository.ConsultarDto(idHospitalLogotipo);
        }

        public HospitalLogotipoDto ConsultarPorHospital(int idHospital)
        {
            return hospitalLogotipoRepository.ConsultarPorHospital(idHospital);
        }

        public void Agregar(HospitalLogotipo hospitalLogotipo)
        {
            var hospitalLogotipoExist = hospitalLogotipoRepository.ConsultarPorHospital(hospitalLogotipo.IdHospital);

            if (hospitalLogotipoExist == null)
            {
                hospitalLogotipoRepository.Agregar(hospitalLogotipo);
            }
            else
            {
                throw new CdisException("Ya existe una imagen asociada al Hospital");
            }
        }

        public void Eliminar(int idHospitalImagen)
        {
            var hospitalLogotipo = hospitalLogotipoRepository.Consultar(idHospitalImagen);

            hospitalLogotipoRepository.Eliminar(hospitalLogotipo);

            var path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "HospitalLogotipo", $"{hospitalLogotipo.Imagen}{MimeTypeMap.GetExtension(hospitalLogotipo.TipoMime)}");
            File.Delete(path);
        }
    }
}
