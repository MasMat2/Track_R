using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;
public interface IDetalleExpedienteRecomendacionGeneral : IRepository<DetalleExpedienteRecomendacionesGenerales>
{
    public void eliminarDetalles(int IdExpedienteRecomendacionGeneral);
}

