using TrackrAPI.Models;
using TrackrAPI.Helpers;
using TrackrAPI.Dtos.Seguridad;

namespace TrackrAPI.Repositorys.Seguridad;

    public class AsistenteDoctorRepository : Repository<AsistenteDoctor>, IAsistenteDoctorRepository
    {

        public AsistenteDoctorRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public AsistenteDoctor Consultar(int idAsistenteDoctor)
        {
            return context.AsistenteDoctor
                .FirstOrDefault(ad => ad.IdAsistenteDoctor == idAsistenteDoctor);
        }

        public IEnumerable<AsistenteDoctorDto> ConsultarAsistentesPorDoctor(int idDoctor)
        {
            return context.AsistenteDoctor
                .Where(ad => ad.IdDoctor == idDoctor)
                .Select(ad => new AsistenteDoctorDto
                {
                    IdAsistenteDoctor = ad.IdAsistenteDoctor,
                    IdUsuario = ad.IdAsistenteNavigation.IdUsuario,
                    NombreAsistente = ad.IdAsistenteNavigation.ObtenerNombreCompleto()
                })
                .ToList();
        }
        public IEnumerable<AsistenteDoctorDto> ConsultarDoctoresPorAsistente(int idAsistente)
        {    
            return context.AsistenteDoctor
                .Where(ad => ad.IdAsistente == idAsistente)
                .Select(ad => new AsistenteDoctorDto
                {
                    IdAsistenteDoctor = ad.IdAsistenteDoctor,
                    IdUsuario = ad.IdDoctorNavigation.IdUsuario,
                    NombreAsistente  = ad.IdDoctorNavigation.ObtenerNombreCompleto()
                })
                .ToList();
        }

}
