namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteMuestrasGridDTO
    {
        public DateTime? FechaMuestra { get; set; }
        public bool FueraDeRango { get; set; }
        public List<ExpedienteMuestrasRegistroDTO>? Registro { get; set; }



    }
}
