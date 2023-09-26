using TrackrAPI.Models;

namespace TrackrAPI.Dtos.GestionExpediente;

    public class UsuarioPadecimientosDTO
    {
          public int IdExpediente { get; set; }
          public List<PadecimientoDTO> Secciones { get; set; }
    }

