using TrackrAPI.Models;
using System.Linq;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class CertificacoLocacionRepository : Repository<CertificadoLocacion>, ICertificadoLocacionRepository
    {
        public CertificacoLocacionRepository(TrackrContext context) : base(context)
        {
            this.context = context;
        }

        public CertificadoLocacion ConsultarPorLocacion(int idLocacion)
        {
            return context.CertificadoLocacion
                    .Where(cl => cl.IdLocacion == idLocacion)
                    .FirstOrDefault();
        }
    }
}
