namespace TrackrAPI.Dtos.Seguridad
{
    public class UsuarioExpedienteGridDTO
    {
        public int IdExpedienteTrackr { get; set; }
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string DoctorAsociado { get; set; }
        public IEnumerable<string> Patologias { get; set; }
        public string? ImagenBase64 { get; set; }
        public string? TipoMime { get; set; }
        public int DosisNoTomadas { get; set; }
        public int VariablesFueraRango { get; set; }
        public int PresionAsistolica { get; set; }
        public int PresionSistolica { get; set; }
        public string Edad { get; set; }
    }
}
