using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class HospitalLogotipoRepository : Repository<HospitalLogotipo>, IHospitalLogotipoRepository
    {
        public HospitalLogotipoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public HospitalLogotipo Consultar(int idHospitalLogotipo)
        {
            return context.HospitalLogotipo
                .Where(hl => hl.IdHospitalLogotipo == idHospitalLogotipo)
                .FirstOrDefault();
        }

        public HospitalLogotipoDto ConsultarDto(int idHospitalLogotipo)
        {
            return context.HospitalLogotipo
                .Where(hl => hl.IdHospitalLogotipo == idHospitalLogotipo)
                .Select(hl => new HospitalLogotipoDto
                {
                    IdHospitalLogotipo = hl.IdHospitalLogotipo,
                    IdHospital = hl.IdHospital,
                    Src = hl.Imagen,
                    TipoMime = hl.TipoMime
                })
                .FirstOrDefault();
        }

        public HospitalLogotipoDto ConsultarPorHospital(int idHospital)
        {
            return context.HospitalLogotipo
                .Where(hl => hl.IdHospital == idHospital)
                .Select(hl => new HospitalLogotipoDto
                {
                    IdHospitalLogotipo = hl.IdHospitalLogotipo,
                    IdHospital = hl.IdHospital,
                    Src = GeneralConstant.RutaArchivoHospitalLogotipo + hl.IdHospitalLogotipo,
                    TipoMime = hl.TipoMime,
                    NombreImagen = hl.Imagen
                })
                .FirstOrDefault();
        }

    }
}
