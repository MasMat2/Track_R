using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.GestionExpediente
{
    public class ExpedientePadecimientoService
    {
        private IExpedientePadecimientoRepository expedientePadecimientoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAsistenteDoctorRepository _asistenteDoctorRepository;

        public ExpedientePadecimientoService(
            IExpedientePadecimientoRepository expedientePadecimientoRepository,
            IUsuarioRepository usuarioRepository,
            IAsistenteDoctorRepository asistenteDoctorRepository
            )
        {
            this.expedientePadecimientoRepository = expedientePadecimientoRepository;
            _usuarioRepository = usuarioRepository;
            _asistenteDoctorRepository = asistenteDoctorRepository;

        }

        public IEnumerable<ExpedientePadecimientoDTO> Consultar(int idDoctor , int idCompania)
        {
            List<int> idDoctorList = new();
            var esAsistente = _usuarioRepository.ConsultarPorPerfil(idCompania, GeneralConstant.ClavePerfilAsistente)
                                                    .Any((usuario) => usuario.IdUsuario == idDoctor);

            if(esAsistente){
               idDoctorList = _asistenteDoctorRepository.ConsultarDoctoresPorAsistente(idDoctor)
                                                        .Select( ad => ad.IdUsuario).ToList(); 
            }
            else{
                idDoctorList.Add(idDoctor);
            }

            return expedientePadecimientoRepository.Consultar(idDoctorList);
        }

        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarParaSelector()
        {
            return expedientePadecimientoRepository.ConsultarParaSelector();
        }

        public IEnumerable<ExpedientePadecimientoDTO> ConsultarPorUsuario(int idUsuario)
        {
            return expedientePadecimientoRepository.ConsultarPorUsuario(idUsuario);
        }

        public IEnumerable<PadecimientoFueraRangoDTO> ConsultarValoresFueraRango(int idPadecimiento, int idUsuario)
        {
            return expedientePadecimientoRepository.ConsultarValoresFueraRango(idPadecimiento, idUsuario);
        }

        public IEnumerable<ExpedientePadecimientoGridDTO> ConsultarParaGridPorUsuario(int idUsuario)
        {
            return expedientePadecimientoRepository.ConsultarParaGridPorUsuario(idUsuario);
        }

    }
}
