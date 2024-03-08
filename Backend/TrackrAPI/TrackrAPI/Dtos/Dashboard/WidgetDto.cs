namespace TrackrAPI.DTOs.Dashboard;

public record WidgetDto(
    int IdWidget,
    int IdExpediente,
    int IdUsuario,
    int IdPadecimiento,
    string Descripcion,
    string TipoWidget,
    bool? MostrarDashboard
);

