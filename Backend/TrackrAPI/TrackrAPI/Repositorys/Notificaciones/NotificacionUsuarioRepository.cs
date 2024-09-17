using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using MimeTypes;
using Org.BouncyCastle.Utilities;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Archivos;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Repositorys.Notificaciones;

public class NotificacionUsuarioRepository : Repository<NotificacionUsuario>, INotificacionUsuarioRepository
{
    private readonly IExpedienteConsumoMedicamentoRepository _expedienteConsumoMedicamentoRepository;
    private readonly IArchivoRepository _archivoRepository;
    private readonly UsuarioService _usuarioService;
    public NotificacionUsuarioRepository(TrackrContext context,
                                         IExpedienteConsumoMedicamentoRepository expedienteConsumoMedicamentoRepository, 
                                         IArchivoRepository archivoRepository,
                                         UsuarioService usuarioService) : base(context)
    {
        _expedienteConsumoMedicamentoRepository = expedienteConsumoMedicamentoRepository;
        _archivoRepository = archivoRepository;
        _usuarioService = usuarioService;
    }

    private IQueryable<NotificacionUsuario> ConsultarPorUsuario(int idUsuario)
    {
        const int limite = 10;

        return context.NotificacionUsuario
            .Where(n => n.IdUsuario == idUsuario)
            .OrderByDescending(n => n.IdNotificacionNavigation.FechaAlta)
            .Take(limite);
    }
    private IQueryable<NotificacionUsuario> ConsultarPorUsuario(List<int> idsDoctor)
    {
        const int limite = 10;

        return context.NotificacionUsuario
            .Where(n => idsDoctor.Contains(n.IdUsuario))
            .OrderByDescending(n => n.IdNotificacionNavigation.FechaAlta)
            .Take(limite);
    }

    public IEnumerable<NotificacionPacienteDTO> ConsultarPorPaciente(int idUsuario)
    {
        return ConsultarPorUsuario(idUsuario)
            .Select(nu => new NotificacionPacienteDTO(
                nu.IdNotificacionUsuario,
                nu.IdNotificacion,
                nu.IdUsuario,
                nu.IdNotificacionNavigation.Titulo,
                nu.IdNotificacionNavigation.Mensaje,
                nu.IdNotificacionNavigation.ComplementoMensaje,
                nu.IdNotificacionNavigation.FechaAlta,
                nu.Visto,
                nu.IdNotificacionNavigation.IdTipoNotificacion,
                nu.IdNotificacionNavigation.IdChat
            ));
    }

    public async Task<IEnumerable<NotificacionDoctorDTO>> ConsultarPorDoctor(List<int> idsDoctor)
    {
        var notificacionesUsuario = ConsultarPorUsuario(idsDoctor)
            .Include(n => n.IdNotificacionNavigation.NotificacionDoctor)
            .Include(n => n.IdNotificacionNavigation.IdTipoNotificacionNavigation)
            .Include(n => n.IdNotificacionNavigation)
            .ThenInclude(n => n.IdPersonaNavigation)
            .AsEnumerable();
    
        var notificacionesUsuarioList = notificacionesUsuario.ToList();
    
        var notificacionesDoctorDto = new List<NotificacionDoctorDTO>();
    
        foreach (var nu in notificacionesUsuarioList)
        {
            var imagenUsuario = await ObtenerImagenUsuario((int)nu.IdNotificacionNavigation.IdPersona);
            notificacionesDoctorDto.Add(new NotificacionDoctorDTO(
                nu.IdNotificacionUsuario,
                nu.IdNotificacion,
                nu.IdUsuario,
                nu.IdNotificacionNavigation.Titulo,
                nu.IdNotificacionNavigation.Mensaje,
                nu.IdNotificacionNavigation.ComplementoMensaje,
                nu.IdNotificacionNavigation.FechaAlta,
                nu.Visto,
                nu.IdNotificacionNavigation.IdTipoNotificacion,
                nu.IdNotificacionNavigation.NotificacionDoctor.FirstOrDefault()?.IdPaciente ?? 0,
                imagenUsuario,
                nu.IdNotificacionNavigation.IdChat,
                nu.IdNotificacionNavigation.IdTipoNotificacionNavigation.Clave
            ));
        }
        
        return notificacionesDoctorDto;
    }

    public void MarcarComoVistas(List<int> idNotificacionUsuario, bool tomaTomada)
    {
        var notificacionesUsuario = context.NotificacionUsuario
            .Where(n => idNotificacionUsuario.Contains(n.IdNotificacionUsuario))
            .ToList();

        foreach (var notificacionUsuario in notificacionesUsuario)
        {
            notificacionUsuario.Visto = true;
            var toma = _expedienteConsumoMedicamentoRepository.ConsularPorNotificacion(notificacionUsuario.IdNotificacion);
            if (toma != null && tomaTomada)
            {
                toma.FechaToma = DateTime.Now;
                _expedienteConsumoMedicamentoRepository.Editar(toma);
            }
            context.NotificacionUsuario.Update(notificacionUsuario);
        }
        context.SaveChanges();
    }

    public async Task<string?> ObtenerImagenUsuario(int IdUsuario)
    {

        return await _usuarioService.ObtenerBytesImagenUsuario(IdUsuario);
    }
}