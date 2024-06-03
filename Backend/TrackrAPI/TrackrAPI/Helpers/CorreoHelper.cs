
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using TrackrAPI.Services.Sftp;


namespace TrackrAPI.Helpers;

public class CorreoHelper
{
    private readonly string Host;
    private readonly int Puerto;
    private readonly string Emisor;
    private readonly string Contrasena;
    private readonly string Alias;
    private readonly bool UseSSL;
    private readonly string AuthName;

    public CorreoHelper(IConfiguration config)
    {
        
        var smtp = config.GetSection("SMTP");
        if (smtp != null)
        {
            Host = smtp.GetSection("Host").Value;
            Puerto = StringToIntPort(smtp.GetSection("Puerto").Value);
            Emisor = smtp.GetSection("Address").Value;
            Contrasena = smtp.GetSection("AuthPassword").Value;
            Alias = smtp.GetSection("DisplayName").Value;
            UseSSL = bool.Parse(smtp.GetSection("UseSSL").Value);
            AuthName = smtp.GetSection("AuthName").Value;
        }
    }

    public void Enviar(Correo correo, AlternateView? htmlView = null)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(correo.Receptor) || correo.Receptor.Trim().StartsWith('.'))
                return;

            MailMessage mail = new();
            mail.To.Add(new MailAddress(correo.Receptor));
            mail.From = new MailAddress(Emisor, Alias);
            mail.Subject = correo.Asunto;
            mail.Body = correo.Mensaje;
            mail.IsBodyHtml = correo.EsMensajeHtml;

            if (htmlView != null)
            {
                mail.AlternateViews.Add(htmlView);
            }

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Host, Puerto)
            {
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(Emisor, Contrasena),
                Port = Puerto,
                Host = Host,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = UseSSL
            };

            client.Send(mail);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al enviar correo: " + ex.Message);
        }
    }

    public void EnviarAdjuntos(Correo correo, List<MimePart> adjuntos)
    {
        if (string.IsNullOrWhiteSpace(correo.Receptor) || correo.Receptor.Trim().StartsWith('.'))
        {
            return;
        }

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(Alias, Emisor));

        var receptores = correo.Receptor.Replace(";", ",");
        foreach (var receptor in receptores.Split(','))
        {
            message.To.Add(new MailboxAddress("", receptor.Trim()));
        }

        message.Subject = correo.Asunto;

        var bodyBuilder = new BodyBuilder
        {
            TextBody = correo.Mensaje,
            HtmlBody = correo.EsMensajeHtml ? correo.Mensaje : null
        };

        foreach (var adjunto in adjuntos)
        {
            bodyBuilder.Attachments.Add(adjunto);
        }

        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            client.Connect(Host, Puerto, SecureSocketOptions.StartTls);
            client.Authenticate(Emisor, Contrasena);
            client.Send(message);
            client.Disconnect(true);
        }
    }


    private int StringToIntPort(string puerto)
    {
        int i = 0;
        if (!int.TryParse(puerto, out i))
        {
            i = -1;
        }

        return i;
    }
}
