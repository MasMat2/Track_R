using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.GestionExpediente;

public class ExpedienteRecomendacionService
{
    private readonly IExpedienteRecomendacionRepository _expedienteRecomendacionRepository;

    public ExpedienteRecomendacionService(IExpedienteRecomendacionRepository expedienteRecomendacionRepository)
    {
        this._expedienteRecomendacionRepository = expedienteRecomendacionRepository;
    }

    public IEnumerable<ExpedienteRecomendacionGridDTO> ConsultarPorUsuario(int idUsuario)
    {
        return _expedienteRecomendacionRepository.ConsultarPorUsuario(idUsuario);
    }

    public ExpedienteRecomendacionDTO ConsultarPorId(int idExpedienteRecomendacion)
    {
        var recomendacionModel = _expedienteRecomendacionRepository.ConsultarPorId(idExpedienteRecomendacion);

        return new ExpedienteRecomendacionDTO
        {
            Descripcion = recomendacionModel.Descripcion,
            Fecha = recomendacionModel.FechaRealizacion,
            IdDoctor = recomendacionModel.IdUsuarioDoctor,
            IdExpediente = recomendacionModel.IdExpediente,
            IdExpedienteRecomendacion = recomendacionModel.IdExpedienteRecomendaciones
        };
    }


    public void Agregar(ExpedienteRecomendacionDTO expedienteRecomendacionDTO)
    {
        var recomendacion = new ExpedienteRecomendaciones
        {
            Descripcion = expedienteRecomendacionDTO.Descripcion ?? string.Empty,
            FechaRealizacion = DateTime.Now,
            IdExpediente = expedienteRecomendacionDTO.IdExpediente,
            IdUsuarioDoctor = expedienteRecomendacionDTO.IdDoctor
        };
        _expedienteRecomendacionRepository.Agregar(recomendacion);
    }

    public void Editar(ExpedienteRecomendacionDTO expedienteRecomendacionDTO)
    {
        var recomendacion = _expedienteRecomendacionRepository.ConsultarPorId(expedienteRecomendacionDTO.IdExpedienteRecomendacion);
        recomendacion.Descripcion = expedienteRecomendacionDTO.Descripcion;
        Console.WriteLine(recomendacion);
        _expedienteRecomendacionRepository.Editar(recomendacion);
    }

    public void Eliminar(int idExpedienteRecomendacion)
    {
        var recomendacion = _expedienteRecomendacionRepository.ConsultarPorId(idExpedienteRecomendacion);
        _expedienteRecomendacionRepository.Eliminar(recomendacion);
    }
}