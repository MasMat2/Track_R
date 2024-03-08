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
       return context.UsuarioWidget
         .Where(uw => uw.IdUsuario == usuarioId)
         .Include(uw => uw.IdUsuarioNavigation)
         .Include(uw => uw.IdWidgetNavigation).ToList();
    }

    public UsuarioWidget ConsultarSeleccionadoPorClave(int usuarioId, string clave)
    {
       return context.UsuarioWidget
         .Where(uw => uw.IdUsuario == usuarioId && uw.IdWidgetNavigation.Clave.Equals(clave))
         .Include(uw => uw.IdUsuarioNavigation)
         .Include(uw => uw.IdWidgetNavigation).FirstOrDefault();
    }

    public void EliminarPorUsuario(int usuarioId)
    {
            var widgets = context.UsuarioWidget.Where(w => w.IdUsuario == usuarioId);
            context.UsuarioWidget.RemoveRange(widgets);
    }

   }
}