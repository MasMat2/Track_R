using Microsoft.EntityFrameworkCore;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Dashboard
{
  public class UsuarioWidgetRepository : Repository<UsuarioWidget>, IUsuarioWidgetRepository
  {
    public UsuarioWidgetRepository(TrackrContext context) : base(context)
    {
    }

    public IEnumerable<UsuarioWidget> ConsultarPorUsuario(int usuarioId)
    {
    //   return context.UsuarioWidget
    //     .Where(uw => uw.IdUsuario == usuarioId)
    //     .Include(uw => uw.IdUsuarioNavigation)
    //     .Include(uw => uw.IdWidgetNavigation);
        return new List<UsuarioWidget>();
    }
  }
}