namespace TrackrAPI.Dtos.SAT
{
    public class CertificadoLocacionDto
    {
        public int IdCertificadoLocacion { get; set; }
        public int IdLocacion { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }

        public string ArchivoBase64 { get; set; }
        public string TipoMime { get; set; }
    }
}
