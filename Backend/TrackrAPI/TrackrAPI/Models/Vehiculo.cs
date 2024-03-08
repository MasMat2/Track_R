using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            ExpedienteAdministrativoViaje = new HashSet<ExpedienteAdministrativoViaje>();
            VehiculoMantenimiento = new HashSet<VehiculoMantenimiento>();
        }

        public int IdVehiculo { get; set; }
        public string Nombre { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public int Modelo { get; set; }
        public string Placas { get; set; } = null!;
        public int KilometrajeActual { get; set; }
        public int KilometrajeInicial { get; set; }
        public int KilometrajeAcumulado { get; set; }
        public int IdTipoVehiculo { get; set; }
        public int KilometrosParaMantenimiento { get; set; }
        public int IdCompania { get; set; }
        public int? IdAuxiliar { get; set; }
        public string? NumeroPermisoSct { get; set; }
        public string? NombreAseguradora { get; set; }
        public string? NumeroPolizaSeguro { get; set; }
        public bool TieneRemolque { get; set; }
        public string? PlacasRemolque { get; set; }
        public int? IdTipoPermisoTransporte { get; set; }
        public int? IdConfiguracionAutotransporte { get; set; }
        public int? IdTipoRemolque { get; set; }

        public virtual Auxiliar? IdAuxiliarNavigation { get; set; }
        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual ConfiguracionAutotransporte? IdConfiguracionAutotransporteNavigation { get; set; }
        public virtual TipoPermisoTransporte? IdTipoPermisoTransporteNavigation { get; set; }
        public virtual TipoRemolque? IdTipoRemolqueNavigation { get; set; }
        public virtual TipoVehiculo IdTipoVehiculoNavigation { get; set; } = null!;
        public virtual ICollection<ExpedienteAdministrativoViaje> ExpedienteAdministrativoViaje { get; set; }
        public virtual ICollection<VehiculoMantenimiento> VehiculoMantenimiento { get; set; }
    }
}
