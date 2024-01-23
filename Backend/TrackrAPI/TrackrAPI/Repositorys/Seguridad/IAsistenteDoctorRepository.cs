using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IAsistenteDoctorRepository : IRepository<AsistenteDoctor>
    {
        public AsistenteDoctor Consultar(int idAsistenteDoctor);
        public IEnumerable<AsistenteDoctorDto> ConsultarAsistentesPorDoctor(int idDoctor);
        public IEnumerable<AsistenteDoctorDto> ConsultarDoctoresPorAsistente(int idAsistente);
    }
}
