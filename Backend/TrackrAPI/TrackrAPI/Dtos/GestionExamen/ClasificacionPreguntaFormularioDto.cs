namespace RoadisAPI.Dtos.GestionExamen
{
    public class ClasificacionPreguntaFormularioDto
    {
        public int IdClasificacionPregunta { get; set; }
        public string? Nombre { get; set; }
        public bool? Estatus { get; set; }
        public string? Clave { get; set; }
    }
}