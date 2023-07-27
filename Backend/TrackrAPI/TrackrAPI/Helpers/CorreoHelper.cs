using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace TrackrAPI.Helpers
{
    public class CorreoHelper
    {
        private string Host;
        private int Puerto;
        private string Emisor;
        private string Contrasena;
        private string Alias;

        public CorreoHelper(IConfiguration config)
        {
            var smtp = config.GetSection("SMTP");
            if (smtp != null)
            {
                Host = smtp.GetSection("Host").Value;
                Puerto = StringToIntPort(smtp.GetSection("Puerto").Value);
                Emisor = smtp.GetSection("Emisor").Value;
                Contrasena = smtp.GetSection("Contrasena").Value;
                Alias = smtp.GetSection("Alias").Value;
            }
        }

        public void Enviar(Correo correo)
        {
            if (!string.IsNullOrWhiteSpace(correo.Receptor))
            {
                if (correo.Receptor.Trim().StartsWith('.'))
                {
                    return;
                }

                SmtpClient cliente = new SmtpClient(Host, Puerto);
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(Emisor, Alias);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.To.Add(correo.Receptor);
                mailMessage.Body = correo.Mensaje;
                mailMessage.Subject = correo.Asunto;
                mailMessage.IsBodyHtml = correo.EsMensajeHtml;
                cliente.EnableSsl = true;
                cliente.UseDefaultCredentials = false;
                cliente.Credentials = new System.Net.NetworkCredential(Emisor, Contrasena);
                cliente.Send(mailMessage);
            }
        }

        public void EnviarAdjuntos(Correo correo, List<Attachment> adjuntos)
        {
            if (!string.IsNullOrWhiteSpace(correo.Receptor))
            {
                if (correo.Receptor.Trim().StartsWith('.'))
                {
                    return;
                }

                SmtpClient cliente = new SmtpClient(Host, Puerto);
                MailMessage mailMessage = new MailMessage();

                foreach (Attachment adjunto in adjuntos)
                {
                    if (adjunto != null) mailMessage.Attachments.Add(adjunto);
                }

                string receptores = correo.Receptor.Replace(";", ",");

                mailMessage.From = new MailAddress(Emisor, Alias);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.To.Add(receptores);
                mailMessage.Body = correo.Mensaje;
                mailMessage.Subject = correo.Asunto;
                mailMessage.IsBodyHtml = correo.EsMensajeHtml;
                cliente.EnableSsl = true;
                cliente.UseDefaultCredentials = false;
                cliente.Credentials = new System.Net.NetworkCredential(Emisor, Contrasena);

                cliente.Send(mailMessage);
            }
        }

        public async void EnviarAdjuntosAsync(Correo correo, List<Attachment> adjuntos)
        {
            if (!string.IsNullOrWhiteSpace(correo.Receptor))
            {
                if (correo.Receptor.Trim().StartsWith('.'))
                {
                    return;
                }

                MailMessage mailMessage = new MailMessage();

                foreach (Attachment adjunto in adjuntos)
                {
                    if (adjunto != null) mailMessage.Attachments.Add(adjunto);
                }

                string receptores = correo.Receptor.Replace(";", ",");

                mailMessage.From = new MailAddress(Emisor, Alias);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.To.Add(receptores);
                mailMessage.Body = correo.Mensaje;
                mailMessage.Subject = correo.Asunto;
                mailMessage.IsBodyHtml = correo.EsMensajeHtml;

                using (var smtpClient = new SmtpClient(Host, Puerto))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(Emisor, Contrasena);
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }

        private int StringToIntPort(string puerto)
        {
            int i = 0;
            if (!Int32.TryParse(puerto, out i))
            {
                i = -1;
            }

            return i;
        }
    }
}
