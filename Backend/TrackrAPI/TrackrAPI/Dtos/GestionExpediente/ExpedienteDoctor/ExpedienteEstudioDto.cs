	public class ExpedienteEstudioDTO
	{
		public int IdExpedienteEstudio { get; set; }
		public int IdExpediente { get; set; }
		public string Nombre { get; set; }
		public DateTime? FechaRealizacion { get; set; }
		public string ArchivoBase64 { get; set; }
		public string ArchivoTipoMime { get; set; }
	}    