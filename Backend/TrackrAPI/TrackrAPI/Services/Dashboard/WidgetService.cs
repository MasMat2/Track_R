using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.DTOs.Dashboard;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Dashboard;
using TrackrAPI.Repositorys.GestionEntidad;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.Dashboard;

public class WidgetService
{
    private readonly IWidgetRepository _widgetRepository;
    private readonly IExpedientePadecimientoRepository _expedientePadecimientoRepository;
    private readonly IEntidadEstructuraRepository _entidadEstructuraRepository;

    public WidgetService(IWidgetRepository widgetRepository,
                         IExpedientePadecimientoRepository expedientePadecimientoRepository,
                         IEntidadEstructuraRepository entidadEstructuraRepository)
    {
        _widgetRepository = widgetRepository;
        _expedientePadecimientoRepository = expedientePadecimientoRepository;
        _entidadEstructuraRepository = entidadEstructuraRepository;
    }

    public IEnumerable<UsuarioPadecimientosDTO> Consultar(int idUsuario)
    {
        var padecimientoUsuario = _expedientePadecimientoRepository.ConsultarPorUsuario(idUsuario);

        var variablesPadecimiento = _entidadEstructuraRepository.ValoresVariablesPadecimiento(idUsuario);

        var infoWidgetUsuario = padecimientoUsuario
        .Select(usuarioNuevo => new
        {
            idExpediente = usuarioNuevo.IdExpediente,
            idPadecimiento = usuarioNuevo.IdPadecimiento,
            iconoClase = variablesPadecimiento.FirstOrDefault(vp => vp.IdPadecimiento == usuarioNuevo.IdPadecimiento).IconoEntidad,
            nombrePadecimiento = usuarioNuevo.NombrePadecimiento,
            idWidgetPadecimiento = variablesPadecimiento.FirstOrDefault(vp => vp.IdPadecimiento == usuarioNuevo.IdPadecimiento).IdWidgetEntidad,
            descripcion = variablesPadecimiento.FirstOrDefault(vp => vp.IdPadecimiento == usuarioNuevo.IdPadecimiento).DescripcionWidget,
            variables = variablesPadecimiento.FirstOrDefault(vp => vp.IdPadecimiento == usuarioNuevo.IdPadecimiento)?.Variables.ToList()
        })
        .GroupBy(result => result.idExpediente)
        .Select(group => new UsuarioPadecimientosDTO
        {
            IdExpediente = group.Key,
            Secciones = group.Select(item => new PadecimientoUsuarioDTO
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


        return infoWidgetUsuario;
    }

    public IEnumerable<TipoWidget> ConsultarTipo()
    {
        return _widgetRepository.ConsultarTipo();
    }
}