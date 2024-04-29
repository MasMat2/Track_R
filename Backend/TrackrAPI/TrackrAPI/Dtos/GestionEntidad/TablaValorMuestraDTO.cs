namespace TrackrAPI.Dtos.GestionEntidad
{
    public class TablaValorMuestraDTO
    {
        public int IdSeccionVariable { get; set; }
        public string Valor { get; set; } = string.Empty;
        public bool FueraDeRango { get; set; }
        public DateTime FechaMuestra { get; set; }
    }
}
