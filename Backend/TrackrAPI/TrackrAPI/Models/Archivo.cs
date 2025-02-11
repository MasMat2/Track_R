﻿using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Archivo
    {
        public Archivo()
        {
            ChatMensaje = new HashSet<ChatMensaje>();
            ExpedienteEstudio = new HashSet<ExpedienteEstudio>();
            ExpedienteTratamiento = new HashSet<ExpedienteTratamiento>();
        }

        public int IdArchivo { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaRealizacion { get; set; }
        public byte[]? Archivo1 { get; set; }
        public string ArchivoTipoMime { get; set; } = null!;
        public string? ArchivoNombre { get; set; }
        public int? IdUsuario { get; set; }
        public bool? EsFotoPerfil { get; set; }
        public string? ArchivoUrl { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<ChatMensaje> ChatMensaje { get; set; }
        public virtual ICollection<ExpedienteEstudio> ExpedienteEstudio { get; set; }
        public virtual ICollection<ExpedienteTratamiento> ExpedienteTratamiento { get; set; }
    }
}
