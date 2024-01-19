using TrackrAPI.Dtos.Padecimientos;
using TrackrAPI.Helpers;
using TrackrAPI.Services.GestionEntidad;
using TrackrAPI.Services.GestionExpediente;

namespace TrackrAPI.Services.Padecimientos;

public class PadecimientoService
{
    private readonly EntidadEstructuraService _entidadEstructuraService;
    private readonly ExpedientePadecimientoService _expedientePadecimientoService;

    public PadecimientoService(
        EntidadEstructuraService entidadEstructuraService,
        ExpedientePadecimientoService expedientePadecimientoService)
    {
        _entidadEstructuraService = entidadEstructuraService;
        _expedientePadecimientoService = expedientePadecimientoService;
    }

    public IEnumerable<PacientesPorPadecimientoDTO> ConsultarPacientesPorPadecimiento(int idDoctor , int idCompania)
    {
        var padecimientos = ConsultarPadecimientos();
        var expedientes = _expedientePadecimientoService.Consultar(idDoctor , idCompania);

        return padecimientos.Select(p => new PacientesPorPadecimientoDTO(
            p.IdPadecimiento,
            p.NombrePadecimiento,
            expedientes.Count(e => e.IdPadecimiento == p.IdPadecimiento)
        ));
    }

    public IEnumerable<PadecimientoDTO> ConsultarPadecimientos()
    {
        return _entidadEstructuraService.ConsultarPorEntidad(GeneralConstant.ClaveEntidadPadecimiento)
            .Where(e => e.Tabulacion == true)
            .Select(p => new PadecimientoDTO(
                p.IdEntidadEstructura,
                p.Nombre ?? string.Empty
            ));
    }
}