using TrackrAPI.Dtos.Padecimientos;
using TrackrAPI.Models;

namespace TrackrAPI.Dtos.GestionExpediente;

public class PadecimientoUsuarioDTO
{
    public string? IconoClase { get; set; }
    public int IdPadecimiento { get; set; }
    public string NombrePadecimiento { get; set; }
    public List<VariableDTO> Variables { get; set; }
    public int? IdWidget { get; set; }
    public string DescripcionWidget { get; set; }
    public int TomasTomadas { get; set; }
    public int TomasTotales { get; set; }
}

