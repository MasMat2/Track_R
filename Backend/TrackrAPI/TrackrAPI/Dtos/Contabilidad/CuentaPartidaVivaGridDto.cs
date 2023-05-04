namespace TrackrAPI.Dtos.Contabilidad
{
    public class CuentaPartidaVivaGridDto
    {
        public int IdCuentaContable { get; set; }
        public string Descripcion { get; set; }
        public decimal Saldo { get; set; }
        public int? Mes { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }


    }
}
