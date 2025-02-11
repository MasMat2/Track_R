﻿using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EntidadEstructura
    {
        public EntidadEstructura()
        {
            EntidadEstructuraTablaValor = new HashSet<EntidadEstructuraTablaValor>();
            EntidadEstructuraValor = new HashSet<EntidadEstructuraValor>();
            ExpedientePadecimiento = new HashSet<ExpedientePadecimiento>();
            ExpedienteTratamiento = new HashSet<ExpedienteTratamiento>();
            InverseIdEntidadEstructuraPadreNavigation = new HashSet<EntidadEstructura>();
            Notificacion = new HashSet<Notificacion>();
            Widget = new HashSet<Widget>();
        }

        public int IdEntidadEstructura { get; set; }
        public string? Nombre { get; set; }
        public string? Clave { get; set; }
        public bool? Tabulacion { get; set; }
        public int IdEntidad { get; set; }
        public int? IdSeccion { get; set; }
        public int? IdEntidadEstructuraPadre { get; set; }
        public int? IdIcono { get; set; }
        public int? IdTipoWidget { get; set; }
        public bool? EsAntecedente { get; set; }

        public virtual EntidadEstructura? IdEntidadEstructuraPadreNavigation { get; set; }
        public virtual Entidad IdEntidadNavigation { get; set; } = null!;
        public virtual Icono? IdIconoNavigation { get; set; }
        public virtual Seccion? IdSeccionNavigation { get; set; }
        public virtual TipoWidget? IdTipoWidgetNavigation { get; set; }
        public virtual ICollection<EntidadEstructuraTablaValor> EntidadEstructuraTablaValor { get; set; }
        public virtual ICollection<EntidadEstructuraValor> EntidadEstructuraValor { get; set; }
        public virtual ICollection<ExpedientePadecimiento> ExpedientePadecimiento { get; set; }
        public virtual ICollection<ExpedienteTratamiento> ExpedienteTratamiento { get; set; }
        public virtual ICollection<EntidadEstructura> InverseIdEntidadEstructuraPadreNavigation { get; set; }
        public virtual ICollection<Notificacion> Notificacion { get; set; }
        public virtual ICollection<Widget> Widget { get; set; }
    }
}
