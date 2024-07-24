using System.Threading;
using System.Threading.Tasks;
using TrackrAPI.Models;
using TrackrAPI.Services.Notificaciones;

public interface IRecordatorioTomasService : IHostedService , IDisposable
{
    Task StartAsync(CancellationToken cancellationToken);
    Task StopAsync(CancellationToken cancellationToken);    bool EsSiguienteCuartoHora(DateTime lastUpdated);
    void ActualizarContador(TrackrContext context);
    Task CrearTratamientoTomas(TrackrContext context, NotificacionPacienteService notificacionPacienteService);

}
