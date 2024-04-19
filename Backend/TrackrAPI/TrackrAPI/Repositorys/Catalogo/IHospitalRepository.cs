using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IHospitalRepository : IRepository<Hospital>
    {
        public IEnumerable<HospitalGridDto> ConsultarPorCompaniaParaGrid(int idCompania);
        public HospitalDto ConsultarDto(int idHospital);
        public IEnumerable<HospitalGridDto> ConsultarGeneral(int idCompania);
        public IEnumerable<HospitalDto> ConsultarPorCompania(int idCompania);
        public Hospital Consultar(int idHotel);
        public Hospital Consultar(string rfc);
        public Hospital ConsultarPorUsuario(int idUsuario);
        public Hospital ConsultarConDependencias(int idHospital);
        public IEnumerable<HospitalDto> ConsultarTodosParaSelector(int idDominio);
        // public IEnumerable<HospitalDto> ConsultarDisponiblesParaListaPrecio(int? idListaPrecioSeleccionada);
        public HospitalDto ConsultarPorID(int idHospital);
        public HospitalDto ConsultarDefaultPorCompania(int idCompania);
    }
}