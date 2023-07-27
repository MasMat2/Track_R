namespace TrackrAPI.Dtos.Catalogo
{
    public class ImpuestoGridDto
    {
        public int IdImpuesto { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string CuentaContableNumeroNombre { get; set; }
        public bool PuedeEditar { get; set; }
    }
}
