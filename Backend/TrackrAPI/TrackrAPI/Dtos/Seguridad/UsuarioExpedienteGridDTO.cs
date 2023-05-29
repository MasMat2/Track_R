namespace TrackrAPI.Dtos.Seguridad
{
    public class UsuarioExpedienteGridDTO
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public List<string> Patologias { get; set; }
        public string? ImagenBase64 { get; set; }
        public string? TipoMime { get; set; }
        public int Glucosa { get; set; }
        public int PresionAsistolica { get; set; }
        public int PresionSistolica { get; set; }
    }
}
