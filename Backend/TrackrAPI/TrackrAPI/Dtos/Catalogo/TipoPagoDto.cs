namespace TrackrAPI.Dtos.Catalogo
{
    public class TipoPagoDto
    {
        public int IdTipoPago { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int? IdConcepto { get; set; }
    }
}
