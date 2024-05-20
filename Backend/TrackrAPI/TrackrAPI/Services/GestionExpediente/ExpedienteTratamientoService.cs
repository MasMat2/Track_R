using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;
using System.Transactions;

namespace TrackrAPI.Services.GestionExpediente;

public class ExpedienteTratamientoService
{
    private readonly IExpedienteTratamientoRepository expedienteTratamientoRepository;
    private readonly IExpedienteTrackrRepository expedienteTrackrRepository;

    public ExpedienteTratamientoService(
        IExpedienteTratamientoRepository expedienteTratamientoRepository,
        IExpedienteTrackrRepository expedienteTrackrRepository)
    {
        this.expedienteTratamientoRepository = expedienteTratamientoRepository;
        this.expedienteTrackrRepository = expedienteTrackrRepository;
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
            Dias = et.TratamientoRecordatorio != null ? String.Join('-', this.ObtenerGruposDiasPadecimiento(et).Select(r => r.Dia)) : "",
            Horas = et.TratamientoRecordatorio != null ? String.Join('-', this.ObtenerGruposDiasPadecimiento(et).Select(r => r.Hora)) : ""
        });

        return expedienteTratamientosDto;
    }

    private IEnumerable<PadecimientoDiasDTO> ObtenerGruposDiasPadecimiento(ExpedienteTratamiento et)
    {
        return et.TratamientoRecordatorio.GroupBy(h => h.Hora)
                                  .Select(tr => new PadecimientoDiasDTO
                                  {
                                      Hora = tr.Key.ToString(),
                                      Dia = String.Join(",", tr.Select(r =>
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

    // Consultar Tratamientos
    public IEnumerable<ExpedienteTratamientoDto> ConsultarTratamientos(int idUsuario)
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

                return new ExpedienteTratamientoDto
                {
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

    public IEnumerable<ExpedienteSelectorDto> SelectorDeDoctor()
    {
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

    private ExpedienteTratamiento MapearTratamiento(ExpedienteTratamientoDto expedienteTratamientoDto)
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
        };
    }

    // Agregar Tratamiento
    public int Agregar(ExpedienteTratamientoDto expedienteTratamientoDto, int idUsuario)
    {
        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        {
            // Crear ExpedienteTratamiento
            ExpedienteTratamiento expedienteTratamiento = MapearTratamiento(expedienteTratamientoDto);

            int idExpediente = expedienteTrackrRepository.ConsultarPorUsuario(idUsuario).IdExpediente;
            expedienteTratamiento.IdExpediente = idExpediente; // Asignar IdExpediente del usuario

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

}

