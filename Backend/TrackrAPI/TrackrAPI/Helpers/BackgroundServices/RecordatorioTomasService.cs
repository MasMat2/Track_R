using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using TrackrAPI.Models;


namespace TrackrAPI.Helpers;

public class RecordatorioTomasService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer _timer;

    public RecordatorioTomasService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Start the timer to run the ProcesoDeFondo method every 15 minutes
        _timer = new Timer(ProcesoDeFondo, null, TimeSpan.Zero, TimeSpan.FromMinutes(15));
        return Task.CompletedTask;
    }

    private void ProcesoDeFondo(object state)
    {
        // Crear scope para acceder a TrackrContext
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<TrackrContext>();

            // Obtener DistributedLock para RecordatorioTomas
            // Este registro de la base de datos nos permite conocer 
            // el momento en el que fue ejecutado por ultima vez el proceso
            var dLock = context.DistributedLocks
                                .Where(dl => dl.Resource == "RecordatorioTomas")
                                .FirstOrDefault();

            // Actualizar DistributedLock solo en el siguiente cuarto de hora y ejecutar proceso
            if(dLock != null && EsSiguienteCuartoHora(dLlock.LastUpdated)){
                context.DistributedLocks.Attach(dLock);
                dlock.LastUpdated = DateTime.UtcNow;
                context.SaveChanges();
                
                ActualizarContador(context);
                // CrearTratamientoTomas(context);

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
        return DateTime.UtcNow > nextQuarterDateTime;
    }

    // Procesos
    public void ActualizarContador(TrackrContext context){
        var dl = context.DistributedLocks.Where(dl => dl.Resource != "TratamientoToma").FirstOrDefault();
        int x = 0;
        if(dl != null){
            context.DistributedLocks.Attach(dLock);
            Int32.TryParse(TextBoxD1.Text, out x);
            dlock.IdDistributedLocks = (x+1).ToString();
            context.SaveChanges();
        }
    }

    public void CrearTratamientoTomas(TrackrContext context){
        DateTime now = DateTime.Now;
        int currentDay = (int)now.DayOfWeek; // Sunday = 0, Monday = 1, ..., Saturday = 6
        if (currentDay == 0) currentDay = 6; // Adjusting to Monday = 0, ..., Sunday = 6
        TimeSpan currentTime = now.TimeOfDay;
        TimeSpan timeIn15Minutes = currentTime + TimeSpan.FromMinutes(15);
        TimeSpan time15MinutesAgo = currentTime - TimeSpan.FromMinutes(15);

        var recordatoriosToProcess = context.TratamientoRecordatorio
        .Where(tr => 
            tr.IdExpedienteTratamientoNavigation.FechaInicio <= now &&
            (tr.IdExpedienteTratamientoNavigation.FechaFin == null || tr.IdExpedienteTratamientoNavigation.FechaFin >= now) &&
            tr.Dia == currentDay &&
            tr.Hora >= currentTime &&
            tr.Hora <= timeIn15Minutes &&
            !tr.TratamientoToma.Any(toma => toma.FechaEnvio > time15MinutesAgo) // Asegura que no se ha creado un TratamientoToma para el TratamientoRecordatorio en los últimos 15 minutos
        )
        .ToList();

        foreach (var recordatorio in recordatoriosToProcess)
        {
            var toma = new TratamientoToma
            {
                IdTratamientoRecordatorio = recordatorio.IdTratamientoRecordatorio,
                FechaEnvio = now
            };
            
            context.TratamientoToma.Add(toma);
        }

        context.SaveChanges();

    }
}
