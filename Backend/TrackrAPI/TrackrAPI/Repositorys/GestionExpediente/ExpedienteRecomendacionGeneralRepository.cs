using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;
public class ExpedienteRecomendacionGeneralRepository : Repository<ExpedienteRecomendacionesGenerales>, IExpedienteRecomendacionGeneralRepository
{
    public ExpedienteRecomendacionGeneralRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public IEnumerable<ExpedienteRecomendacionGeneralGridDTO> ConsultarGrid()
    {
        var tipoMapping = new Dictionary<int, string>
        {
            { 1, "Todos" },
            { 2, "Paciente por padecimiento" },
            { 3, "Pacientes" }
        };
        return context.ExpedienteRecomendacionesGenerales
        .Include(us => us.IdAdministradorNavigation)
        .Select(x => new ExpedienteRecomendacionGeneralGridDTO
        {
            IdExpedienteRecomendacion = x.IdExpedienteRecomendacionesGenerales,
            Fecha = x.FechaRealizacion.ToShortDateString(),
            Descripcion = x.Descripcion,
            Doctor = x.IdAdministradorNavigation.Nombre,
            Tipo = tipoMapping.ContainsKey((int)x.Tipo) ? tipoMapping[(int)x.Tipo] : "Desconocido"

        })
        .ToList();
    }
}

