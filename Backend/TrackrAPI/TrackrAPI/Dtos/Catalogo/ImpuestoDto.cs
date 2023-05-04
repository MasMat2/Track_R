namespace TrackrAPI.Dtos.Catalogo
{
    public class ImpuestoDto
    {
        public int IdImpuesto { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PorcentajeNeto { get; set; }
        public int? IdCuentaContableRedondeo { get; set; }
    }
}
