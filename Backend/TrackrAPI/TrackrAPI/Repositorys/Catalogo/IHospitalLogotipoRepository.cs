using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IHospitalLogotipoRepository : IRepository<HospitalLogotipo>
    {
        public HospitalLogotipo Consultar(int idHospitalLogotipo);
        public HospitalLogotipoDto ConsultarDto(int idHospitalLogotipo);
        public HospitalLogotipoDto ConsultarPorHospital(int idHospital);
    }
}
