﻿namespace TrackrAPI.Dtos.GestionExamen
{
    public class ExamenReactivoExcelDto
    {
        public int IdExamen { get; set; }
        public int IdReactivo { get ; set;}
        public string Pregunta { get; set; } = null!;
        public string RespuestaAlumno { get; set; } = null!;
        public bool NecesitaRevision { get; set; } 
        public bool PreguntaAbierta { get; set; }
    }
}
