namespace TrackrAPI.Dtos.GestionExpediente.ExpedienteDoctor
{
    public class ExpedienteDoctorConImagenPerfilDTO
    {
        public int IdUsuarioDoctor { get; set; }
        public int IdExpedienteDoctor { get; set; }
        public int IdExpediente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Ambito { get; set; } = string.Empty;
        public string Hospital { get; set; } = string.Empty;
        public string TipoMime { get; set; }
        public string ImagenBase64 { get; set; }


    }
}
