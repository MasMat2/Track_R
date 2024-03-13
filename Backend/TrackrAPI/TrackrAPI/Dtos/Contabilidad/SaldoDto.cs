using System.Collections.Generic;

namespace TrackrAPI.Dtos.Contabilidad
{
    public class SaldoDto {

        public SaldoDto() {
        }

        public int Mes { get; set; }
        public int Anio { get; set; }
        public int IdJerarquia { get; set; }
        public int? IdVersionPoliza { get; set; }
        public bool EsPresupuesto { get; set; }

        public List<JerarquiaEstructuraDto> JerarquiaEstructuraDtoList { get; set; }

    }
}
