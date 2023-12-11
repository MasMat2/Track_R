namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteMuestrasGridDTO
    {
        public int IdEntidadEstructuraTablaValor { get; set; }
        public int IdEntidadEstructura { get; set; }
        public DateTime? FechaMuestra { get; set; }
        public bool FueraDeRango { get; set; }
        public List<ExpedienteMuestrasRegistroDTO>? Registro { get; set; }



    }
}
