namespace CanalDistAPI.Dtos.Seguridad
{
    public class JerarquiaAccesoEstructuraDto
    {
        public int IdJerarquiaAccesoEstructura { get; set; }
        public int? IdJerarquiaAcceso { get; set; }
        public int? IdAcceso { get; set; }
        public int? IdJerarquiaAccesoEstructuraPadre { get; set; }
        public int? IdAccesoPadre { get; set; }
    }
}
