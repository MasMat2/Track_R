namespace TrackrAPI.Dtos.PedidoEnLinea
{
    public class CarritoDto
    {
        public int IdCarrito { get; set; }
        public int IdPresentacion { get; set; }
        public int IdCompania { get; set; }
        public string NombrePresentacion { get; set; }
        public string DescripcionPresentacion { get; set; }
        public double Cantidad { get; set; }
        public string Comentarios { get; set; }
        public double? Precio { get; set; }
        public byte[] ImagenPresentacion { get; set; }
    }
}
