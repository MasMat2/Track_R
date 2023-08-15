using System;
using System.Collections.Generic;
namespace TrackrAPI.Dtos.GestionExpediente
{
    public class ExpedienteEstudioFormularioCapturaDTO
    {
        public int IdExpedienteEstudio { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdExpediente { get; set; }
        public DateTime? FechaRealizacion { get; set; }
        public byte[] Archivo { get; set; } = null!;
        public string ArchivoTipoMime { get; set; } = null!;
        public string ArchivoNombre { get; set; } = null!;
    }
}




   
       

 

