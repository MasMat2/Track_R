namespace TrackrAPI.Dtos.GestionEntidad
{
    public class ValoresPorCampoGridDTO
    {
        public string unidadMedida { get; set; }

        public IEnumerable<ValoresHistogramaDTO> valores { get; set; }
    }
}
