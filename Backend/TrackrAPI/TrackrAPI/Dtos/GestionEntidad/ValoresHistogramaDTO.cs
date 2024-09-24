namespace TrackrAPI.Dtos.GestionEntidad
{
    public class ValoresHistogramaDTO
    {
        public DateTime? FechaMuestra {  get; set; }
        public decimal Valor { get; set; }
        public bool? FueraDeRango { get; set; }
    }
}
