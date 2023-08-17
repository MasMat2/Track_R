namespace TrackrAPI.Dtos.GestionEntidad
{
    public class TablaValorMuestraDTO
    {
        public string ClaveCampo { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public bool FueraDeRango { get; set; }
    }
}
