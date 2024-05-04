using TrackrAPI.Models;

namespace TrackrAPI.Dtos.Catalogo
{
    public class ConceptoFormularioDto
    {
        public int IdConcepto { get; set; }
        public int IdCompania { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public List<int> IdsTipoConcepto { get; set; }
        public string TipoMovimiento { get; set; }
        public int? IdCuentaContable { get; set; }
        public bool? Operativo { get; set; }
        public int? IdSatProductoServicio { get; set; }
        public int? IdSatUnidad { get; set; }
        public int? IdTipoAuxiliar { get; set; }

        public ConceptoFormularioDto()
        {

        }

        //public ConceptoFormularioDto(Concepto concepto)
        //{
        //    IdConcepto = concepto.IdConcepto;
        //    IdCompania = (int)concepto.IdCompania;
        //    Clave = concepto.Clave;
        //    Nombre = concepto.Nombre;
        //    IdsTipoConcepto = new();
        //    TipoMovimiento = concepto.TipoMovimiento;
        //    IdCuentaContable = concepto.IdCuentaContable;
        //    Operativo = concepto.Operativo;
        //    IdSatProductoServicio = concepto.IdSatProductoServicio;
        //    IdSatUnidad = concepto.IdSatUnidad;
        //    IdTipoAuxiliar = concepto.IdTipoAuxiliar;
        //}
    }
}
