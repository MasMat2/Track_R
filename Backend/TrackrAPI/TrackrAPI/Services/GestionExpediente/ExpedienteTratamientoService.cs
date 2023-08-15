using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;
using System.Transactions;

namespace TrackrAPI.Services.GestionExpediente;
public class ExpedienteTratamientoService
{
    private readonly IExpedienteTratamientoRepository expedienteTratamientoRepository;
    
    public ExpedienteTratamientoService(IExpedienteTratamientoRepository expedienteTratamientoRepository)
    {
        this.expedienteTratamientoRepository = expedienteTratamientoRepository;
    }

    public int Agregar(ExpedienteTratamientoDto expedienteTratamientoDto)
    {
        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        {
            ExpedienteTratamiento expedienteTratamiento = MapearTratamiento(expedienteTratamientoDto);

            int id = expedienteTratamientoRepository.Agregar(expedienteTratamiento);
                        
            // Crear una entrada TratamientoRecordatorio para cada dia y hora
            List<TratamientoRecordatorio> recordatorios = new List<TratamientoRecordatorio>();
            for (int i = 0; i < expedienteTratamientoDto.DiaSemana.Length; i++)
            {
                if (expedienteTratamientoDto.DiaSemana[i])
                {
                    foreach (var hour in expedienteTratamientoDto.Horas)
                    {
                        var recordatorio = new TratamientoRecordatorio
                        {
                            IdExpedienteTratamiento = id,
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
           
            return id;
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
        

    public IEnumerable<ExpedienteTratamientoDto> ConsultarPorUsuario(int idUsuario)
    {
        var expedienteTratamientos = expedienteTratamientoRepository.ConsultarPorUsuario(idUsuario);

        return expedienteTratamientos.Select(et => 
            {
            
                // Filtar recordatorios activos
                var activeRecordatorios = et.TratamientoRecordatorio.Where(tr => tr.Activo).ToList();

                // Obtener dias distintos
                var distinctDays = activeRecordatorios.Select(tr => tr.Dia).Distinct().ToArray();

                // Representar los dias en byte
                bool[] daysOfWeek = new bool[7];
                foreach (var day in distinctDays)
                {
                    daysOfWeek[day - 1] = true;
                }

                // Extrear las horas que existen en todos los dias activos
                var allHours = activeRecordatorios.GroupBy(tr => tr.Hora)
                                                .Where(group => group.Count() == distinctDays.Length)
                                                .OrderByDescending(group => group.Key)
                                                .Select(group => group.Key)
                                                .ToArray();

                // Extrear bitacora
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
                    ImagenBase64  = (et.Imagen != null && et.Imagen.Length > 0) ? System.Convert.ToBase64String(et.Imagen) : "1",

                    
                    RecordatorioActivo = true,
                    DiaSemana = daysOfWeek,
                    Horas = allHours,
                    Bitacora = bitacora
                    
                };
            });

    }

     public IEnumerable<ExpedienteSelectorDto> SelectorDeDoctor(){
        return expedienteTratamientoRepository.SelectorDeDoctor();
     }

     
    public IEnumerable<ExpedienteSelectorDto> SelectorDePadecimiento(int idUsuario){
        return expedienteTratamientoRepository.SelectorDePadecimiento(idUsuario);
     }

}

