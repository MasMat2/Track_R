using TrackrAPI.Dtos.GestionEgresos;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.GestionEgresos
{
    public class TipoAuxiliarRepository : Repository<TipoAuxiliar>, ITipoAuxiliarRepository
    {
        public TipoAuxiliarRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }
        public IEnumerable<TipoAuxiliarDto> ConsultarParaSelector()
        {
            return context.TipoAuxiliar
                .Select(ta => new TipoAuxiliarDto
                {
                    IdTipoAuxiliar = ta.IdTipoAuxiliar,
                    Descripcion = ta.Descripcion,
                    Clave = ta.Clave,
                    Nombre = ta.Clave + " - " + ta.Descripcion
                }).ToList();
        }

        public IEnumerable<TipoAuxiliarDto> ConsultarPorTipoCuentaParaSelector(int idTipoCuentaContable)
        {
            return context.TipoAuxiliar
                .Where(ta => ta.IdTipoCuentaContable == idTipoCuentaContable)
                .Select(ta => new TipoAuxiliarDto
                {
                    IdTipoAuxiliar = ta.IdTipoAuxiliar,
                    Descripcion = ta.Descripcion,
                    Clave = ta.Clave,
                    Nombre = ta.Clave + " - " + ta.Descripcion
                }).ToList();
        }

        public IEnumerable<TipoAuxiliar> ConsultarTodos()
        {
            return context.TipoAuxiliar.ToList();
        }

        public TipoAuxiliar ConsultarPorClave(string clave)
        {
            return context.TipoAuxiliar
                .Where(ta => ta.Clave == clave)
                .FirstOrDefault();
        }

        public TipoAuxiliar Consultar(int idTipoAuxiliar)
        {
            return context.TipoAuxiliar
                .Where(ta => ta.IdTipoAuxiliar == idTipoAuxiliar)
                .FirstOrDefault();
        }

        public TipoAuxiliar ConsultarDependencias(int idTipoAuxiliar)
        {
            return context.TipoAuxiliar
                .Where(ta => ta.IdTipoAuxiliar == idTipoAuxiliar)
                .Select(ta => new TipoAuxiliar
                {
                    Auxiliar = ta.Auxiliar
                }).FirstOrDefault();
        }
    }
}
