using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;
using System.Transactions;
using TrackrAPI.Services.Sftp;

namespace TrackrAPI.Services.GestionExpediente;

public class ExpedienteTratamientoService
{
    private readonly IExpedienteTratamientoRepository expedienteTratamientoRepository;
    private readonly IExpedienteTrackrRepository expedienteTrackrRepository;
    private readonly SftpService _sftpService;
    private readonly string defaultPath = Path.Combine("Archivos", "Tratamiento", "placeholder.png");

    public ExpedienteTratamientoService(
        IExpedienteTratamientoRepository expedienteTratamientoRepository,
        IExpedienteTrackrRepository expedienteTrackrRepository,
        SftpService sftpService)
    {
        this.expedienteTratamientoRepository = expedienteTratamientoRepository;
        this.expedienteTrackrRepository = expedienteTrackrRepository;
        _sftpService = sftpService;
    }

    public IEnumerable<ExpedienteTratamientoGridDTO> ConsultarParaGrid(int idUsuario)
    {
        var expedienteTratamientos = expedienteTratamientoRepository.ConsultarParaGrid(idUsuario);

        var expedienteTratamientosDto = expedienteTratamientos.Select(et => new ExpedienteTratamientoGridDTO
        {
            IdExpedienteTratamiento = et.IdExpedienteTratamiento,
            Farmaco = et.Farmaco,
            Cantidad = et.Cantidad,
            Unidad = et.Unidad,
            Indicaciones = et.Indicaciones,
            Padecimiento = et.IdPadecimientoNavigation?.Nombre ?? string.Empty,
            FechaRegistro = et.FechaRegistro,
            Dias = et.TratamientoRecordatorio != null ? String.Join(" - ", this.ObtenerGruposDiasPadecimiento(et).Select(r => r.Dia)) : "",
            Horas = et.TratamientoRecordatorio != null ? String.Join(" - ", this.ObtenerGruposDiasPadecimiento(et).Select(r => r.Hora)) : ""
        });

        return expedienteTratamientosDto;
    }

    private IEnumerable<PadecimientoDiasDTO> ObtenerGruposDiasPadecimiento(ExpedienteTratamiento et)
    {
        return et.TratamientoRecordatorio.GroupBy(h => h.Hora)
                                  .Select(tr => new PadecimientoDiasDTO
                                  {
                                      Hora = FormatearHora(tr.Key),
                                      Dia = String.Join(", ", tr.Select(r =>
                                      {
                                          if (r.Dia == 1)
                                          {
                                              return "Lu";
                                          }
                                          else if (r.Dia == 2)
                                          {
                                              return "Ma";
                                          }
                                          else if (r.Dia == 3)
                                          {
                                              return "Mi";
                                          }
                                          else if (r.Dia == 4)
                                          {
                                              return "Ju";
                                          }
                                          else if (r.Dia == 5)
                                          {
                                              return "Vi";
                                          }
                                          else if (r.Dia == 6)
                                          {
                                              return "Sa";
                                          }
                                          else if (r.Dia == 7)
                                          {
                                              return "Do";
                                          }
                                          return "";
                                      }).ToList())
                                  }).ToList() ;
    }

    private string FormatearHora(TimeSpan hora)
    {
        DateTime dateTime = DateTime.Today.Add(hora);
        string formattedTime = dateTime.ToString("h:mm tt");
        return formattedTime;
    }

    // Consultar Tratamientos
    public IEnumerable<ExpedienteTratamientoDetalleDto> ConsultarTratamientos(int idUsuario)
    {
        var expedienteTratamientos = expedienteTratamientoRepository.ConsultarTratamientos(idUsuario);

        return expedienteTratamientos.Select(et =>
            {

                // RecordatorioActivo
                bool recordatorioActivo = et.TratamientoRecordatorio.Any(tr => tr.Activo);

                // DiaSemana: Byte
                var activeRecordatorios = et.TratamientoRecordatorio.Where(tr => tr.Activo).ToList(); // Filtar recordatorios activos

                var distinctDays = activeRecordatorios.Select(tr => tr.Dia).Distinct().ToArray();  // Obtener dias distintos

                bool[] diaSemana = new bool[7];
                foreach (var day in distinctDays)
                {
                    diaSemana[day - 1] = true;
                }

                // Horas: Extrear las horas que comparten todos los dias activos
                var horas = activeRecordatorios.GroupBy(tr => tr.Hora)
                                                .Where(group => group.Count() == distinctDays.Length)
                                                .OrderBy(group => group.Key)
                                                .Select(group => group.Key)
                                                .ToArray();

                // Bitacora
                var bitacora = et.TratamientoRecordatorio
                        .SelectMany(tr => tr.TratamientoToma)
                        .Where(tt => tt.FechaToma.HasValue)
                        .OrderByDescending(tt => tt.FechaToma.Value)
                        .Select(tt => tt.FechaToma.Value)
                        .ToArray();

                return new ExpedienteTratamientoDetalleDto
                {
                    IdExpedienteTratamiento = et.IdExpedienteTratamiento,
                    IdExpediente = et.IdExpediente,
                    Farmaco = et.Farmaco,
                    Cantidad = et.Cantidad,
                    FechaRegistro = et.FechaRegistro,
                    Unidad = et.Unidad,
                    Indicaciones = et.Indicaciones,
                    Padecimiento = et.IdPadecimientoNavigation.Nombre,
                    ImagenBase64 = (et.Imagen != null && et.Imagen.Length > 0) ? System.Convert.ToBase64String(et.Imagen) : "",

                    RecordatorioActivo = recordatorioActivo,
                    DiaSemana = diaSemana,
                    Horas = horas,
                    Bitacora = bitacora

                };
            });
    }

    // Consultar Tratamientos para la app movil
    public IEnumerable<ExpedienteTratamientoPerfilDto> ConsultarTratamientosTrackr(int idUsuario)
    {
        var expedienteTratamientos = expedienteTratamientoRepository.ConsultarTratamientos(idUsuario);
        var expedienteTratamientosDto = expedienteTratamientos.Select(et => new ExpedienteTratamientoPerfilDto
        {
            IdExpedienteTratamiento = et.IdExpedienteTratamiento,
            Farmaco = et.Farmaco,
            Cantidad = et.Cantidad,
            Unidad = et.Unidad,
            Padecimiento = et.IdPadecimientoNavigation.Nombre,
            ImagenBase64 = _sftpService.DownloadFileAsBase64(et.ArchivoUrl ?? defaultPath),
            TipoMime = et.ImagenTipoMime ?? "image/png"
        });

        return expedienteTratamientosDto;
    }

    // Consultar Detalle Tratamiento
    public ExpedienteTratamientoDetalleDto ConsultarTratamientoParaDetalle(int idExpedienteTratamiento)
    {
        var expedienteTratamiento = expedienteTratamientoRepository.ConsultarTratamiento(idExpedienteTratamiento);


        bool recordatorioActivo = expedienteTratamiento.TratamientoRecordatorio.Any(tr => tr.Activo);
        // DiaSemana: Byte
        var activeRecordatorios = expedienteTratamiento.TratamientoRecordatorio.Where(tr => tr.Activo).ToList(); // Filtar recordatorios activos

        var distinctDays = activeRecordatorios.Select(tr => tr.Dia).Distinct().ToArray();  // Obtener dias distintos

        bool[] diaSemana = new bool[7];
        foreach (var day in distinctDays)
        {
            diaSemana[day - 1] = true;
        }

        var expedienteTratamientoDto = new ExpedienteTratamientoDetalleDto
        {
            
        };

        var horas = activeRecordatorios.GroupBy(tr => tr.Hora)
                                            .Where(group => group.Count() == distinctDays.Length)
                                            .OrderBy(group => group.Key)
                                            .ToArray();

        // Bitacora
        var bitacora = expedienteTratamiento.TratamientoRecordatorio
                .SelectMany(tr => tr.TratamientoToma)
                .Where(tt => tt.FechaToma.HasValue)
                .OrderByDescending(tt => tt.FechaToma.Value)
                .Select(tt => tt.FechaToma.Value)
                .ToArray();

        return new ExpedienteTratamientoDetalleDto
        {
            IdExpedienteTratamiento = expedienteTratamiento.IdExpedienteTratamiento,
            IdExpediente = expedienteTratamiento.IdExpediente,
            Farmaco = expedienteTratamiento.Farmaco,
            Cantidad = expedienteTratamiento.Cantidad,
            FechaRegistro = expedienteTratamiento.FechaRegistro,
            FechaInicio = expedienteTratamiento.FechaInicio,
            FechaFin = expedienteTratamiento.FechaFin,
            Unidad = expedienteTratamiento.Unidad,
            Indicaciones = expedienteTratamiento.Indicaciones,
            Padecimiento = expedienteTratamiento.IdPadecimientoNavigation.Nombre,
            IdPadecimiento = expedienteTratamiento.IdPadecimiento,
            IdUsuarioDoctor = expedienteTratamiento.IdUsuarioDoctor,
            ImagenBase64 = _sftpService.DownloadFileAsBase64(expedienteTratamiento.ArchivoUrl ?? defaultPath),
            NombreDoctor = expedienteTratamiento.IdUsuarioDoctorNavigation.Nombre,
            ApellidosDoctor = expedienteTratamiento.IdUsuarioDoctorNavigation.ApellidoPaterno + " " + expedienteTratamiento.IdUsuarioDoctorNavigation.ApellidoMaterno,
            TituloDoctor = expedienteTratamiento.IdUsuarioDoctorNavigation.IdTituloAcademicoNavigation.Nombre,
            RecordatorioActivo = recordatorioActivo,
            DiaSemana = diaSemana,
            Horas = horas.Select(group => group.Key).ToArray(),
            Bitacora = bitacora,
            TipoMime = expedienteTratamiento.ImagenTipoMime ?? "image/png"

        };
    }

    public IEnumerable<ExpedienteSelectorDto> SelectorDeDoctor(){
        return expedienteTratamientoRepository.SelectorDeDoctor();
    }

    public IEnumerable<ExpedienteSelectorDto> SelectorDePadecimiento(int idUsuario)
    {
        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        {
            var i = expedienteTratamientoRepository.SelectorDePadecimiento(idUsuario);
            scope.Complete();
            return i;

        }
    }

    private ExpedienteTratamiento MapearTratamiento(ExpedienteTratamientoDetalleDto expedienteTratamientoDto)
    {
        return new ExpedienteTratamiento
        {
            IdExpediente = expedienteTratamientoDto.IdExpediente,
            Farmaco = expedienteTratamientoDto.Farmaco,
            FechaRegistro = DateTime.Now,
            Cantidad = expedienteTratamientoDto.Cantidad,
            Unidad = expedienteTratamientoDto.Unidad,
            Indicaciones = expedienteTratamientoDto.Indicaciones,
            IdPadecimiento = expedienteTratamientoDto.IdPadecimiento,
            IdUsuarioDoctor = expedienteTratamientoDto.IdUsuarioDoctor,
            Imagen = Convert.FromBase64String(expedienteTratamientoDto.ImagenBase64),
            FechaInicio = expedienteTratamientoDto.FechaInicio,
            FechaFin = expedienteTratamientoDto.FechaFin,
            
        };
    }

    // Agregar Tratamiento
    public int Agregar(ExpedienteTratamientoDetalleDto expedienteTratamientoDto, int idUsuario)
    {
        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        {
            // Crear ExpedienteTratamiento
            ExpedienteTratamiento expedienteTratamiento = MapearTratamiento(expedienteTratamientoDto);

            int idExpediente = expedienteTrackrRepository.ConsultarPorUsuario(idUsuario).IdExpediente;
            expedienteTratamiento.IdExpediente = idExpediente; // Asignar IdExpediente del usuario

            var remoteImagePath = Path.Combine("Archivos", "Tratamiento", $"{Guid.NewGuid()}.png");
            expedienteTratamiento.ArchivoUrl = remoteImagePath;

            var imagenToBase64 = Convert.ToBase64String(expedienteTratamiento.Imagen);
            _sftpService.UploadBytesFile(remoteImagePath, imagenToBase64);

            int idExpedienteTratamiento = expedienteTratamientoRepository.Agregar(expedienteTratamiento);

            // Crear una entrada TratamientoRecordatorio por cada dia y por cada hora
            List<TratamientoRecordatorio> recordatorios = new List<TratamientoRecordatorio>();
            for (int i = 0; i < expedienteTratamientoDto.DiaSemana?.Length; i++)
            {
                if (expedienteTratamientoDto.DiaSemana[i])
                {
                    foreach (var hour in expedienteTratamientoDto.Horas)
                    {
                        var recordatorio = new TratamientoRecordatorio
                        {
                            IdExpedienteTratamiento = idExpedienteTratamiento,
                            Dia = (byte)(i + 1),
                            Hora = hour,
                            Activo = true
                        };
                        recordatorios.Add(recordatorio);
                    }
                }
            }

            expedienteTratamientoRepository.AgregarRecordatorios(recordatorios);

            scope.Complete();

            return idExpedienteTratamiento;
        }
    }
    public void EliminarTratamiento(int idExpedienteTratamiento)
    {
        var expedienteTratamientoExistente = expedienteTratamientoRepository.ConsultarTratamiento(idExpedienteTratamiento);

        if(expedienteTratamientoExistente is not null)
        {
            var tratamientoRecordatorios = expedienteTratamientoExistente.TratamientoRecordatorio.ToList();

            tratamientoRecordatorios.ForEach(tr =>
            {
                expedienteTratamientoRepository.EliminarTratamientoTomas(tr.TratamientoToma);
            });
            expedienteTratamientoRepository.EliminarRecordatorios(tratamientoRecordatorios);
            expedienteTratamientoRepository.Eliminar(expedienteTratamientoExistente);
        }
    }


    public int EditarTratamiento(ExpedienteTratamientoDetalleDto expedienteTratamientoDto, int idUsuario)
    {
        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        {
            // Crear ExpedienteTratamiento
            ExpedienteTratamiento expedienteTratamiento = MapearTratamiento(expedienteTratamientoDto);

            expedienteTratamiento.IdExpedienteTratamiento = expedienteTratamientoDto.IdExpedienteTratamiento;
            int idExpedienteTratamiento = expedienteTratamiento.IdExpedienteTratamiento;

            var expedienteTratamientoExistente = expedienteTratamientoRepository.ConsultarTratamiento(idExpedienteTratamiento);
            expedienteTratamiento.IdExpediente = expedienteTratamientoExistente.IdExpediente;
            expedienteTratamiento.FechaRegistro = expedienteTratamientoExistente.FechaRegistro;

            expedienteTratamientoRepository.Editar(expedienteTratamiento);



            bool recordatorioActivo = expedienteTratamientoExistente.TratamientoRecordatorio.Any(tr => tr.Activo);
            // DiaSemana: Byte
            var activeRecordatorios = expedienteTratamientoExistente.TratamientoRecordatorio.Where(tr => tr.Activo).ToList(); // Filtar recordatorios activos

            var distinctDays = activeRecordatorios.Select(tr => tr.Dia).Distinct().ToArray();  // Obtener dias distintos

            bool[] diaSemana = new bool[7];
            foreach (var day in distinctDays)
            {
                diaSemana[day - 1] = true;
            }

            var horas = activeRecordatorios.GroupBy(tr => tr.Hora)
                                                .Where(group => group.Count() == distinctDays.Length)
                                                .OrderBy(group => group.Key)
                                                .Select(group => group.Key)
                                                .ToArray();


            //Recordatorios de la peticion
            List<TratamientoRecordatorio> recordatorios = new List<TratamientoRecordatorio>();
            for (int i = 0; i < expedienteTratamientoDto.DiaSemana?.Length; i++)
            {
                if (expedienteTratamientoDto.DiaSemana[i])
                {
                    foreach (var hour in expedienteTratamientoDto.Horas)
                    {
                        var recordatorio = new TratamientoRecordatorio
                        {
                            IdExpedienteTratamiento = expedienteTratamiento.IdExpedienteTratamiento,
                            Dia = (byte)(i + 1),
                            Hora = hour,
                            Activo = true
                        };
                        
                        recordatorios.Add(recordatorio);
                    }
                }
            }

            //Recordatorios ya existentes
            List<TratamientoRecordatorio> recordatoriosExistentes = new List<TratamientoRecordatorio>();
            for (int i = 0; i < diaSemana.Length; i++)
            {
                if (diaSemana[i])
                {
                    foreach (var hour in horas)
                    {
                        var recordatorio = new TratamientoRecordatorio
                        {
                            IdExpedienteTratamiento = expedienteTratamiento.IdExpedienteTratamiento,
                            Dia = (byte)(i + 1),
                            Hora = hour,
                            Activo = true
                        };

                        recordatoriosExistentes.Add(recordatorio);
                    }
                }
            }

            // Obtener recordatorios que est�n en la BD pero no en la petici�n
            var recordatoriosEliminados = recordatoriosExistentes
                .Where(existingRecordatorio =>
                    !recordatorios.Any(peticionRecordatorio =>
                        peticionRecordatorio.Dia == existingRecordatorio.Dia &&
                        peticionRecordatorio.Hora == existingRecordatorio.Hora))
                .ToList();


            // Obtener recordatorios que est�n en la petici�n pero no en la BD
            var recordatoriosAgregados = recordatorios
                .Where(nuevoRecordatorio =>
                    !recordatoriosExistentes.Any(existingRecordatorio =>
                        existingRecordatorio.Dia == nuevoRecordatorio.Dia &&
                        existingRecordatorio.Hora == nuevoRecordatorio.Hora))
                .ToList();


            recordatoriosEliminados.ForEach(recordatorioEliminar =>
            {
                var recordatorio = activeRecordatorios.FirstOrDefault(r => r.Dia == recordatorioEliminar.Dia && r.Hora == recordatorioEliminar.Hora);
                if (recordatorio != null)
                {
                    recordatorioEliminar.IdTratamientoRecordatorio = recordatorio.IdTratamientoRecordatorio;
                    var tratamientoTomas = expedienteTratamientoRepository.ConsultarTratamientoTomas(recordatorioEliminar.IdTratamientoRecordatorio);
                    expedienteTratamientoRepository.EliminarTratamientoTomas(tratamientoTomas);
                }
            });

            expedienteTratamientoRepository.AgregarRecordatorios(recordatoriosAgregados);
            expedienteTratamientoRepository.EliminarRecordatorios(recordatoriosEliminados);

            scope.Complete();

            return idExpedienteTratamiento;
        }
    }

}

