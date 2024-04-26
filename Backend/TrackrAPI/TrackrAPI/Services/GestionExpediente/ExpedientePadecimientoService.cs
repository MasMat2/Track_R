using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.GestionExpediente
{
    public class ExpedientePadecimientoService
    {
        private IExpedientePadecimientoRepository expedientePadecimientoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAsistenteDoctorRepository _asistenteDoctorRepository;
        private readonly IExpedienteTrackrRepository _expedienteTrackrRepository;

        public ExpedientePadecimientoService(
            IExpedientePadecimientoRepository expedientePadecimientoRepository,
            IUsuarioRepository usuarioRepository,
            IAsistenteDoctorRepository asistenteDoctorRepository,
            IExpedienteTrackrRepository expedienteTrackrRepository
            )
        {
            this.expedientePadecimientoRepository = expedientePadecimientoRepository;
            _usuarioRepository = usuarioRepository;
            _asistenteDoctorRepository = asistenteDoctorRepository;
            _expedienteTrackrRepository = expedienteTrackrRepository;

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

        public void Eliminar(int idExpedientePadecimiento)
        {
            var expedientePadecimiento = expedientePadecimientoRepository.Consultar(idExpedientePadecimiento) ?? throw new CdisException("El padecimiento no se encuentra registrado en este expediente"); ;
            expedientePadecimientoRepository.Eliminar(expedientePadecimiento);
        }

        public int AgregarPadecimiento(AgregarExpedientePadecimientoDTO expedientePadecimientoDto, int idUsuario)
        {
            var expedienteUsuario = _expedienteTrackrRepository.ConsultarPorUsuario(idUsuario) ?? throw new CdisException("El usuario no tiene expediente aún");
            var expedientePadecimientoExistente = expedientePadecimientoRepository.ConsultarPorUsuario(idUsuario).Where(ep => ep.IdPadecimiento == expedientePadecimientoDto.IdPadecimiento).FirstOrDefault();

            if(expedientePadecimientoExistente != null)
            {
                throw new CdisException("El padecimiento ya está asociado a este expediente");
            }

            var expedientePadecimiento = new ExpedientePadecimiento
            {
                IdExpediente = expedienteUsuario.IdExpediente,
                FechaDiagnostico = expedientePadecimientoDto.FechaDiagnostico,
                IdPadecimiento = expedientePadecimientoDto.IdPadecimiento,
                IdUsuarioDoctor = expedientePadecimientoDto.IdUsuarioDoctor
            };

            return expedientePadecimientoRepository.Agregar(expedientePadecimiento).IdExpedientePadecimiento;

        }
    }
}
