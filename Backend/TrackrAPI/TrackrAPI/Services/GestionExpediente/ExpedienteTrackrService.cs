using Microsoft.Extensions.Hosting.Internal;
using MimeTypes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.DTOs.Dashboard;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Repositorys.Inventario;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Archivos;
using TrackrAPI.Services.Dashboard;
using TrackrAPI.Services.Seguridad;
using TrackrAPI.Services.Sftp;
using static System.Net.Mime.MediaTypeNames;

namespace TrackrAPI.Services.GestionExpediente;

public class ExpedienteTrackrService
{
    private readonly IExpedienteTrackrRepository _expedienteTrackrRepository;
    private readonly IDomicilioRepository _domicilioRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IExpedientePadecimientoRepository _expedientePadecimientoRepository;
    private readonly UsuarioWidgetService _usuarioWidgetService;
    private readonly ExpedienteTrackrValidatorService _expedienteTrackrValidatorService;
    private readonly IAsistenteDoctorRepository _asistenteDoctorRepository;
    private readonly ArchivoService _archivoService;
    private readonly IExpedienteDoctorRepository _expedienteDoctorRepository;
    private readonly SftpService _sftpService;

    public ExpedienteTrackrService(
        IExpedienteTrackrRepository expedienteTrackrRepository,
        IDomicilioRepository domicilioRepository,
        IUsuarioRepository usuarioRepository,
        IExpedientePadecimientoRepository expedientePadecimientoRepository,
        UsuarioWidgetService usuarioWidgetService,
        ExpedienteTrackrValidatorService expedienteTrackrValidatorService,
        IAsistenteDoctorRepository asistenteDoctorRepository,
        ArchivoService archivoService,
        IExpedienteDoctorRepository expedienteDoctorRepository,
        SftpService sftpService


        )
    {
        this._expedienteTrackrRepository = expedienteTrackrRepository;
        this._domicilioRepository = domicilioRepository;
        this._usuarioRepository = usuarioRepository;
        this._expedientePadecimientoRepository = expedientePadecimientoRepository;
        _usuarioWidgetService = usuarioWidgetService;
        _expedienteTrackrValidatorService = expedienteTrackrValidatorService;
        _asistenteDoctorRepository = asistenteDoctorRepository;
        _archivoService = archivoService;
        _expedienteDoctorRepository = expedienteDoctorRepository;
        _sftpService = sftpService;
    }
    /// <summary>
    /// Consulta el expediente de un usuario
    /// </summary>
    /// <param name="idUsuario">Id del Usuario a Consultar</param>
    /// <returns>ExpedienteTrackR consultado</returns>
    public ExpedienteTrackr ConsultarPorUsuario(int idUsuario)
    {
        return _expedienteTrackrRepository.ConsultarPorUsuario(idUsuario);
    }

    /// <summary>
    /// Agrega un expedienteWrapper.Primero asigna cada una de las propiedades del expedienteWrapper a sus respectivos modelos.
    /// Para después utilizar el Repositorio de cada modelo y agregarlos.
    /// </summary>
    /// <param name="expedienteWrapper">El expediente, el domicilio y el usuario a agregar</param>
    /// <returns>El id del expediente agregado</returns>
    public int AgregarWrapper(ExpedienteWrapper expedienteWrapper)
    {
        using TransactionScope scope = new();
        ExpedienteTrackr expedienteTrackr = expedienteWrapper.expediente;
        expedienteTrackr.IdUsuario = expedienteWrapper.paciente.IdUsuario;
        _expedienteTrackrValidatorService.ValidarAgregar(expedienteTrackr);

        expedienteTrackr = _expedienteTrackrRepository.Agregar(expedienteTrackr);
        expedienteTrackr.Numero = expedienteTrackr.IdExpediente.ToString().PadLeft(6, '0');
        _expedienteTrackrRepository.Editar(expedienteTrackr);

        AgregarPadecimientos(expedienteWrapper.padecimientos, expedienteTrackr.IdExpediente);

        _usuarioWidgetService.modificarSeleccionWidgets(expedienteTrackr.IdUsuario, GeneralConstant.WidgetsDefault);

        scope.Complete();
        return expedienteTrackr.IdExpediente;
    }

    public int AgregarExpedienteNuevoUsuario(int idUsuario)
    {
        using TransactionScope scope = new();
        ExpedienteTrackr expedienteTrackr = new ExpedienteTrackr
        {
            IdUsuario = idUsuario,
            Numero = "0",
            FechaNacimiento = Utileria.ObtenerFechaActual(),
            Cintura = 1,
            Estatura = 1,
            Peso = 1,
            IdGenero = 9,
            FechaAlta = Utileria.ObtenerFechaActual()
        };
        _expedienteTrackrValidatorService.ValidarAgregar(expedienteTrackr);

        expedienteTrackr = _expedienteTrackrRepository.Agregar(expedienteTrackr);
        expedienteTrackr.Numero = expedienteTrackr.IdExpediente.ToString().PadLeft(6, '0');
        _expedienteTrackrRepository.Editar(expedienteTrackr);

        _usuarioWidgetService.modificarSeleccionWidgets(expedienteTrackr.IdUsuario, GeneralConstant.WidgetsDefault);

        scope.Complete();
        return expedienteTrackr.IdExpediente;
    }

    /// <summary>
    /// Edita un expedienteWrapper. Primero asigna cada una de las propiedades del expedienteWrapper a sus respectivos modelos.
    /// Para después utilizar el Repositorio de cada modelo y editarlos.
    /// </summary>
    /// <param name="expedienteWrapper">El expediente, el domicilio y el usuario a editar</param>
    /// <returns>El id del expediente editado</returns>
    public int EditarWrapper(ExpedienteWrapper expedienteWrapper, int idDoctor)
    {
        ExpedienteTrackr expedienteTrackr = expedienteWrapper.expediente;
        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        {

            var expediente = _expedienteDoctorRepository.ConsultarExpedientePorDoctor(expedienteTrackr.IdExpediente, idDoctor);

            if (expediente == null)
            {
                var expedienteDoctor = new ExpedienteDoctor
                {
                    IdExpediente = expedienteTrackr.IdExpediente,
                    IdUsuarioDoctor = idDoctor
                };

                _expedienteDoctorRepository.Agregar(expedienteDoctor);
            }

            var idUsuario = expedienteWrapper.paciente.IdUsuario;
            _expedienteTrackrValidatorService.ValidarEditar(expedienteTrackr);

            // Agregar ExpedienteTrackR
            expedienteTrackr.IdUsuario = idUsuario;
            expedienteTrackr.FechaAlta = DateTime.Now;
            if (expedienteTrackr.IdExpediente == 0)
            {
                ExpedienteTrackr expedienteAgregado = _expedienteTrackrRepository.Agregar(expedienteTrackr);
                expedienteTrackr.Numero = expedienteAgregado.IdExpediente.ToString().PadLeft(6, '0');
                _expedienteTrackrRepository.Editar(expedienteTrackr);
            }
            else
            {
                expedienteTrackr.Numero = expedienteTrackr.IdExpediente.ToString().PadLeft(6, '0');
                _expedienteTrackrRepository.Editar(expedienteTrackr);
            }

            AgregarPadecimientos(expedienteWrapper.padecimientos, expedienteTrackr.IdExpediente);
            scope.Complete();
        }
        return expedienteTrackr.IdExpediente;
    }

    public void Eliminar(int IdExpediente)
    {
        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        {
            _expedientePadecimientoRepository.EliminarPorExpediente(IdExpediente);
            var expedienteTrakcr = _expedienteTrackrRepository.Consultar(IdExpediente);
            _expedienteTrackrRepository.Eliminar(expedienteTrakcr);
            scope.Complete();
        }
    }

    public void AgregarPadecimientos(IEnumerable<ExpedientePadecimientoDTO> padecimientos, int idExpediente)
    {
        _expedientePadecimientoRepository.EliminarPorExpediente(idExpediente);
        foreach (var padecimientoDTO in padecimientos)
        {
            var padecimiento = new ExpedientePadecimiento();

            padecimiento.IdPadecimiento = padecimientoDTO.IdPadecimiento;
            padecimiento.FechaDiagnostico = padecimientoDTO.FechaDiagnostico;
            padecimiento.IdExpediente = idExpediente;

            if (padecimiento.IdPadecimiento == 0)
            {
                continue;
            }

            _expedientePadecimientoRepository.Agregar(padecimiento);

        }
    }

    public IEnumerable<UsuarioExpedienteGridDTO> ConsultarParaGrid(int idDoctor, int idCompania)
    {
        List<int> idDoctorList = new();
        var esAsistente = _usuarioRepository.ConsultarPorRol(GeneralConstant.ClaveRolAsistente, idCompania)
                                                .Any((usuario) => usuario.IdUsuario == idDoctor);

        if (esAsistente)
        {
            idDoctorList = _asistenteDoctorRepository.ConsultarDoctoresPorAsistente(idDoctor)
                                                         .Select(ad => ad.IdUsuario).ToList();
        }
        else
        {
            idDoctorList.Add(idDoctor);
        }

        IEnumerable<UsuarioExpedienteGridDTO> expedientes = _expedienteTrackrRepository.ConsultarParaGrid(idDoctorList, idCompania);
        foreach (UsuarioExpedienteGridDTO expediente in expedientes)
        {

            var img = _archivoService.ObtenerImagenUsuario(expediente.IdUsuario);

            if (img != null)
            {
                expediente.ImagenBase64 = "data:" + img.ArchivoTipoMime + ";base64," +  _sftpService.DownloadFile(img.ArchivoUrl);
                //expediente.ImagenBase64 = "data:" + img.ArchivoTipoMime + ";base64," + Convert.ToBase64String(img.Archivo1);
            }
            // string filePath = $"Archivos/Usuario/{expediente.IdUsuario}{MimeTypeMap.GetExtension(expediente.TipoMime)}";

            //     //Console.WriteLine("Expediente : " + JsonConvert.SerializeObject(expediente, Formatting.Indented));
            //     //Console.WriteLine("--------------------");
            // if (File.Exists(filePath))
            // {
            //     byte[] imageArray = File.ReadAllBytes(filePath);

            //     expediente.ImagenBase64 = Convert.ToBase64String(imageArray);
            // }

            expediente.DosisNoTomadas = _expedienteTrackrRepository.DosisNoTomadas(expediente.IdExpedienteTrackr);
            expediente.VariablesFueraRango = _expedienteTrackrRepository.VariablesFueraRango(expediente.IdUsuario);

        }
        expedientes = expedientes.OrderBy(e => e.DoctorAsociado);
        return expedientes;
    }

    public IEnumerable<TomasTomadasPorPadecimientoDto> RecordatoriosPorPadecimiento(int idUsuario)
    {
        var recordatorios = _expedienteTrackrRepository.RecordatoriosPorUsuario(idUsuario);

        return recordatorios
        .GroupBy(tr => tr.Padecimiento)
        .Select(recordatoriosPorPad => new TomasTomadasPorPadecimientoDto
        {
            IdPadecimiento = recordatoriosPorPad.Key,
            TomasTomadas = recordatoriosPorPad
            .GroupBy(tr => tr.Dia)
            .Select(recordatoriosPorDia => new TomasTomadasPorDiaDto
            {
                Dia = recordatoriosPorDia.Key,
                TomasTotales = recordatoriosPorDia.Count(),
                TomasTomadas = recordatoriosPorDia.Sum(rpd => rpd.Tomas.Count(t => t.FechaToma != null))
            }).ToList()
        });
    }

    public IEnumerable<TomasTomadasPorDiaDto> RecordatoriosPorDia(int idUsuario)
    {
        var recordatorios = _expedienteTrackrRepository.RecordatoriosPorUsuario(idUsuario);

        return recordatorios
        .GroupBy(tr => tr.Dia)
           .Select(recordatoriosPorDia => new TomasTomadasPorDiaDto
           {
               Dia = recordatoriosPorDia.Key,
               TomasTotales = recordatoriosPorDia.Sum(rpd => rpd.Tomas.Count),
               TomasTomadas = recordatoriosPorDia.Sum(rpd => rpd.Tomas.Count(t => t.FechaToma != null))
           });
    }

    public IEnumerable<TomasTomadasPorPadecimientoDto> RecordatoriosPorPadecimientoHoy(int idUsuario)
    {
        DateTime now = DateTime.Now;
        int currentDay = ((int)now.DayOfWeek + 7) % 7;
        var recordatoriosPorPadecimiento = RecordatoriosPorPadecimiento(idUsuario).ToList();

        var tomasATomarHoy = recordatoriosPorPadecimiento
        .Select(th => new TomasTomadasPorPadecimientoDto
        {
            IdPadecimiento = th.IdPadecimiento,
            TomasTomadas = th.TomasTomadas.Where(t => t.Dia == currentDay).ToList()
        });
        return tomasATomarHoy;

    }
    public (int TomasTotales, int TomasTomadas) TomasPadecimientoTotalesHoy(int idUsuario, int idPadecimiento)
    {
        var recordatoriosPorPadecimientoHoy = RecordatoriosPorPadecimientoHoy(idUsuario).ToList();

        int tomasTotales = recordatoriosPorPadecimientoHoy.Where(th => th.IdPadecimiento == idPadecimiento)
                                                          .Select(ttp => ttp.TomasTomadas.Select(tt => tt.TomasTotales).FirstOrDefault())
                                                          .FirstOrDefault();

        int tomasTomadas = recordatoriosPorPadecimientoHoy.Where(th => th.IdPadecimiento == idPadecimiento)
                                                          .Select(ttp => ttp.TomasTomadas.Select(tt => tt.TomasTomadas).FirstOrDefault())
                                                          .FirstOrDefault();

        return (tomasTotales, tomasTomadas);
    }
    /// <summary>
    /// Consulta un expedienteWrapper por usuario. Construye el ExpedienteWrapper a base de 4 modelos:
    /// Expediente, Domicilio, Usuario y Padecimientos.
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns>El expedienteWrapper del usuario</returns>
    public ExpedienteWrapper ConsultarWrapperPorUsuario(int idUsuario, int idDoctor)
    {
        ExpedienteWrapper expedienteWrapper = new ExpedienteWrapper
        {
            expediente = ConsultarPorUsuario(idUsuario),
            // domicilio = domicilioRepository.ConsultarPorUsuario(idUsuario).FirstOrDefault(),
            paciente = _usuarioRepository.ConsultarDto(idUsuario),
            padecimientos = _expedientePadecimientoRepository.ConsultarPorUsuarioDoctor(idUsuario, idDoctor).ToList()
        };
        return expedienteWrapper;
    }
    public UsuarioExpedienteSidebarDTO ConsultarParaSidebar(int idUsuario)
    {
        return _expedienteTrackrRepository.ConsultarParaSidebar(idUsuario);

    }
    public IEnumerable<ApegoTomaMedicamentoDto> ApegoMedicamentoUsuarios(int idDoctor, int idCompania)
    {
        List<int> idDoctorList = new();
        var esAsistente = _usuarioRepository.ConsultarPorRol(GeneralConstant.ClaveRolAsistente, idCompania)
                                                .Any((usuario) => usuario.IdUsuario == idDoctor);

        if (esAsistente)
        {
            idDoctorList = _asistenteDoctorRepository.ConsultarDoctoresPorAsistente(idDoctor)
                                                        .Select(ad => ad.IdUsuario).ToList();
        }
        else
        {
            idDoctorList.Add(idDoctor);
        }

        return _expedienteTrackrRepository.ApegoMedicamentoUsuarios(idDoctorList);
    }
    public IEnumerable<ApegoTomaMedicamentoDto> ApegoTratamientoPorPaciente(int idUsuario)
    {
        return _expedienteTrackrRepository.ApegoTratamientoPorPaciente(idUsuario);
    }

    public IEnumerable<UsuarioExpedienteGridDTO> ConsultarParaRecomendacionesGenerales()
    {
        return _expedienteTrackrRepository.ConsultarParaRecomendacionesGenerales();
    }

}
