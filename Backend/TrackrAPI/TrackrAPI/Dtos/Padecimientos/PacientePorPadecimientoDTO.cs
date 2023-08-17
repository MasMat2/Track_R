namespace TrackrAPI.Dtos.Padecimientos;

public record PacientesPorPadecimientoDTO(
    int IdPadecimiento, // IdEntidadEstructura
    string NombrePadecimiento,
    int CantidadPacientes
);