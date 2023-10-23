using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;
public class DetalleExpedienteRecomendacionGeneral : Repository<DetalleExpedienteRecomendacionesGenerales>, IDetalleExpedienteRecomendacionGeneral
{
    public DetalleExpedienteRecomendacionGeneral (TrackrContext context): base(context)
    {
        base.context = context;
    }
}

