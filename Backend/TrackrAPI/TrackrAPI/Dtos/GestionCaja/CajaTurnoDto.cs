using System;

namespace TrackrAPI.Dtos.GestionCaja
{
    public class CajaTurnoDto
    {
        public int IdCajaTurno { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaContable { get; set; }
        public double FondoCaja { get; set; }
        public DateTime FechaAlta { get; set; }
        public double? MontoEsperado { get; set; }
        public double? MontoIngresado { get; set; }
        public int IdCaja { get; set; }
        public int IdTurno { get; set; }
        public int IdUsuario { get; set; }
        public bool TurnoCerrado { get; set; }
        public string Descripcion { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCaja { get; set; }
        public string Turno { get; set; }
    }
}
