using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Models;
using TrackrAPI.Services.Notificaciones;


namespace TrackrAPI.Helpers;

public class RecordatorioTomasService : IRecordatorioTomasService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer _timer;
    private bool Update { get; set; } = false;

    private readonly int waitTime = 5;

    public RecordatorioTomasService(IServiceScopeFactory scopeFactory
    )
    {
        
        _scopeFactory = scopeFactory;
    }

    public void SetUpdate(bool update){
        Update = update;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Start the timer to run the ProcesoDeFondo method every 15 minutes
         _timer = new Timer(async state =>
        {
            await ProcesoDeFondo(state);
        }, null, TimeSpan.Zero, TimeSpan.FromMinutes(waitTime));
        return Task.CompletedTask;
    }

    private async Task ProcesoDeFondo(object state)
    {
        // Crear scope para acceder a TrackrContext
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<TrackrContext>();
            var notificacionPacienteService = scope.ServiceProvider.GetRequiredService<NotificacionPacienteService>();

            // Obtener DistributedLock para RecordatorioTomas
            // Este registro de la base de datos nos permite conocer 
            // el momento en el que fue ejecutado por ultima vez el proceso
            var dLock = context.DistributedLocks
                                .Where(dl => dl.Resource == "RecordatorioTomas")
                                .FirstOrDefault();

            // Actualizar DistributedLock solo en el siguiente cuarto de hora y ejecutar proceso
            if(dLock != null && EsSiguienteCuartoHora(dLock.LastUpdated) || Update){
                context.DistributedLocks.Attach(dLock);
                dLock.LastUpdated = DateTime.Now.ToUniversalTime();
                context.SaveChanges();
                
                ActualizarContador(context);
                await CrearTratamientoTomas(context , notificacionPacienteService);

            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }


    // Helpers
    public bool EsSiguienteCuartoHora(DateTime lastUpdated)
    {
        DateTime nextQuarterDateTime = lastUpdated.AddMinutes(15 - (lastUpdated.Minute % 15));
        return DateTime.Now.ToUniversalTime() > nextQuarterDateTime;
    }

    // Procesos
    public void ActualizarContador(TrackrContext context){
        var dl = context.DistributedLocks.Where(dl => dl.Resource != "RecordatorioTomas").FirstOrDefault();
        int x = 0;
        if(dl != null){
            context.DistributedLocks.Attach(dl);
            int.TryParse(dl.Resource, out x);
            dl.Resource = (x+1).ToString();
            context.SaveChanges();
        }
    }

    public async Task CrearTratamientoTomas(TrackrContext context , NotificacionPacienteService notificacionPacienteService){
        DateTime now = DateTime.Now.ToUniversalTime();
        int currentDay = ((int)now.DayOfWeek + 7) % 7; // Monday = 1, ..., Saturday = 6, Sunday = 0
        TimeSpan currentTime = now.TimeOfDay;
        TimeSpan timeIn15Minutes = currentTime + TimeSpan.FromMinutes(15);
        DateTime time15MinutesAgo = now - TimeSpan.FromMinutes(15);

        var recordatoriosToProcess = context.TratamientoRecordatorio
        .Include( tr => tr.IdExpedienteTratamientoNavigation)
            .ThenInclude( et => et.IdExpedienteNavigation)
        .Where(tr => 
            tr.Dia == currentDay &&
            tr.Hora >= currentTime &&
            tr.Hora <= timeIn15Minutes &&
            !tr.TratamientoToma.Any(toma => toma.FechaEnvio > time15MinutesAgo) // Asegura que no se ha creado un TratamientoToma para el TratamientoRecordatorio en los últimos 15 minutos
        )
        .ToList();
        foreach (var recordatorio in recordatoriosToProcess)
        {

            var mensajeNotificacion = $"Tomar {recordatorio.IdExpedienteTratamientoNavigation.Cantidad} {recordatorio.IdExpedienteTratamientoNavigation.Unidad}";
            var idUsuario = recordatorio.IdExpedienteTratamientoNavigation.IdExpedienteNavigation.IdUsuario;
            var notificacion = new NotificacionCapturaDTO
            (
                recordatorio.IdExpedienteTratamientoNavigation.Farmaco,
                mensajeNotificacion,
                recordatorio.Hora.ToString(),
                6,
                idUsuario,
                null,
                null
            );

            var notificacionInsertada = await notificacionPacienteService.Notificar(notificacion , idUsuario);

            var toma = new TratamientoToma
            {
                IdTratamientoRecordatorio = recordatorio.IdTratamientoRecordatorio,
                FechaEnvio = now,
                IdNotificacion = notificacionInsertada.IdNotificacion 
            };
            
            context.TratamientoToma.Add(toma);
        }

        context.SaveChanges();

    }
}
