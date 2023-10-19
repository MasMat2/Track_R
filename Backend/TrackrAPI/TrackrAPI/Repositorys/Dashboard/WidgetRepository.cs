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
}