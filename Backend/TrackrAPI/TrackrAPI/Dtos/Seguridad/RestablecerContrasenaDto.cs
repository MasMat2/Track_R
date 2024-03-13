namespace TrackrAPI.Dtos.Seguridad
{
    public class RestablecerContrasenaDto
    {
        public int IdUsuario { get; set; }

        public string Correo { get; set; }

        public string CorreoPersonal { get; set; }

        public string ContrasenaActualizada { get; set; }

        public string Contrasena { get; set; }

        public string Clave { get; set; }

    }
}
