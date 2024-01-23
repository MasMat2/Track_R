using System;
using System.Collections.Generic;

namespace TrackrAPI.Dtos.Seguridad
{
    public class AsistenteDoctorDto
    {
       public int IdAsistenteDoctor { get; set; }
       public int IdUsuario { get; set; }
       public string NombreAsistente { get; set; } = string.Empty;
       public string ImagenBase64 { get; set; }
       public string ImagenTipoMime { get; set; }
    }
}
