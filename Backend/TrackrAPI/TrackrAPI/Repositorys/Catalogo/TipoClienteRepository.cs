using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class TipoClienteRepository : Repository<TipoCliente>, ITipoClienteRepository {

        public TipoClienteRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public List<TipoCliente> ConsultarPorCompania(int idCompania) {

                var tipoClienteList = from tc in context.TipoCliente where tc.IdCompania == idCompania select tc;
                return tipoClienteList.ToList();

        }
    }
}
