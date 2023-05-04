namespace Trackr.Dtos.PedidoEnLinea
{
    public class FlujoDto
    {
        public int IdFlujo { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool? EsDefault { get; set; }
        public int? IdTipoFlujo { get; set; }
        public int? IdRol { get; set; }
        public bool PermiteModificar { get; set; }
    }
}
