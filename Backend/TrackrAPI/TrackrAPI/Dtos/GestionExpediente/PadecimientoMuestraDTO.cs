using TrackrAPI.Dtos.GestionEntidad;

namespace TrackrAPI.Dtos.GestionExpediente
{
    public class PadecimientoMuestraDTO
    {
        public int IdPadecimiento { get; set; }
        public string NombrePadecimiento { get; set; }
        public IEnumerable<SeccionMuestraDTO> SeccionMuestraDTOs { get; set; }
    }
}
