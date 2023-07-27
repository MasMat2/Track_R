namespace TrackrAPI.Dtos.GestionCaja
{
    public class CajaDto
    {
        public int IdCaja { get; set; }
        public string Nombre { get; set; }
        public int IdHospital { get; set; }
        public string Descripcion { get; set; }
        public bool Automatica { get; set; }
        public int? IdUsuarioResponsable { get; set; }
        public int? IdTipoActivo { get; set; }
        public int? IdCuentaContable { get; set; }
        public string NombreUsuarioResponsable { get; set; }
        public int? IdMoneda { get; set; }
        public int? IdCuentaContableAutomatica { get; set; }
    }
}
