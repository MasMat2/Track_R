namespace TrackrAPI.Dtos.GestionExamen
{
    public class RespuestasExcelDto
    {
        public IEnumerable<string> Preguntas { get; set; }
        public IEnumerable<IGrouping<int, ExamenReactivoExcelDto>> Respuestas { get; set; }
    }
}
