using System.Collections.Generic;

namespace TrackrAPI.Dtos.Catalogo
{
    public class ConceptoDto
    {
        public int IdConcepto { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string TipoMovimiento { get; set; }
        public int? IdCuentaContable { get; set; }
        public bool? Operativo { get; set; }
        public string NombreUnidadSat { get; set; }
        public string NombreProductoServicioSat { get; set; }
        public int? IdSatProductoServicio { get; set; }
        public int? IdSatUnidad { get; set; }
        public int? IdTipoAuxiliar { get; set; }
        public List<int> IdsTipoConcepto { get; set; }
        public string ClaveTipoCuentaContable { get; set; }
        public bool RequiereAuxiliar { get; set; }
        public int IdCompania { get; set; }
    }
}
