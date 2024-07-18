using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Dashboard;

public class WidgetRepository : Repository<Widget>, IWidgetRepository
{
    public WidgetRepository(TrackrContext context) : base(context)
    {
    }

    public Widget ConsultarPorPadecimiento(int idPadecimiento)
    {
        return context.Widget.Where(w => w.IdPadecimiento == idPadecimiento).FirstOrDefault();
    }

    public IEnumerable<TipoWidget> ConsultarTipo()
    {
        return context.TipoWidget.ToList();
    }

    public IEnumerable<Widget> ConsultarTodos()
    {
        return context.Widget.ToList();
    }

    public Widget consultarPorClave(string clave)
    {
        return context.Widget.Where(w => w.Clave.Equals(clave)).FirstOrDefault();
    }

    public IEnumerable<Widget> ConsultarDefault()
    {
        return context.Widget.Where(w => GeneralConstant.WidgetsDefault.Contains(w.Clave));
    }


}