using System.Transactions;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Dashboard;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Services.GestionExpediente
{
    public class ExpedientePadecimientoService
    {
        private IExpedientePadecimientoRepository expedientePadecimientoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioService _usuarioService;
        private readonly IAsistenteDoctorRepository _asistenteDoctorRepository;
        private readonly IExpedienteTrackrRepository _expedienteTrackrRepository;
        private readonly IExpedienteDoctorRepository _expedienteDoctorRepository;
        private readonly UsuarioWidgetService _usuarioWidgetService;
        public ExpedientePadecimientoService(
            IExpedientePadecimientoRepository expedientePadecimientoRepository,
            IUsuarioRepository usuarioRepository,
            IAsistenteDoctorRepository asistenteDoctorRepository,
            IExpedienteTrackrRepository expedienteTrackrRepository,
            IExpedienteDoctorRepository expedienteDoctorRepository,
            UsuarioService usuarioService,
            UsuarioWidgetService widgetService
            )
        {
            this.expedientePadecimientoRepository = expedientePadecimientoRepository;
            _usuarioRepository = usuarioRepository;
            _asistenteDoctorRepository = asistenteDoctorRepository;
            _expedienteTrackrRepository = expedienteTrackrRepository;
            _expedienteDoctorRepository = expedienteDoctorRepository;
            _usuarioService = usuarioService;
            _usuarioWidgetService = widgetService;

        }

        public IEnumerable<ExpedientePadecimientoDTO> Consultar(int idDoctor , int idCompania)
        {
            List<int> idDoctorList = new();
            var esAsistente = _usuarioService.EsAsistente(idCompania, idDoctor);

            if(esAsistente){
               idDoctorList = _asistenteDoctorRepository.ConsultarDoctoresPorAsistente(idDoctor)
                                                        .Select( ad => ad.IdUsuario).ToList(); 
            }
            else{
                idDoctorList.Add(idDoctor);
            }

            return _expedienteDoctorRepository.ConsultarPacientesPorPadecimiento(idDoctorList);
        }

        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarParaSelector()
        {
            return expedientePadecimientoRepository.ConsultarParaSelector();
        }

        public IEnumerable<ExpedientePadecimientoDTO> ConsultarPorUsuario(int idUsuario)
        {
            return expedientePadecimientoRepository.ConsultarPorUsuario(idUsuario);
        }
        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarPorUsuarioParaSelector(int idUsuario)
        {
            return expedientePadecimientoRepository.ConsultarPorUsuarioParaSelector(idUsuario);
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
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });
                                    
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

            _usuarioWidgetService.AgregarWidgetPadecimiento(idUsuario, expedientePadecimientoDto.IdPadecimiento);
            var intExpedientePadecimiento = expedientePadecimientoRepository.Agregar(expedientePadecimiento).IdExpedientePadecimiento;

            scope.Complete();

            return intExpedientePadecimiento;            
        }
    }
}
