using TrackrAPI.Dtos.GestionExpediente.ExpedienteDoctor;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;

public interface IExpedienteDoctorRepository : IRepository<ExpedienteDoctor>
{
    public IEnumerable<ExpedienteDoctor> Consultar(int idExpediente);
    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarSelector(int idExpediente);
    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarDoctores();

}