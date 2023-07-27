namespace TrackrAPI.Helpers
{
    public partial class Correo
    {
        public string Receptor { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public bool EsMensajeHtml { get; set; }

        public Correo() { }

        public Correo(string receptor, string asunto, string mensaje, bool esMensajeHtml)
        {
            Receptor = receptor;
            Asunto = asunto;
            Mensaje = mensaje;
            EsMensajeHtml = esMensajeHtml;
        }
    }

}
