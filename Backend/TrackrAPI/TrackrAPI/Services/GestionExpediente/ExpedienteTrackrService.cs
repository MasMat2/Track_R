using Microsoft.Extensions.Hosting.Internal;
using MimeTypes;
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
using TrackrAPI.Services.Dashboard;
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

    public ExpedienteTrackrService(
        IExpedienteTrackrRepository expedienteTrackrRepository,
        IDomicilioRepository domicilioRepository,
        IUsuarioRepository usuarioRepository,
        IExpedientePadecimientoRepository expedientePadecimientoRepository,
        UsuarioWidgetService usuarioWidgetService,
        ExpedienteTrackrValidatorService expedienteTrackrValidatorService
        )
    {
        this._expedienteTrackrRepository = expedienteTrackrRepository;
        this._domicilioRepository = domicilioRepository;
        this._usuarioRepository = usuarioRepository;
        this._expedientePadecimientoRepository = expedientePadecimientoRepository;
        _usuarioWidgetService = usuarioWidgetService;
        _expedienteTrackrValidatorService = expedienteTrackrValidatorService;
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

        _usuarioWidgetService.modificarSeleccionWidgets(expedienteTrackr.IdUsuario , GeneralConstant.WidgetsDefault);

        scope.Complete();
        return expedienteTrackr.IdExpediente;
    }

    public int AgregarExpedienteNuevoUsuario(Usuario usuario)
    {
        using TransactionScope scope = new();
        ExpedienteTrackr expedienteTrackr = new ExpedienteTrackr
        {
            IdUsuario = usuario.IdUsuario,
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
    public int EditarWrapper(ExpedienteWrapper expedienteWrapper)
    {
        ExpedienteTrackr expedienteTrackr = expedienteWrapper.expediente;
        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        {
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

    public IEnumerable<UsuarioExpedienteGridDTO> ConsultarParaGrid(int idDoctor)
    {
        IEnumerable<UsuarioExpedienteGridDTO> expedientes = _expedienteTrackrRepository.ConsultarParaGrid(idDoctor);
        foreach(UsuarioExpedienteGridDTO expediente in expedientes)
        {
            if (!string.IsNullOrWhiteSpace(expediente.TipoMime))
            {
                string filePath = $"Archivos/Usuario/{expediente.IdUsuario}{MimeTypeMap.GetExtension(expediente.TipoMime)}";
                if (File.Exists(filePath))
                {
                    byte[] imageArray = File.ReadAllBytes(filePath);
                    expediente.ImagenBase64 = Convert.ToBase64String(imageArray);
                }
                expediente.DosisNoTomadas = _expedienteTrackrRepository.DosisNoTomadas(expediente.IdExpedienteTrackr);
                expediente.VariablesFueraRango = _expedienteTrackrRepository.VariablesFueraRango(expediente.IdUsuario);
            }

        }
        return expedientes;
    }

    /// <summary>
    /// Consulta un expedienteWrapper por usuario. Construye el ExpedienteWrapper a base de 4 modelos:
    /// Expediente, Domicilio, Usuario y Padecimientos.
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns>El expedienteWrapper del usuario</returns>
    public ExpedienteWrapper ConsultarWrapperPorUsuario(int idUsuario)
    {
        ExpedienteWrapper expedienteWrapper = new ExpedienteWrapper
        {
            expediente = ConsultarPorUsuario(idUsuario),
            // domicilio = domicilioRepository.ConsultarPorUsuario(idUsuario).FirstOrDefault(),
            paciente = _usuarioRepository.ConsultarDto(idUsuario),
            padecimientos = _expedientePadecimientoRepository.ConsultarPorUsuario(idUsuario)
        };
        return expedienteWrapper;
    }
            public UsuarioExpedienteSidebarDTO ConsultarParaSidebar(int idUsuario)
    {
        return _expedienteTrackrRepository.ConsultarParaSidebar(idUsuario);

    }
    public IEnumerable<ApegoTomaMedicamentoDto> ApegoMedicamentoUsuarios(int idDoctor)
    {
        return _expedienteTrackrRepository.ApegoMedicamentoUsuarios(idDoctor);
    }

}
