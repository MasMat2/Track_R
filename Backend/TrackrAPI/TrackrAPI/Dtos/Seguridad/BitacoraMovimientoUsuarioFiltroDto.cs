using System;

namespace CanalDistAPI.Dtos.Seguridad
{
    public class BitacoraMovimientoUsuarioFiltroDto
    {
        public int? IdUsuario { get; set; }
        public int? IdLocacion { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
    }
}
