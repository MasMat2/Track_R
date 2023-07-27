using TrackrAPI.Dtos.GestionEgresos;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.GestionEgresos
{
    public interface ITipoAuxiliarRepository : IRepository<TipoAuxiliar>
    {
        public IEnumerable<TipoAuxiliarDto> ConsultarParaSelector();
        public IEnumerable<TipoAuxiliarDto> ConsultarPorTipoCuentaParaSelector(int idTipoCuentaContable);
        public IEnumerable<TipoAuxiliar> ConsultarTodos();
        public TipoAuxiliar ConsultarPorClave(string clave);
        public TipoAuxiliar Consultar(int idTipoAuxiliar);
        public TipoAuxiliar ConsultarDependencias(int idTipoAuxiliar);
    }
}
