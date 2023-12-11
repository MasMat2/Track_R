using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;
public class DetalleExpedienteRecomendacionGeneral : Repository<DetalleExpedienteRecomendacionesGenerales>, IDetalleExpedienteRecomendacionGeneral
{
    public DetalleExpedienteRecomendacionGeneral (TrackrContext context): base(context)
    {
        base.context = context;
    }

    public void eliminarDetalles(int IdExpedienteRecomendacionGeneral)
    {
        var eliminarDetalles = context.DetalleExpedienteRecomendacionesGenerales
               .Where(exp => exp.IdExpedienteRecomendacionesGenerales == IdExpedienteRecomendacionGeneral)
               .ToList();

        context.DetalleExpedienteRecomendacionesGenerales.RemoveRange(eliminarDetalles);
        context.SaveChanges();
    }
}

