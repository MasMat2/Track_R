namespace TrackrAPI.Dtos.Seguridad
{
    public class UsuarioRolGridDto
    {
        public int IdUsuarioRol { get; set; }
        public int IdRol { get; set; }
        public int? IdCuentaContable { get; set; }
        public int? IdConcepto { get; set; }
        public string NombreRol { get; set; }
        public string ClaveRol { get; set; }
        public string CuentaContable { get; set; }
        public string Concepto { get; set; }
    }
}
