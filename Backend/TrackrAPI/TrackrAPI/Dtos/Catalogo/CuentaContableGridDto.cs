using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Catalogo
{
    public class CuentaContableGridDto
    {
        public int IdCuentaContable { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Reconciliatoria { get; set; }
        public bool RecibeMovimientos { get; set; }
        public bool Auxiliar { get; set; }
        public bool PartidaAbierta { get; set; }
        public bool Automatica { get; set; }
        public int? IdSubtipoCuentaContable { get; set; }
        public int? IdTipoCuentaContable { get; set; }
        public int? IdCompania { get; set; }
        public int IdAgrupadorCuentaContable { get; set; }
        public string TipoCuentaNombre { get; set; }
        public string SubtipoCuentaNombre { get; set; }

        public CuentaContableGridDto(int idCuentaContable, string numero, string nombre, string descripcion, bool reconciliatoria,
            bool recibeMoviminetos, bool auxiliar, bool partidaAbierta, bool automatica, string tipoCuentaNombre, string subtipoCuentaNombre,
            int? idSubtipoCuentaContable, int? idTipoCuentaContable, int? idCompania)
        {
            this.IdCuentaContable = idCuentaContable;
            this.Numero = numero;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Reconciliatoria = reconciliatoria;
            this.RecibeMovimientos = recibeMoviminetos;
            this.Auxiliar = auxiliar;
            this.PartidaAbierta = partidaAbierta;
            this.Automatica = automatica;
            this.TipoCuentaNombre = tipoCuentaNombre;
            this.SubtipoCuentaNombre = subtipoCuentaNombre;
            this.IdTipoCuentaContable = idTipoCuentaContable;
            this.IdSubtipoCuentaContable = idSubtipoCuentaContable;
            this.IdCompania = idCompania;
        }

        public CuentaContableGridDto() { }

    }
}
