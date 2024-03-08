using TrackrAPI.Repositorys;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TrackrAPI.Dtos.Seguridad;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class RolAccesoRepository: Repository<RolAcceso>, IRolAccesoRepository
    {
        public RolAccesoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public RolAcceso Consultar(int idRolAcceso)
        {
            return context.RolAcceso
                .Where(ra => ra.IdRolAcceso == idRolAcceso)
                .FirstOrDefault();
        }

        public IEnumerable<RolAcceso> ConsultarTodosParaSelector()
        {
            return context.RolAcceso
                .OrderBy(r => r.Nombre).ToList();
        }

        public RolAcceso ConsultarPorClave(string clave)
        {
            return context.RolAcceso
                   .Where(r => r.Clave == clave)
                   .FirstOrDefault();
        }
    }
}
