using Org.BouncyCastle.Crypto.Engines;
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Services.GestionExamen;

public class ProgramacionExamenService
{
    private readonly IProgramacionExamenRepository _programacionExamenRepository;
    private readonly ProgramacionExamenValidatorService _programacionExamenValidatorService;
    private readonly UsuarioService _usuarioService;
    private readonly IAsistenteDoctorRepository _asistenteDoctorRepository;

    public ProgramacionExamenService(
        IProgramacionExamenRepository programacionExamenRepository,
        ProgramacionExamenValidatorService programacionExamenValidatorService,
        UsuarioService usuarioService,
        IAsistenteDoctorRepository asistenteDoctorRepository)
    {
        _programacionExamenRepository = programacionExamenRepository;
        _programacionExamenValidatorService = programacionExamenValidatorService;
        _usuarioService = usuarioService;
        _asistenteDoctorRepository = asistenteDoctorRepository;
    }

    public ProgramacionExamenDto? Consultar(int idProgramacionExamen)
    {
        ProgramacionExamenDto? programacionExamen = _programacionExamenRepository.Consultar(idProgramacionExamen);
        return programacionExamen;
    }
    public IEnumerable<ProgramacionExamenGridDto> ConsultarGeneral(int idCompania, int idUsuarioSesion)
    {
        bool esAsistente = _usuarioService.EsAsistente(idCompania,idUsuarioSesion);

        List<int> idDoctorList = new();
        if(esAsistente){
            idDoctorList = _asistenteDoctorRepository.ConsultarDoctoresPorAsistente(idUsuarioSesion)
                                                          .Select(ad => ad.IdUsuario).ToList();
            idDoctorList.Add(idUsuarioSesion);
            return _programacionExamenRepository.ConsultarGeneral(idCompania, idDoctorList);
        }else{
            idDoctorList.Add(idUsuarioSesion);
        }

        return _programacionExamenRepository.ConsultarGeneral(idCompania, idDoctorList);
    }
    public IEnumerable<ProgramacionExamenGridDto> ConsultarTodosParaSelector()
    {
        return _programacionExamenRepository.ConsultarTodosParaSelector();
    }

    public int Agregar(ProgramacionExamen programacionExamen, int idCompania)
    {
        _programacionExamenValidatorService.ValidarAgregar(programacionExamen);
        programacionExamen.Clave = GenerarClave(idCompania);
        _programacionExamenRepository.Agregar(programacionExamen);
        return programacionExamen.IdProgramacionExamen;
    }

    private string GenerarClave(int idCompania)
    {
        var programacionExamen = _programacionExamenRepository.ConsultarGeneral(idCompania).OrderByDescending(x => x.IdProgramacionExamen).FirstOrDefault();

        int idProgramacionExamen = programacionExamen?.IdProgramacionExamen ?? 0;
        return (idProgramacionExamen + 1).ToString();
    }

    public void Editar(ProgramacionExamen programacionExamen)
    {
        var programacionExamenExistente = _programacionExamenRepository.Consultar(programacionExamen.IdProgramacionExamen);
        if(programacionExamen is not null)
        {
            programacionExamen.FechaAlta = programacionExamenExistente.FechaAlta;
        }

        _programacionExamenValidatorService.ValidarEditar(programacionExamen);
        _programacionExamenRepository.Editar(programacionExamen);
    }

    public void Eliminar(int idProgramacionExamen)
    {
        ProgramacionExamenDto? programacionExamen = _programacionExamenRepository.Consultar(idProgramacionExamen);
        _programacionExamenValidatorService.ValidarEliminar(idProgramacionExamen);

        if (programacionExamen != null)
        {
            ProgramacionExamen programacionExamenMod = new()
            {
                IdProgramacionExamen = programacionExamen.IdProgramacionExamen,
                IdTipoExamen = programacionExamen.IdTipoExamen,
                IdUsuarioResponsable = programacionExamen.IdUsuarioResponsable,
                Clave = programacionExamen.Clave,
                Duracion = programacionExamen.Duracion,
                CantidadParticipantes = programacionExamen.CantidadParticipantes,
                FechaExamen = programacionExamen.FechaExamen,
                HoraExamen = programacionExamen.HoraExamen,
                FechaAlta = programacionExamen.FechaAlta,
                Estatus = false
            };

            _programacionExamenRepository.Editar(programacionExamenMod);
        }
    }
}
