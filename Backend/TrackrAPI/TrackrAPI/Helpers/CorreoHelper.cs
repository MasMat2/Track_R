
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace TrackrAPI.Helpers;

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

    public async Task Enviar(Correo correo)
    {
        try
        {

            if (string.IsNullOrWhiteSpace(correo.Receptor) || correo.Receptor.Trim().StartsWith('.'))
                return;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Alias, Emisor));
            message.To.Add(MailboxAddress.Parse(correo.Receptor));
            message.Subject = correo.Asunto;

            if (correo.EsMensajeHtml)
            {
                var builder = new BodyBuilder { HtmlBody = correo.Mensaje };

                foreach (var imagen in correo.Imagenes)
                    builder.LinkedResources.Add(imagen);

                message.Body = builder.ToMessageBody();
            }
            else
            {
                message.Body = new TextPart("plain") { Text = correo.Mensaje };
            }

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(Host, Puerto, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(Emisor, Contrasena);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

        }
        catch (Exception)
        {
            throw new CdisException("Ocurrió un error al enviar el correo");
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

        using (var client = new SmtpClient())
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
