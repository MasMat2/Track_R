using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.GestionExpediente;
public class DetalleExpedienteRecomendacionGeneralService
{
    private readonly IDetalleExpedienteRecomendacionGeneral _detalleExpedienteRecomendacionGeneral;
    
    public DetalleExpedienteRecomendacionGeneralService(
            IDetalleExpedienteRecomendacionGeneral detalleExpedienteRecomendacionGeneral)
    {
        _detalleExpedienteRecomendacionGeneral = detalleExpedienteRecomendacionGeneral;
    }

    public void eliminarDetalle(int IdExpedienteRecomendacionGeneral)
    {
        _detalleExpedienteRecomendacionGeneral.eliminarDetalles(IdExpedienteRecomendacionGeneral);
    }
}

