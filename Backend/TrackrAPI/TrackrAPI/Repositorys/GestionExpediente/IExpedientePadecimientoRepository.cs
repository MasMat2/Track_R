using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente
{
    public interface IExpedientePadecimientoRepository : IRepository<ExpedientePadecimiento>
    {
        public IEnumerable<ExpedientePadecimientoDTO> Consultar(int idDoctor);
        public IEnumerable<ExpedientePadecimientoDTO> ConsultarPorUsuario(int idUsuario);
        public IEnumerable<ExpedientePadecimientoGridDTO> ConsultarParaGridPorUsuario(int idUsuario);
        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarParaSelector();
        public void EliminarPorExpediente(int idExpediente);
        public IEnumerable<PadecimientoFueraRangoDTO> ConsultarValoresFueraRango(int idPadecimiento, int idUsuario);
        public IEnumerable<ExpedientePadecimiento> ConsultarPorPadecimiento(int? idPadecimiento);
        public List<int> ConsultarIdsDoctorPorUsuario(int idUsuario);
    }
}
