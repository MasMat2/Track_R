using TrackrAPI.Models;

namespace TrackrAPI.Dtos.GestionExpediente;

    public class PadecimientoDTO
    {
        /*public string? IconoClase {get; set;} */
        public int IdPadecimiento { get; set; }
        public string NombrePadecimiento { get; set; }
        public List<VariablesPadecimientoDTO> Variables { get; set; }
        /*public int? IdWidget { get; set; } */
        public string DescripcionWidget { get; set; }
    }

