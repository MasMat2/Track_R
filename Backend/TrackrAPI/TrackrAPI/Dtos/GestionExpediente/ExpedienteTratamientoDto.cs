namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteTratamientoDto
    {
        public int IdExpediente { get; set; }
        public string Farmaco { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Indicaciones { get; set; }
        public string Padecimiento { get; set; }
        public int IdPadecimiento { get; set; }
        public int IdUsuarioDoctor { get; set; }
        public string? ImagenBase64 { get; set; }
        public bool RecordatorioActivo { get; set; }
        public bool[] DiaSemana { get; set; }
        public TimeSpan[] Horas { get; set; }        
        public DateTime[] Bitacora { get; set; }
    }
}
