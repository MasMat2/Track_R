﻿using TrackrAPI.Models;

namespace TrackrAPI.Dtos.Seguridad
{
    public class ExpedienteWrapper
    {
        public UsuarioDto paciente { get; set; }
        public ExpedienteTrackr expediente { get; set; }
        public Domicilio domicilio { get; set; }

        public IEnumerable<ExpedientePadecimientoDTO> padecimientos { get; set; }
    }
}
