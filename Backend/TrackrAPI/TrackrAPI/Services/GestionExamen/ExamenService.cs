using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Helpers;
using DinkToPdf;
using DinkToPdf.Contracts;
using System;


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
    private readonly IConverter _converter;

    private readonly string AsuntoCorreo = "Programación de examen";

    public ExamenService(
        IExamenRepository examenRepository,
        ExamenValidatorService examenValidatorService,
        CorreoHelper correoHelper,
        IProgramacionExamenRepository programacionExamenRepository,
        IUsuarioRepository usuarioRepository,
        IExamenReactivoRepository examenReactivoRepository,
        IReactivoRepository reactivoRepository,
        IContenidoExamenRepository contenidoExamenRepository,
        IConverter converter)
    {
        _correoHelper = correoHelper;
        _examenRepository = examenRepository;
        _examenValidatorService = examenValidatorService;
        _examenReactivoRepository = examenReactivoRepository;
        _reactivoRepository = reactivoRepository;
        _contenidoExamenRepository = contenidoExamenRepository;
        _programacionExamenRepository = programacionExamenRepository;
        _usuarioRepository = usuarioRepository;
        _converter = converter;
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

    public IEnumerable<ExamenGridDto> ConsultarMisExamenesContestados(int idUsuario)
    {
        return _examenRepository.ConsultarMisExamenesContestados(idUsuario);
    }

    public IEnumerable<CuestionariosPorResponsableDto> ConsultarExamenesPendientesPorResponsable(int idUsuario)
    {
        return _examenRepository.ConsultarExamenesPendientesPorResponsable(idUsuario);
    }

    public IEnumerable<CuestionariosPorResponsableDto> ConsultarExamenesVencidosPorResponsable(int idUsuario)
    {
        return _examenRepository.ConsultarExamenesVencidosPorResponsable(idUsuario);
    }

    public IEnumerable<CuestionariosPorResponsableDto> ConsultarExamenesContestadosPorResponsable(int idUsuario)
    {
        return _examenRepository.ConsultarExamenesContestadosPorResponsable(idUsuario);
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

            foreach (ContenidoExamen cte in cteList)
            {
                IEnumerable<Reactivo> reactivoList = _reactivoRepository.ConsultarReactivosAleatorio(cte.IdAsignatura, cte.IdNivelExamen, cte.TotalPreguntas ?? 0);

                foreach (Reactivo reactivo in reactivoList)
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

    public byte[] descargarRespuestasPdf(int idExamen)
    {
        var examen = _examenRepository.ConsultarMiExamen(idExamen);

        string nombreArchivo = @$"Respuestas_{examen.TipoExamen}_{examen.NombreUsuario}.pdf";

        string html = ObtenerEstilos();

        html += GenerarPdf(idExamen);

        var globalSettings = new GlobalSettings
        {
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.Letter,
            DocumentTitle = nombreArchivo,
        };

        var objectSettings = new ObjectSettings
        {
            HtmlContent = html,
            WebSettings = { DefaultEncoding = "utf-8" }
        };

        var pdfFile = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        };

        return _converter.Convert(pdfFile);

    }

    public string GenerarPdf(int idExamen)
    {
        string preguntas = "";
        var index = 0;

        var examen = _examenRepository.ConsultarMiExamen(idExamen);
        var reactivos = _examenReactivoRepository.ConsultarReactivosExamen(examen.IdExamen);

        foreach (var reactivo in reactivos)
        {
            if (reactivo.ImagenBase64 != "" && reactivo.ImagenBase64 != "data:;base64,")
            {
                preguntas +=
                    @$"
                        <div class='pregunta'>
                           <h3>{ index+1 }.- { reactivo.Pregunta }</h3>
                           <div style='text-align: center; margin-top: 10px; margin-bottom: 10px;'>
                               <img id='logo' class='imagenPregunta' src='{ reactivo.ImagenBase64 }' height='200'/>
                           </div>
                           <p>{ reactivo.Respuestas.ToList() }</p>
                           <p>Respondió: <span style='font-weight: bold;'>{ reactivo.RespuestaAlumno }</span></p>
                       </div>
                    ";
            }
            else
            {
                preguntas +=
                @$"
                    <div class='pregunta'>
                        <h3>{index+1 }.- { reactivo.Pregunta }</h3>
                        <p> { reactivo.Respuestas.ToList() }</p>
                        <p>Respondió: <span style='font-weight: bold;'>{ reactivo.RespuestaAlumno }</span></p>
                    </div>
                ";
            }
            index++;
        }

        return
            @$"
                <div class='body'>
                    <div class='header'>
                        <div class='titulo'>
                            <h1>{ examen.TipoExamen }: { examen.Clave }</h1>
                        </div>
                        <div class='subtitulo'>
                            <div><h2>Respondió: { examen.NombreUsuario }</h2></div>
                            <div>
                                <h2> Fecha: { examen.FechaExamen.Value.ToShortDateString() }</h2>
                                <h2> Hora: { examen.HoraExamen }</h2>
                            </div>
                        </div>
                    </div>
                    <div class='preguntas'>
                        { preguntas }
                    </div>
                </div>
            ";

    }

    private string ObtenerEstilos()
    {
        return @"
            <style>
                
                .body{
                    font-family: 'Times New Roman', Times, serif;
                }
                .header{
                    margin-top: 5vh;
                    margin-bottom: 2vh;
                    text-align: center;

                }

                .titulo{
                    font-size: 20px
                }

                .subtitulo{
                    font-size: 18px;
                    margin: 5vh 1vw 5vh 1vw ;
                    justify-content: space-between;
                    display: flex;
    
                }

                .preguntas{
                    font-size: 15px;
                    margin: 2em;
                }

                .pregunta p{
                    white-space: pre-wrap;
                }

            </style>";
    }
}
