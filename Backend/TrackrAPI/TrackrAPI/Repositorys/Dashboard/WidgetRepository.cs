using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Dashboard;

public class WidgetRepository : Repository<Widget>, IWidgetRepository
{
    public WidgetRepository(TrackrContext context) : base(context)
    {
    }

    public IEnumerable<Widget> Consultar()
    {
        return context.Widget;
    }

    public Widget? ConsultarPorClave(string clave)
    {
        return context.Widget
            .FirstOrDefault(w => w.Clave == clave);
    }
}