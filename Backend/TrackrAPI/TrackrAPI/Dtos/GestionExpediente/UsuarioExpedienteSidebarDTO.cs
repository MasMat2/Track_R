using System.Collections.Generic;

namespace TrackrAPI.Dtos.GestionExpediente
{
    public class UsuarioExpedienteSidebarDTO
    {
        public int idUsuario { get; set; }
        public string nombreCompleto { get; set; }
        public string? tipoMime { get; set; }
        public string? ImagenBase64 { get; set; }
        public int idGenero { get; set; }
        public string edad { get; set; }
        public string? colonia { get; set; }
        public string? ciudad { get; set; }
        public string? estado { get; set; }
        public IEnumerable<ExpedienteSidebarDTO>? padecimientos { get; set; }
    }
}

