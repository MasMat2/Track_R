using System.Net.Mail;

namespace TrackrAPI.Helpers
{
    public partial class Correo
    {
        public string Receptor { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public bool EsMensajeHtml { get; set; }

        public List<Attachment> Adjuntos { get; set; }

        //imagenes que se muestran en el cuerpo y no como adjuntos
        public List<LinkedResource> Imagenes { get; set; }


        public Correo() { }

        public Correo(string receptor, string asunto, string mensaje, bool esMensajeHtml,List <Attachment> adjuntos,List<LinkedResource> imagenes)
        {
            Receptor = receptor;
            Asunto = asunto;
            Mensaje = mensaje;
            EsMensajeHtml = esMensajeHtml;
            Adjuntos = adjuntos;
            Imagenes = imagenes;
        }
    }

}
