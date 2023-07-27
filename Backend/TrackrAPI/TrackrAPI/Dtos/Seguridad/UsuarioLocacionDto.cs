namespace TrackrAPI.Dtos.Seguridad
{
    public class UsuarioLocacionDto
    {
        public int IdUsuarioLocacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdPerfil { get; set; }
        public int IdCompania { get; set; }
        public int IdLocacion { get; set; }
        public string Compania { get; set; }
        public string Locacion { get; set; }
        public string Perfil { get; set; }
    }
}
