using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Reactivo
    {
        public Reactivo()
        {
            ExamenReactivo = new HashSet<ExamenReactivo>();
        }

        public int IdReactivo { get; set; }
        public int IdAsignatura { get; set; }
        public int IdNivelExamen { get; set; }
        public string? Clave { get; set; }
        public string? Pregunta { get; set; }
        public byte[]? Imagen { get; set; }
        public string? ImagenTipoMime { get; set; }
        public string? ImagenNombre { get; set; }
        public string? Respuesta { get; set; }
        public string? RespuestaCorrecta { get; set; }
        public bool? NecesitaRevision { get; set; }
        public DateTime? FechaAlta { get; set; }
        public bool? Estatus { get; set; }

        public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;
        public virtual NivelExamen IdNivelExamenNavigation { get; set; } = null!;
        public virtual ICollection<ExamenReactivo> ExamenReactivo { get; set; }
    }
}
