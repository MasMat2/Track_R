namespace TrackrAPI.Services.Notificaciones;
public class NotificacionPacienteServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public NotificacionPacienteServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public NotificacionPacienteService CreateScopedInstance()
    {
        using var scope = _serviceProvider.CreateScope();
        return scope.ServiceProvider.GetRequiredService<NotificacionPacienteService>();
    }
}
