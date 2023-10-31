
namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteMuestrasRegistroDTO
    {

        public int IdEntidadEstructuraTablaValor { get; set; }
        public int Numero { get; set; }
        public int IdEntidadEstructura { get; set; }
        public string ClaveCampo { get; set; }
        public string? Valor { get; set; }
        public int? IdTabla { get; set; }
        public bool? FueraDeRango { get; set; }
        public DateTime? FechaMuestra { get; set; }

    }
}
