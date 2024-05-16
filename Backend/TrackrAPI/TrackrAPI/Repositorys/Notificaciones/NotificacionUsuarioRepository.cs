using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using MimeTypes;
using Org.BouncyCastle.Utilities;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Archivos;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Repositorys.Notificaciones;

public class NotificacionUsuarioRepository : Repository<NotificacionUsuario>, INotificacionUsuarioRepository
{
    private readonly IExpedienteConsumoMedicamentoRepository _expedienteConsumoMedicamentoRepository;
    private readonly IArchivoRepository _archivoRepository;
    public NotificacionUsuarioRepository(TrackrContext context, IExpedienteConsumoMedicamentoRepository expedienteConsumoMedicamentoRepository, IArchivoRepository archivoRepository) : base(context)
    {
        _expedienteConsumoMedicamentoRepository = expedienteConsumoMedicamentoRepository;
        _archivoRepository = archivoRepository;
    }

    private IQueryable<NotificacionUsuario> ConsultarPorUsuario(int idUsuario)
    {
        const int limite = 10;

        return context.NotificacionUsuario
            .Where(n => n.IdUsuario == idUsuario)
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
                nu.IdNotificacionNavigation.FechaAlta,
                nu.Visto,
                nu.IdNotificacionNavigation.IdTipoNotificacion,
                nu.IdNotificacionNavigation.IdChat
            ));
    }

    public IEnumerable<NotificacionDoctorDTO> ConsultarPorDoctor(int idUsuario)
    {
        var notificacionesUsuario = ConsultarPorUsuario(idUsuario)
            .Include(n => n.IdNotificacionNavigation.NotificacionDoctor)
            .Include(n => n.IdNotificacionNavigation.IdTipoNotificacionNavigation)
            .Include(n => n.IdNotificacionNavigation)
            .ThenInclude(n => n.IdPersonaNavigation)
            .AsEnumerable();

        return notificacionesUsuario
            .Select(nu => new NotificacionDoctorDTO(
                nu.IdNotificacionUsuario,
                nu.IdNotificacion,
                nu.IdUsuario,
                nu.IdNotificacionNavigation.Titulo,
                nu.IdNotificacionNavigation.Mensaje,
                nu.IdNotificacionNavigation.FechaAlta,
                nu.Visto,
                nu.IdNotificacionNavigation.IdTipoNotificacion,
                nu.IdNotificacionNavigation.NotificacionDoctor.FirstOrDefault()?.IdPaciente ?? 0,
                this.ObtenerImagenUsuario(nu?.IdNotificacionNavigation?.IdPersona != null ? (int)nu?.IdNotificacionNavigation?.IdPersona : 0, !string.IsNullOrEmpty(nu?.IdNotificacionNavigation?.IdPersonaNavigation?.ImagenTipoMime) ? nu?.IdNotificacionNavigation?.IdPersonaNavigation?.ImagenTipoMime : null),
                nu.IdNotificacionNavigation.IdChat,
                nu.IdNotificacionNavigation.IdTipoNotificacionNavigation.Clave
            ));
    }

    public void MarcarComoVistas(List<int> idNotificacionUsuario)
    {
        var notificacionesUsuario = context.NotificacionUsuario
            .Where(n => idNotificacionUsuario.Contains(n.IdNotificacionUsuario))
            .ToList();

        foreach (var notificacionUsuario in notificacionesUsuario)
        {
            notificacionUsuario.Visto = true;
            var toma = _expedienteConsumoMedicamentoRepository.ConsularPorNotificacion(notificacionUsuario.IdNotificacion);
            if (toma != null)
            {
                toma.FechaToma = DateTime.Now;
                _expedienteConsumoMedicamentoRepository.Editar(toma);
            }
            context.NotificacionUsuario.Update(notificacionUsuario);
        }
        context.SaveChanges();
    }

    public string? ObtenerImagenUsuario(int IdUsuario, string ImagenTipoMime)
    {

        if (!string.IsNullOrEmpty(ImagenTipoMime))
        {
            var img = _archivoRepository.ObtenerImagenUsuario(IdUsuario);
            if (img != null)
            {
                return "data:" + img.ArchivoTipoMime + ";base64," + Convert.ToBase64String(img.Archivo1);
            }

            // string filePath = $"Archivos/Usuario/{IdUsuario}{MimeTypeMap.GetExtension(ImagenTipoMime)}";
            // if (File.Exists(filePath))
            // {
            //     byte[] imageArray = File.ReadAllBytes(filePath);
            //     var ImagenBase64 = Convert.ToBase64String(imageArray);
            //     return "data:"+ImagenTipoMime+";base64,"+ImagenBase64;
            // }
        }
        return null;
    }
}