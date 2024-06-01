using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Web;

namespace TrackrAPI.Helpers
{
    public static class Logger
    {

        public static void WriteError(Exception ex, IWebHostEnvironment env)
        {
            try
            {
                DateTime today = DateTime.Now;
                string filePath = Path.Combine(env.ContentRootPath, "Log/Errores/" + today.Date.Year + "-" + today.Date.Month + "-" + today.Date.Day + ".txt");
                FileInfo fi = new FileInfo(filePath);

                if (!fi.Directory.Exists)
                {
                    Directory.CreateDirectory(fi.DirectoryName);
                }

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Fecha: " + today.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Error: " + ex.Message);
                        writer.WriteLine("StackTrace: " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
            }
            catch (Exception) { }
        }

    public static void NotificarPorSlack(Exception ex)
        {
            try
            {
                var urlWithAccessToken = "https://hooks.slack.com/services/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";
                var client = new SlackClient(urlWithAccessToken);

                client.PostMessage(username: null,
                           text: ex?.Message,
                           errorMessage: ex?.StackTrace);
            }
            catch (Exception exd) { 
                Console.WriteLine("Error al enviar notificación por Slack: " + exd.Message);
            }
        }
    }
}
