using TrackrAPI.Dtos.General;
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;
public class ClasificacionPreguntaRepository : Repository<ClasificacionPregunta>, IClasificacionPreguntaRepository
{
    public ClasificacionPreguntaRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public ClasificacionPreguntaFormularioDto Consultar(int idClasificacionPregunta)
    {
        return context.ClasificacionPregunta
        .Where(cp => cp.IdClasificacionPregunta == idClasificacionPregunta)
        .Select(cp => new ClasificacionPreguntaFormularioDto
        {
            IdClasificacionPregunta = cp.IdClasificacionPregunta,
            Nombre = cp.Nombre,
            Estatus = cp.Estatus,
            Clave = cp.Clave,
        })
        .FirstOrDefault();


    }

    public IEnumerable<ClasificacionPreguntaGridDto> ConsultarParaGrid()
    {
        return context.ClasificacionPregunta
        .Select(cp => new ClasificacionPreguntaGridDto
        {
            IdClasificacionPregunta = cp.IdClasificacionPregunta,
            Nombre = cp.Nombre,
            Estatus = cp.Estatus,
            Clave = cp.Clave

        }).ToList();
    }

    public void Eliminar(int idClasificacionPregunta)
    {
        var cp = context.ClasificacionPregunta
        .Where(cp => cp.IdClasificacionPregunta == idClasificacionPregunta).FirstOrDefault();

        if (cp == null)
        {
            throw new CdisException("La informacion no se encuentra registrada");

        }

        context.ClasificacionPregunta.Attach(cp);
        cp.Estatus = false;
        context.SaveChanges();
    }

    public IEnumerable<SimpleSelectorDto> ConsultarTodosParaSelector()
    {
        return context.ClasificacionPregunta
        .Where(p => p.Estatus == true)
        .Select(p => new SimpleSelectorDto
        {
            Id = p.IdClasificacionPregunta,
            Value = p.Nombre
        })
        .ToList();
    }

}
