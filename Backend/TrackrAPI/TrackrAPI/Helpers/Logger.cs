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
                string filePath = Path.Combine(env.ContentRootPath, "Log/Errores/" + today.Date.Day + "-" + today.Date.Month + "-" + today.Date.Year + ".txt");
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
    }
}
