using TrackrAPI.Dtos.GestionExpediente.ExpedienteDoctor;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Archivos;

namespace TrackrAPI.Services.GestionExpediente;

public class ExpedienteDoctorService
{
    private readonly IExpedienteDoctorRepository _expedienteDoctorRepository;
    private readonly IExpedienteTrackrRepository _expedienteTrackrRepository;
    private readonly ExpedienteDoctorValidatorService _expedienteDoctorValidatorService;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ArchivoService _archivoService;

    public ExpedienteDoctorService(IExpedienteDoctorRepository expedienteDoctorRepository,
                                     IExpedienteTrackrRepository expedienteTrackrRepository,
                                     ExpedienteDoctorValidatorService expedienteDoctorValidatorService,
                                     IUsuarioRepository usuarioRepository,
                                     ArchivoService archivoService

                                    )
    {
        _expedienteDoctorRepository = expedienteDoctorRepository;
        _expedienteTrackrRepository = expedienteTrackrRepository;
        _expedienteDoctorValidatorService = expedienteDoctorValidatorService;
        _usuarioRepository = usuarioRepository;
        _archivoService = archivoService;
    }

    public IEnumerable<ExpedienteDoctorCardsDTO> ConsultarExpediente(int idUsuario)
    {
        int idExpediente = _expedienteTrackrRepository.ConsultarPorUsuario(idUsuario).IdExpediente;


        return _expedienteDoctorRepository.ConsultarExpediente(idExpediente)
        .Select(dto => new ExpedienteDoctorCardsDTO
        {
            IdExpediente = dto.IdExpediente,
            IdUsuarioDoctor = dto.IdUsuarioDoctor,
            IdExpedienteDoctor = dto.IdExpedienteDoctor,
            Ambito = "Endicronologia",
            Hospital = dto.IdUsuarioDoctorNavigation.IdCompaniaNavigation.Nombre,
            Nombre = dto.IdUsuarioDoctorNavigation.Nombre + " " + dto.IdUsuarioDoctorNavigation.ApellidoPaterno + " " + dto.IdUsuarioDoctorNavigation.ApellidoMaterno,
        }).ToList();
    }

    public IEnumerable<ExpedienteDoctorConImagenPerfilDTO> ConsultarExpedienteConImagenesPerfil(int idUsuario)
    {
        int idExpediente = _expedienteTrackrRepository.ConsultarPorUsuario(idUsuario).IdExpediente;


        var doctores = _expedienteDoctorRepository.ConsultarExpediente(idExpediente)
        .Select(dto => new ExpedienteDoctorConImagenPerfilDTO
        {
            IdExpediente = dto.IdExpediente,
            IdUsuarioDoctor = dto.IdUsuarioDoctor,
            IdExpedienteDoctor = dto.IdExpedienteDoctor,
            Ambito = "Endicronologia",
            Hospital = dto.IdUsuarioDoctorNavigation.IdCompaniaNavigation.Nombre,
            Nombre = dto.IdUsuarioDoctorNavigation.Nombre + " " + dto.IdUsuarioDoctorNavigation.ApellidoPaterno + " " + dto.IdUsuarioDoctorNavigation.ApellidoMaterno,
        }).ToList();

        foreach (var doctor in doctores)
        {
            var usuario = _usuarioRepository.ConsultarDto(doctor.IdUsuarioDoctor);
            if (usuario != null)
            {
                if (!string.IsNullOrEmpty(usuario.ImagenTipoMime))
                {
                    var imagen = _archivoService.ObtenerImagenUsuario(usuario.IdUsuario);
                    if (imagen != null)
                    {
                        doctor.ImagenBase64 = Convert.ToBase64String(imagen.Archivo1);
                        doctor.TipoMime = imagen.ArchivoTipoMime;
                    }
                }
            }
        }

        return doctores;
    }


    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarSelector(int idUsuario)
    {
        int idExpediente = _expedienteTrackrRepository.ConsultarPorUsuario(idUsuario).IdExpediente;
        int idCompania = _usuarioRepository.ConsultarDto(idUsuario).IdCompania;
        return _expedienteDoctorRepository.ConsultarSelector(idExpediente, idCompania);
    }
    public void Eliminar(ExpedienteDoctorDTO expedienteDoctorDTO)
    {

        _expedienteDoctorValidatorService.ValidarEliminar(expedienteDoctorDTO.IdUsuarioDoctor);
        var expedienteDoctor = new ExpedienteDoctor
        {
            IdUsuarioDoctor = expedienteDoctorDTO.IdUsuarioDoctor,
            IdExpediente = expedienteDoctorDTO.IdExpediente,
            IdExpedienteDoctor = expedienteDoctorDTO.IdExpedienteDoctor
        };

        _expedienteDoctorRepository.Eliminar(expedienteDoctor);
    }

    public void Agregar(ExpedienteDoctorDTO expedienteDoctorDTO, int idUsuario)
    {

        int idCompania = _usuarioRepository.ConsultarDto(idUsuario).IdCompania;

        if (expedienteDoctorDTO.IdExpediente == 0 || expedienteDoctorDTO.IdExpediente == null)
        {
            int idExpediente = _expedienteTrackrRepository.ConsultarPorUsuario(idUsuario).IdExpediente;
            _expedienteDoctorValidatorService.ValidarAgregar(idExpediente, expedienteDoctorDTO.IdUsuarioDoctor, idCompania);

            var expedienteDoctor = new ExpedienteDoctor
            {
                IdUsuarioDoctor = expedienteDoctorDTO.IdUsuarioDoctor,
                IdExpediente = idExpediente
            };

            _expedienteDoctorRepository.Agregar(expedienteDoctor);
        }
        else
        {
            _expedienteDoctorValidatorService.ValidarAgregar(expedienteDoctorDTO.IdExpediente, expedienteDoctorDTO.IdUsuarioDoctor, idCompania);

            var expedienteDoctor = new ExpedienteDoctor
            {
                IdUsuarioDoctor = expedienteDoctorDTO.IdUsuarioDoctor,
                IdExpediente = expedienteDoctorDTO.IdExpediente
            };

            _expedienteDoctorRepository.Agregar(expedienteDoctor);
        }
        
    }

}