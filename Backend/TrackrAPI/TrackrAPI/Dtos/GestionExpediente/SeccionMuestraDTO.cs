namespace TrackrAPI.Dtos.GestionExpediente
{
    public class SeccionMuestraDTO
    {
        public string ClaveCampo { get; set; }
        public string NombreSeccionCampo { get; set; }
        public List<SeccionCampoMuestraDTO> SeccionesCampo { get; set; }
    }
}
