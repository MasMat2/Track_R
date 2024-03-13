using TrackrAPI.Dtos.Seguridad;

namespace TrackrAPI.Dtos.PedidoEnLinea
{
    public class FlujoDetalleDto
    {
        public int IdFlujoDetalle { get; set; }
        public int IdFlujo { get; set; }
        public int? IdRol { get; set; }
        public int? IdArea { get; set; }
        public int IdEstatusPedido { get; set; }
        public int Orden { get; set; }
        public string NombreRol { get; set; }
        public string NombreEstatusPedido { get; set; }
        public string ClaveEstatusPedido { get; set; }
        public bool? SolicitarResponsable { get; set; }
        public string NombreFlujo { get; set; }

        // Responsables del flujo detalle
        public int[] IdsUsuariosResponsables { get; set; }
        public List<UsuarioDto> Responsables { get; set; }
    }
}
