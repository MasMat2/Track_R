using TrackrAPI.Dtos.GestionExpediente.ExpedienteDoctor;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;

public interface IExpedienteDoctorRepository : IRepository<ExpedienteDoctor>
{
    public IEnumerable<ExpedienteDoctor> ConsultarExpediente(int idExpediente);
    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarSelector(int idExpediente , int idCompania);
    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarDoctores(int idCompania);
    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarDoctores();

}