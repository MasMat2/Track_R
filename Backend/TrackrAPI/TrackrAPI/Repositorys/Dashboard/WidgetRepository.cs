using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Dashboard;

public class WidgetRepository : Repository<Widget>, IWidgetRepository
{
    public WidgetRepository(TrackrContext context) : base(context)
    {
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


}