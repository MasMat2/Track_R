using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.DTOs.Dashboard;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Dashboard;

public class WidgetRepository : Repository<Widget>, IWidgetRepository
{
    public WidgetRepository(TrackrContext context) : base(context)
    {
    }

    public IEnumerable<UsuarioPadecimientosDTO> ConsultarPorUsuario(int idUsuario)
    {  
        var padecimientosUsuario = context.ExpedienteTrackr
                                    .Include( et => et.ExpedientePadecimiento)
                                        .ThenInclude( ep => ep.IdPadecimientoNavigation)
                                        .ThenInclude( ep => ep.IdSeccionNavigation)
                                        .ThenInclude( ep => ep.SeccionCampo)
                                    .Where( ep => ep.IdUsuario == idUsuario)
                                    .Select( ee => new {
                                        idExpediente = ee.IdExpediente,
                                        expedientePadecimientos = ee.ExpedientePadecimiento.Select(padecimiento => new {
                                            idPadecimiento = padecimiento.IdPadecimiento,
                                            nombrePadecimiento = padecimiento.IdPadecimientoNavigation.Nombre
                                        }),
                                    }) 
                                    .ToList(); 
                                    
        var variablesPadecimiento = context.EntidadEstructura
                    .Include(ee => ee.IdEntidadEstructuraPadreNavigation)
                    .Include(ee => ee.IdEntidadNavigation)
                    .Include(ee => ee.IdSeccionNavigation)
                    .Include(ee => ee.EntidadEstructuraTablaValor)
                        .Where(ee => ee.IdEntidadEstructuraPadre != null )
                        .GroupBy(ee => ee.IdEntidadEstructuraPadre)
                    .Select(group => new
                    {
                        idEntidadEstructura = group.First().IdEntidadEstructura,
                        idPadecimiento = group.Key,
                        nombrePadecimiento = group.First().IdEntidadEstructuraPadreNavigation.Nombre,
                        idEntidad = group.First().IdEntidadEstructuraPadreNavigation.IdEntidad,
                        descripcionWidget = group.First().IdEntidadEstructuraPadreNavigation.IdTipoWidgetNavigation.Descripcion,
                        idWidgetEntidad = group.First().IdEntidadEstructuraPadreNavigation.IdTipoWidget,
                        iconoEntidad = group.First().IdEntidadEstructuraPadreNavigation.IdIconoNavigation.Clase,
                        idSeccion = group.First().IdSeccionNavigation.IdSeccion,
                        nombreSeccion = group.First().IdSeccionNavigation.Nombre,
                        seccionClave = group.First().IdSeccionNavigation.Clave,
                        variables = group.SelectMany(ee => ee.IdSeccionNavigation.SeccionCampo
                            .Select(sC => new VariablesPadecimientoDTO
                            {
                                VariableClave = sC.Clave,
                                Descripcion = sC.Descripcion,
                                MostrarDashboard = sC.MostrarDashboard,
                                IconoClase = sC.IdIconoNavigation.Clase,
                                ValorVariable = 55
                            }))
                            .Take(2)
                            .ToList()
                    })
                    .ToList();


            var infoWidgetUsuario = padecimientosUsuario
            .SelectMany(usuario => usuario.expedientePadecimientos, (usuario, padecimiento) => new
            {
                    idExpediente = usuario.idExpediente,
                    idPadecimiento = padecimiento.idPadecimiento,
                    iconoClase = variablesPadecimiento.FirstOrDefault( vp => vp.idPadecimiento == padecimiento.idPadecimiento).iconoEntidad ,
                    nombrePadecimiento = padecimiento.nombrePadecimiento,
                    idWidgetPadecimiento = variablesPadecimiento
                                            .FirstOrDefault( vp => vp.idPadecimiento == padecimiento.idPadecimiento).idWidgetEntidad ,
                    descripcion = variablesPadecimiento.FirstOrDefault( vp => vp.idPadecimiento == padecimiento.idPadecimiento).descripcionWidget, 
                    variables = variablesPadecimiento.FirstOrDefault(vp => vp.idPadecimiento == padecimiento.idPadecimiento)?.variables
            })
            .GroupBy(result => result.idExpediente )
            .Select(group => new UsuarioPadecimientosDTO
            {
                IdExpediente = group.Key,
                Secciones = group.Select(item => new PadecimientoDTO
                {
                    IdPadecimiento = item.idPadecimiento,
                    NombrePadecimiento = item.nombrePadecimiento,
                    Variables = item.variables,
                    IdWidget = item.idWidgetPadecimiento, 
                    DescripcionWidget = item.descripcion, 
                    IconoClase = item.iconoClase 
                }).ToList()
            })
            .ToList();

          return infoWidgetUsuario ;
    }

    public IEnumerable<TipoWidget> ConsultarTipo()
    {
        return context.TipoWidget.ToList();
    }
}