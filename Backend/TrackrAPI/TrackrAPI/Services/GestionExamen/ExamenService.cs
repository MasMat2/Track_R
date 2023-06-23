using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Helpers;

namespace TrackrAPI.Services.GestionExamen;

public class ExamenService
{
    private readonly IExamenRepository _examenRepository;
    private readonly IProgramacionExamenRepository _programacionExamenRepository;
    private readonly ExamenValidatorService _examenValidatorService;
    private readonly IExamenReactivoRepository _examenReactivoRepository;
    private readonly IReactivoRepository _reactivoRepository;
    private readonly IContenidoExamenRepository _contenidoExamenRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly CorreoHelper _correoHelper;

    private readonly string AsuntoCorreo = "Programación de examen";

    public ExamenService(
        IExamenRepository examenRepository,
        ExamenValidatorService examenValidatorService,
        CorreoHelper correoHelper,
        IProgramacionExamenRepository programacionExamenRepository,
        IUsuarioRepository usuarioRepository,
        IExamenReactivoRepository examenReactivoRepository,
        IReactivoRepository reactivoRepository,
        IContenidoExamenRepository contenidoExamenRepository)
    {
        _correoHelper = correoHelper;
        _examenRepository = examenRepository;
        _examenValidatorService = examenValidatorService;
        _examenReactivoRepository = examenReactivoRepository;
        _reactivoRepository = reactivoRepository;
        _contenidoExamenRepository = contenidoExamenRepository;
        _programacionExamenRepository = programacionExamenRepository;
        _usuarioRepository = usuarioRepository;
    }

    public Examen? Consultar(int idExamen)
    {
        Examen? examen = _examenRepository.Consultar(idExamen);
        return examen;
    }
    public IEnumerable<Examen> ConsultarGeneral(int idProgramacionExamen)
    {
        return _examenRepository.ConsultarGeneral(idProgramacionExamen);
    }
    public IEnumerable<Examen> ConsultarTodosParaSelector(int idProgramacionExamen)
    {
        return _examenRepository.ConsultarTodosParaSelector(idProgramacionExamen);
    }
    public IEnumerable<ExamenGridDto> ConsultarMisExamenes(int idUsuario)
    {
        return _examenRepository.ConsultarMisExamenes(idUsuario);
    }
    public IEnumerable<ExamenCalificacionDto> ConsultarCalificaciones(int idProgramacionExamen)
    {
        return _examenRepository.ConsultarCalificaciones(idProgramacionExamen);
    }

    public ExamenDto? ConsultarMiExamen(int idExamen)
    {
        // Revisar si el examen ya tiene reactivos asignados
        Examen? examen = _examenRepository.Consultar(idExamen);

        if (examen == null)
        {
            return null;
        }

        if (!examen.ExamenReactivo.Any())
        {
            IEnumerable<ContenidoExamen> cteList = _contenidoExamenRepository.ConsultarTodosNoFormato(examen.IdProgramacionExamenNavigation.IdTipoExamen);

            foreach(ContenidoExamen cte in cteList)
            {
                IEnumerable<Reactivo> reactivoList = _reactivoRepository.ConsultarReactivosAleatorio(cte.IdAsignatura, cte.IdNivelExamen, cte.TotalPreguntas ?? 0);

                foreach(Reactivo reactivo in reactivoList)
                {
                    ExamenReactivo examenReactivo = new()
                    {
                        IdExamen = examen.IdExamen,
                        IdReactivo = reactivo.IdReactivo,
                        FechaAlta = DateTime.Now,
                        Estatus = true
                    };

                    _examenReactivoRepository.Agregar(examenReactivo);
                }
            }
        }

        return _examenRepository.ConsultarMiExamen(idExamen);
    }

    public ExamenDto? ConsultarMiExamenIndividual(int idExamen)
    {
        Examen? examen = _examenRepository.Consultar(idExamen);

        if (examen == null)
        {
            return null;
        }

        IEnumerable<ContenidoExamen> cteList = _contenidoExamenRepository
            .ConsultarTodosNoFormato(examen.IdProgramacionExamenNavigation.IdTipoExamen);

        int totalReactivos = 0;

        foreach(ContenidoExamen cte in cteList)
        {
            totalReactivos += _reactivoRepository
                .ConsultarCantidadReactivos(cte.IdAsignatura, cte.IdNivelExamen);
        }

        ExamenDto? examenDto = _examenRepository.ConsultarMiExamen(idExamen);

        if (examenDto == null)
        {
            return null;
        }

        _examenValidatorService.ValidarCantidadReactivos(examenDto, totalReactivos);

        return examenDto;
    }

    public int Agregar(Examen examen)
    {
        _examenValidatorService.ValidarAgregar(examen);
        _examenRepository.Agregar(examen);
        return examen.IdExamen;
    }

    public void Editar(Examen examen)
    {
        _examenValidatorService.ValidarEditar(examen);
        _examenRepository.Editar(examen);
    }

    public void Eliminar(int idExamen)
    {
        Examen? examen = _examenRepository.Consultar(idExamen);
        _examenValidatorService.ValidarEliminar(idExamen);

        if (examen != null)
        {
            examen.Estatus = false;

            _examenRepository.Editar(examen);
        }
    }

    public void Actualizar(List<Examen> examenList)
    {
        if (examenList[0].IdProgramacionExamen == 0)
        {
            return;
        }

        List<Examen> examenDto = _examenRepository
            .ConsultarGeneral(examenList[0].IdProgramacionExamen)
            .ToList();

        foreach (Examen examen in examenList)
        {
            var usuario = _usuarioRepository.Consultar(examen.IdUsuarioParticipante);

            if (examenDto.Exists(p => p.IdExamen == examen.IdExamen))
            {
                // Editar(examen);
            }
            else
            {
                Agregar(examen);

                if (!string.IsNullOrWhiteSpace(usuario.CorreoPersonal))
                {
                    EnviarCorreo(usuario.CorreoPersonal, examen);
                }
            }
        }

        foreach (Examen examen in examenDto)
        {
            if (!examenList.Exists(p => p.IdExamen == examen.IdExamen))
            {
                Eliminar(examen.IdExamen);
            }
        }
    }

    public void EnviarCorreo(string correo, Examen examen)
    {
        // var programacionExamen = _programacionExamenRepository.Consultar(examen.IdProgramacionExamen);

        // if (programacionExamen == null
        //     || programacionExamen.FechaExamen == null
        //     || programacionExamen.HoraExamen == null)
        // {
        //     return;
        // }

        // var modeloCorreo = FormatoCorreo(
        //     correo,
        //     programacionExamen.FechaExamen.Value,
        //     programacionExamen.HoraExamen.Value);

        // _correoHelper.Enviar(modeloCorreo);

    }

    public Correo FormatoCorreo(string receptor, DateTime fecha, TimeSpan hora)
    {
        // string logotipo = _config.GetSection("AppSettings:UrlFrontEnd").Value + "assets/img/logotipo.png";
        // string logotipo2 = _config.GetSection("AppSettings:UrlFrontEnd").Value + "assets/img/atencion-express.png";

        return new Correo
        {
            Receptor = receptor,
            Asunto = AsuntoCorreo,
            Mensaje =
            $"<div>"
                //+ $"<span><img src = \"{logotipo}\"  style = \"width: 200px; height: 45px;\"></span>"
                //+ $"<span><img src = \"{logotipo2}\"  style = \"width: 130px; height: 48px;\" align=\"right\" ></span>"
                + $"</div>"
                + $"<hr style=\"border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;\"> "
                + $"<p>Saludos, este es un correo para notificarle que se le programó un examen.</p>"
                + $"<p>El día: {fecha.Day}/{fecha.Month}/{fecha.Year} a la hora: {hora} <b></b></p>"
                + $"<p>Le deseamos un excelente día por parte del equipo.</p>"
                + $"<p><b>Departamento de Atención al Cliente.</b></p>"
                + $"<hr style=\"border: none; border-bottom: 1px #FF6A00 solid; margin: 20px 0;\">",
            EsMensajeHtml = true
        };
    }
}
