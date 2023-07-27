using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Distribucion
{
    public class TipoEmpleadoRepository : Repository<TipoEmpleado>, ITipoEmpleadoRepository {

        public TipoEmpleadoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public TipoEmpleado Consultar(int idTipoEmpleado) {

                var tipoEmpleado = from te in context.TipoEmpleado where te.IdTipoEmpleado == idTipoEmpleado select te;
                return tipoEmpleado.FirstOrDefault();

        }

        public List<TipoEmpleado> ConsultarPorCompania(int idCompania) {

                var tipoEmpleadoList = from te in context.TipoEmpleado where te.IdCompania == idCompania select te;
                return tipoEmpleadoList.ToList();

        }

        public TipoEmpleado Consultar(string clave) {

                var tipoEmpleado = from te in context.TipoEmpleado where te.Clave == clave select te;
                return tipoEmpleado.FirstOrDefault();

        }

    }
}
