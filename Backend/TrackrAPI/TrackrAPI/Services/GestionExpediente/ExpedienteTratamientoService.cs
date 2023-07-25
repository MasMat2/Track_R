using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.GestionExpediente;
public class ExpedienteTratamientoService
{
    private readonly IExpedienteTratamientoRepository expedienteTratamientoRepository;
    
    public ExpedienteTratamientoService(IExpedienteTratamientoRepository expedienteTratamientoRepository)
    {
        this.expedienteTratamientoRepository = expedienteTratamientoRepository;
    }

    public ExpedienteTratamientoDto? Consultar(int idExpedienteTratamiento)
    {
        var expedienteTratamiento =  expedienteTratamientoRepository.Consultar(idExpedienteTratamiento);

        if (expedienteTratamiento is null)
        {
            return null;
        }

        var expedienteTratamientoDto = new ExpedienteTratamientoDto
        {

            IdExpedienteTratamiento = expedienteTratamiento.IdExpedienteTratamiento,
            Farmaco = expedienteTratamiento.Farmaco,
            Cantidad = expedienteTratamiento.Cantidad,
            Unidad = expedienteTratamiento.Unidad,
            Indicaciones = expedienteTratamiento.Indicaciones,
            IdPadecimiento = expedienteTratamiento.IdPadecimiento,
            FechaRegistro = expedienteTratamiento.FechaRegistro,

        };

        return expedienteTratamientoDto;
    }       

    public IEnumerable<ExpedienteTratamientoGridDTO> ConsultarPorUsuario(int idUsuario)
    {
        var expedienteTratamientos = expedienteTratamientoRepository.ConsultarPorUsuario(idUsuario);

        var expedienteTratamientosDto = expedienteTratamientos.Select(et => new ExpedienteTratamientoGridDTO
            {
                IdExpedienteTratamiento = et.IdExpedienteTratamiento,
                Farmaco = et.Farmaco,
                Cantidad = et.Cantidad,
                Unidad = et.Unidad,
                Indicaciones = et.Indicaciones,
                Padecimiento = et.IdPadecimiento.ToString() ?? string.Empty,
                FechaRegistro = et.FechaRegistro
            });

            return expedienteTratamientosDto;
    }

}

