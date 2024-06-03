using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.DTOs.Dashboard;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente
{
    public interface IExpedienteTrackrRepository : IRepository<ExpedienteTrackr>
    {
        public ExpedienteTrackr Consultar(int idExpediente);
        public ExpedienteTrackr ConsultarPorNumero(string numero);
        public ExpedienteTrackr ConsultarPorUsuario(int idUsuario);

        public IEnumerable<UsuarioExpedienteGridDTO> ConsultarParaGrid(List<int> idDoctor, int idCompania);
        public UsuarioExpedienteSidebarDTO ConsultarParaSidebar(int idUsuario);
        public int DosisNoTomadas(int idExpediente);
        public int VariablesFueraRango(int idUsuario);
        public IEnumerable<ApegoTomaMedicamentoDto> ApegoMedicamentoUsuarios(List<int> idDoctor);
        public IEnumerable<ApegoTomaMedicamentoDto> ApegoTratamientoPorPaciente(int idUsuario);
        public IEnumerable<ExpedienteTrackr> ConsultarExpedientes();
        public IEnumerable<RecordatorioUsuarioDto> RecordatoriosPorUsuario(int idUsuario);

        public IEnumerable<UsuarioExpedienteGridDTO> ConsultarParaRecomendacionesGenerales();
    }
}
