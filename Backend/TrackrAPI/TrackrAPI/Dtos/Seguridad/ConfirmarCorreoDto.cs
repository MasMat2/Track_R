namespace TrackrAPI.Dtos.Seguridad
{
    public class ConfirmarCorreoDto
    {
        public string Correo { get; set; }
        public string Token { get; set; }
        public int IdUsuario { get; set; }
    }
}
