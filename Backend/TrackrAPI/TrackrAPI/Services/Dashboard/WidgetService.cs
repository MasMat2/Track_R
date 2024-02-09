using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Dtos.Padecimientos;
using TrackrAPI.DTOs.Dashboard;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Dashboard;
using TrackrAPI.Repositorys.GestionEntidad;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Services.GestionExpediente;

namespace TrackrAPI.Services.Dashboard;

public class WidgetService
{
    private readonly IWidgetRepository _widgetRepository;
    private readonly IUsuarioWidgetRepository _usuarioWidgetRepository;
    private readonly IExpedientePadecimientoRepository _expedientePadecimientoRepository;
    private readonly IEntidadEstructuraRepository _entidadEstructuraRepository;
    private readonly IEntidadEstructuraTablaValorRepository _entidadEstructuraTablaValorRepository;
    private readonly ExpedienteTrackrService _expedienteTrackrService;

    public WidgetService(IWidgetRepository widgetRepository,
                         IExpedientePadecimientoRepository expedientePadecimientoRepository,
                         IEntidadEstructuraRepository entidadEstructuraRepository,
                         IUsuarioWidgetRepository usuarioWidgetRepository,
                         IEntidadEstructuraTablaValorRepository entidadEstructuraTablaValorRepository,
                         ExpedienteTrackrService expedienteTrackrService)

    {
        _widgetRepository = widgetRepository;
        _expedientePadecimientoRepository = expedientePadecimientoRepository;
        _entidadEstructuraRepository = entidadEstructuraRepository;
        _usuarioWidgetRepository = usuarioWidgetRepository;
        _entidadEstructuraTablaValorRepository = entidadEstructuraTablaValorRepository;
        _expedienteTrackrService = expedienteTrackrService;
    }

    public IEnumerable<UsuarioPadecimientosDTO> Consultar(int idUsuario)
    {
        var padecimientoUsuario = _expedientePadecimientoRepository.ConsultarPorUsuario(idUsuario);

        var padecimientoUsuarioSeleccionado = padecimientoUsuario.
            Where(padecimiento => EsWidgetSeleccionado(idUsuario, padecimiento.clavePadecimiento)).ToList();

        var variablesPadecimiento = ValoresVariablesPadecimiento(idUsuario);

        var infoWidgetUsuario = padecimientoUsuarioSeleccionado
        .Select(usuarioNuevo => new
        {
            idExpediente = usuarioNuevo.IdExpediente,
            idPadecimiento = usuarioNuevo.IdPadecimiento,
            iconoClase = variablesPadecimiento.FirstOrDefault(vp => vp.IdPadecimiento == usuarioNuevo.IdPadecimiento).IconoEntidad,
            nombrePadecimiento = usuarioNuevo.NombrePadecimiento,
            idWidgetPadecimiento = variablesPadecimiento.FirstOrDefault(vp => vp.IdPadecimiento == usuarioNuevo.IdPadecimiento).IdWidgetEntidad,
            descripcion = variablesPadecimiento.FirstOrDefault(vp => vp.IdPadecimiento == usuarioNuevo.IdPadecimiento).DescripcionWidget,
            variables = variablesPadecimiento.FirstOrDefault(vp => vp.IdPadecimiento == usuarioNuevo.IdPadecimiento)?.Variables.ToList().Any() == false
                                ? new List<VariableDTO>() { new VariableDTO() {
                                        VariableClave = "No hay registros clínicos",
                                        Descripcion = "No hay registros clínicos",
                                        MostrarDashboard = true,
                                        IconoClase = "fas fa-exclamation-triangle",
                                        ValorVariable = "-",
                                        unidadMedida = ""
                                     }}
                                : variablesPadecimiento.FirstOrDefault(vp => vp.IdPadecimiento == usuarioNuevo.IdPadecimiento).Variables.ToList()
        })
        .GroupBy(result => result.idExpediente)
        .Select(group => new UsuarioPadecimientosDTO
        {
            IdExpediente = group.Key,
            Secciones = group.Select(item =>
            {
                var (tomasTotales, tomasTomadas) = _expedienteTrackrService.TomasPadecimientoTotalesHoy(idUsuario, item.idPadecimiento);
                return new PadecimientoUsuarioDTO
                {
                    IdPadecimiento = item.idPadecimiento,
                    NombrePadecimiento = item.nombrePadecimiento,
                    Variables = item.variables,
                    IdWidget = item.idWidgetPadecimiento,
                    DescripcionWidget = item.descripcion,
                    IconoClase = item.iconoClase,
                    TomasTomadas = tomasTomadas,
                    TomasTotales = tomasTotales
                };
            })
            .ToList()
        })
        .ToList();


        return infoWidgetUsuario;
    }

    public IEnumerable<PadecimientoVariablesDTO> ValoresVariablesPadecimiento(int idUsuario)
    {
        var variablesPadecimiento = _entidadEstructuraRepository.ValoresVariablesPadecimiento(idUsuario);

        var aux = variablesPadecimiento.GroupBy(ee => ee.IdEntidadEstructuraPadre)
                .Select(group => new PadecimientoVariablesDTO
                {
                    IdEntidadEstructura = group.First().IdEntidadEstructura,
                    IdPadecimiento = group.Key,
                    NombrePadecimiento = group.First().IdEntidadEstructuraPadreNavigation.Nombre,
                    IdEntidad = group.First().IdEntidadEstructuraPadreNavigation.IdEntidad,
                    DescripcionWidget = group.First().IdEntidadEstructuraPadreNavigation.IdTipoWidgetNavigation.Descripcion,
                    IdWidgetEntidad = group.First().IdEntidadEstructuraPadreNavigation.IdTipoWidget,
                    IconoEntidad = group.First().IdEntidadEstructuraPadreNavigation.IdIconoNavigation.Clase,
                    IdSeccion = group.First().IdSeccionNavigation.IdSeccion,
                    NombreSeccion = group.First().IdSeccionNavigation.Nombre,
                    SeccionClave = group.First().IdSeccionNavigation.Clave,
                    Variables = group.SelectMany(ee => ee.IdSeccionNavigation.SeccionCampo
                                                        .Where(sC => _entidadEstructuraTablaValorRepository.ExisteValorEnEntidadEstructura(idUsuario, sC.Clave))
                                                        .Select(sC =>
                                                        {
                                                            return new VariableDTO
                                                            {
                                                                VariableClave = sC.Clave,
                                                                Descripcion = sC.Descripcion,
                                                                MostrarDashboard = sC.MostrarDashboard,
                                                                IconoClase = sC.IdIconoNavigation?.Clase,
                                                                unidadMedida = sC.IdDominioNavigation.UnidadMedida,
                                                                ValorVariable = _entidadEstructuraTablaValorRepository.ConsultarUltimoValor(idUsuario, sC.Clave)
                                                            };
                                                        })
                                                        .Take(2)
                                                        ).ToList()
                }).ToList();

        return aux;
    }

    public IEnumerable<Widget> consultarTodos()
    {
        return _widgetRepository.ConsultarTodos();
    }

    public IEnumerable<TipoWidget> ConsultarTipo()
    {
        return _widgetRepository.ConsultarTipo();
    }

    private bool EsWidgetSeleccionado(int usuarioId, string clave)
    {
        var widget = _usuarioWidgetRepository.ConsultarSeleccionadoPorClave(usuarioId, clave);

        return widget != null;
    }


    public IEnumerable<Widget> WidgetsDisponiblesPorUsuario(int idUsuario)
    {
        var widgetsDefault = _widgetRepository.ConsultarDefault();
        var widgetsPadecimientosUsuario = _expedientePadecimientoRepository.ConsultarWidgetsSeguimientoPadecimientoPorUsuario(idUsuario);

        //Widgets predeterminados + widgets de los padecimientos que tenga el usuario:
        return widgetsDefault.Concat(widgetsPadecimientosUsuario);
    }


}