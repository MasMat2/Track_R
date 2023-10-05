using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using TrackrAPI.Models;


namespace TrackrAPI.Helpers;

public class RecordatorioTomasService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer _timer;

    private int waitTime = 1;

    public RecordatorioTomasService(IServiceScopeFactory scopeFactory)
    {
        Console.WriteLine("constructor");
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Start the timer to run the ProcesoDeFondo method every 15 minutes
        _timer = new Timer(ProcesoDeFondo, null, TimeSpan.Zero, TimeSpan.FromMinutes(waitTime));
        return Task.CompletedTask;
    }

    private void ProcesoDeFondo(object state)
    {
        Console.WriteLine("proceso de fondo");
        // Crear scope para acceder a TrackrContext
        using (var scope = _scopeFactory.CreateScope())
        {
            Console.WriteLine("inside scope");
            var context = scope.ServiceProvider.GetRequiredService<TrackrContext>();

            // Obtener DistributedLock para RecordatorioTomas
            // Este registro de la base de datos nos permite conocer 
            // el momento en el que fue ejecutado por ultima vez el proceso
            var dLock = context.DistributedLocks
                                .Where(dl => dl.Resource == "RecordatorioTomas")
                                .FirstOrDefault();

            // Actualizar DistributedLock solo en el siguiente cuarto de hora y ejecutar proceso
            if(dLock != null && EsSiguienteCuartoHora(dLock.LastUpdated)){
                context.DistributedLocks.Attach(dLock);
                dLock.LastUpdated = DateTime.UtcNow;
                context.SaveChanges();
                
                ActualizarContador(context);
                CrearTratamientoTomas(context);

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
        DateTime nextQuarterDateTime = lastUpdated.AddMinutes(waitTime - (lastUpdated.Minute % waitTime));
        Console.WriteLine("EsSiguienteCuartoHora");
        Console.WriteLine(DateTime.UtcNow);
        Console.WriteLine(nextQuarterDateTime);
        return DateTime.UtcNow > nextQuarterDateTime;
    }

    // Procesos
    public void ActualizarContador(TrackrContext context){
        var dl = context.DistributedLocks.Where(dl => dl.Resource != "RecordatorioTomas").FirstOrDefault();
        int x = 0;
        if(dl != null){
            context.DistributedLocks.Attach(dl);
            int.TryParse(dl.Resource, out x);
            dl.Resource = (x+1).ToString();
            Console.WriteLine(dl.Resource);
            context.SaveChanges();
        }
    }

    public void CrearTratamientoTomas(TrackrContext context){
        Console.WriteLine("Crear tratamiento tomas");
        // DateTime now = DateTime.Now;
        DateTime now = DateTime.Today.AddHours(21).AddMinutes((48 + DateTime.Now.TimeOfDay.Minutes - 38) % 60);
        Console.WriteLine(now);
        int currentDay = ((int)now.DayOfWeek + 6) % 7; // Monday = 0, ..., Saturday = 5, Sunday = 6
        TimeSpan currentTime = now.TimeOfDay;
        TimeSpan timeIn15Minutes = currentTime + TimeSpan.FromMinutes(15);
        DateTime time15MinutesAgo = now - TimeSpan.FromMinutes(15);

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
            
            Console.WriteLine(toma.IdTratamientoRecordatorio);
            context.TratamientoToma.Add(toma);
        }

        context.SaveChanges();

    }
}
