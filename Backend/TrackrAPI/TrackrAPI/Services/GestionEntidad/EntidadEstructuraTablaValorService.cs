using System.Transactions;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Globalization;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Services.Notificaciones;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.GestionEntidad
{
    public class EntidadEstructuraTablaValorService
    {
        private readonly IEntidadEstructuraTablaValorRepository entidadEstructuraTablaValorRepository;
        private readonly ISeccionCampoRepository seccionCampoRepository;
        private readonly IExpedientePadecimientoRepository expedientePadecimientoRepository;
        private readonly IEntidadEstructuraRepository entidadEstructuraRepository;
        private readonly NotificacionDoctorService _notificacionDoctorService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDominioHospitalRepository _dominioHospitalRepository;
        private readonly IDominioRepository _dominioRepository;

        public EntidadEstructuraTablaValorService(
            IEntidadEstructuraTablaValorRepository entidadEstructuraTablaValorRepository,
            ISeccionCampoRepository seccionCampoRepository,
            IExpedientePadecimientoRepository expedientePadecimientoRepository,
            IEntidadEstructuraRepository entidadEstructuraRepository,
            NotificacionDoctorService notificacionDoctorService,
            IUsuarioRepository usuarioRepository,
            IDominioHospitalRepository dominioHospitalRepository,
            IDominioRepository dominioRepository
            )
        {
            this.entidadEstructuraTablaValorRepository = entidadEstructuraTablaValorRepository;
            this.seccionCampoRepository = seccionCampoRepository;
            this.expedientePadecimientoRepository = expedientePadecimientoRepository;
            this.entidadEstructuraRepository = entidadEstructuraRepository;
            _notificacionDoctorService = notificacionDoctorService;
            _usuarioRepository = usuarioRepository;
            _dominioHospitalRepository = dominioHospitalRepository;
            _dominioRepository = dominioRepository;
        }

        public List<RegistroTablaDto> ConsultarRegistroTablaPorTabulacion(int idEntidadEstructura, int idTabla)
        {
            return entidadEstructuraTablaValorRepository
                .ConsultarPorTabulacion(idEntidadEstructura, idTabla)
                .GroupBy(e => e.IdEntidadEstructura)
                .SelectMany(tabla =>
                {
                    return tabla.GroupBy(e => e.Numero)
                        .Select(registro => new RegistroTablaDto()
                        {
                            IdRegistroTabla = registro.Key,
                            IdEntidadEstructura = tabla.First().IdEntidadEstructura,
                            Valores = registro.Where(ev => ev.Numero == registro.Key).ToList()
                        });
                })
                .OrderBy(r => r.IdRegistroTabla)
                .ToList();
        }

        public List<RegistroTablaDto> ConsultarPorPestanaSeccion(int idEntidadEstructura, int idTabla)
        {
            return entidadEstructuraTablaValorRepository
                .ConsultarPorPestanaSeccion(idEntidadEstructura, idTabla)
                .GroupBy(e => e.Numero)
                .Select(registro =>
                {
                    return new RegistroTablaDto()
                    {
                        IdRegistroTabla = registro.Key,
                        IdEntidadEstructura = registro.First().IdEntidadEstructura,
                        Valores = registro.Where(ev => ev.Numero == registro.Key).ToList()
                    };
                })
                .OrderBy(r => r.IdRegistroTabla)
                .ToList();
        }


        public void Agregar(EntidadTablaRegistroDto registro)
        {
            using var ts = new TransactionScope();

            var ultimoRegistro = entidadEstructuraTablaValorRepository.ConsultarUltimoRegistro(registro.IdEntidadEstructura, registro.IdTabla);

            foreach (var valorDto in registro.Valores)
            {
                var valor = new EntidadEstructuraTablaValor()
                {
                    IdEntidadEstructura = registro.IdEntidadEstructura,
                    IdTabla = registro.IdTabla,
                    Numero = ultimoRegistro + 1,
                    IdSeccion = valorDto.IdSeccionVariable,
                    Valor = valorDto.Valor,
                    FueraDeRango = valorDto.FueraDeRango,
                    FechaMuestra = valorDto.FechaMuestra
                };

                entidadEstructuraTablaValorRepository.Agregar(valor);
            }

            ts.Complete();
        }

        public async Task AgregarMuestra(TablaValorMuestraDTO[] muestraDTO, int idUsuario)
        {
            using var ts = new TransactionScope();
         
            
            foreach (var muestra in muestraDTO)
            {
                    var idSeccion = seccionCampoRepository.Consultar(muestra.IdSeccionVariable).IdSeccion;
                    var IdEntidadEstructuraHijo = entidadEstructuraRepository.ConsultarPorEntidadSeccionVariable(idSeccion).IdEntidadEstructuraPadre;
                    
                
                var muestraAAgregar = new EntidadEstructuraTablaValor()
                {
                    IdEntidadEstructura = (int) IdEntidadEstructuraHijo,
                    IdSeccion = muestra.IdSeccionVariable,
                    IdTabla = idUsuario,
                    Valor = muestra.Valor,
                    FechaMuestra = muestra.FechaMuestra,
                    FueraDeRango = muestra.FueraDeRango
                };

                if ((bool)muestraAAgregar.FueraDeRango)
                {
                    List<int> idsDoctores =  this.expedientePadecimientoRepository.ConsultarIdsDoctorPorUsuario(idUsuario);
                    string nombreVariable = this.seccionCampoRepository.Consultar(muestraAAgregar.IdSeccion).Descripcion;
                    string nombrePaciente  = _usuarioRepository.ConsultarDto(idUsuario).Nombre;
                    var notificacion = new NotificacionDoctorCapturaDTO(

                        "El paciente " + nombrePaciente + " ha registrado un valor fuera de rango en la variable " + nombreVariable + ".",
                        4,
                        idUsuario,
                        idUsuario,
                        null
                    );
                    await _notificacionDoctorService.Notificar(notificacion, idsDoctores);
                }
                var ultimoRegistro = entidadEstructuraTablaValorRepository.ConsultarUltimoRegistro(muestraAAgregar.IdEntidadEstructura, idUsuario);
                muestraAAgregar.Numero = ultimoRegistro + 1;
                entidadEstructuraTablaValorRepository.Agregar(muestraAAgregar);

            }
            ts.Complete();
        }

        private bool EstaFueraDeRango(EntidadEstructuraTablaValor valorDb , int idHospital)
        {
            var seccionCampo = seccionCampoRepository.Consultar(valorDb.IdSeccion);
            var dominioHospital = _dominioHospitalRepository.Consultar(idHospital , seccionCampo.IdDominio);
            decimal? valorMaximo;
            decimal? valorMinimo;

            if(dominioHospital == null)
            {
                var dominio = _dominioRepository.Consultar(seccionCampo.IdDominio);
                valorMaximo = dominio.ValorMaximo;
                valorMinimo = dominio.ValorMinimo;
            }else{
                valorMaximo = dominioHospital.ValorMaximo;
                valorMinimo = dominioHospital.ValorMinimo;
            }

            return int.Parse(valorDb.Valor) > valorMaximo || int.Parse(valorDb.Valor) < valorMinimo;
        }


        public void Editar(EntidadTablaRegistroDto registro, int idUsuario)
        {
            using var ts = new TransactionScope();

            var usuarioSesion = _usuarioRepository.ConsultarDto(idUsuario);
            var valoresDto = registro.Valores;

            foreach (var valorDto in valoresDto)
            {
                var valorDb = entidadEstructuraTablaValorRepository.ConsultarPorId(valorDto.IdEntidadEstructuraTablaValor);


                valorDb.Valor = valorDto.Valor;
                valorDb.FueraDeRango = EstaFueraDeRango(valorDb, usuarioSesion.IdHospital);

                entidadEstructuraTablaValorRepository.Editar(valorDb);

            }

            ts.Complete();
        }

        public void Eliminar(EntidadTablaRegistroDto registro)
        {
            using var ts = new TransactionScope();

            var valores = registro.Valores;

            foreach (var valor in valores)
            {
                var valorDb = entidadEstructuraTablaValorRepository.ConsultarPorId(valor.IdEntidadEstructuraTablaValor);
                if(valorDb != null){
                    entidadEstructuraTablaValorRepository.Eliminar(valorDb);
                }
            }

            ts.Complete();
        }

        public IEnumerable<ValoresFueraRangoGridDTO> ConsultarValores(int idPadecimiento, int idUsuario, bool? fueraRango)
        {
            var columnas = this.seccionCampoRepository.ConsultarSeccionesPadecimientos(idPadecimiento);
            var clavesCampos = columnas.Select(c => c.IdSeccionVariable).ToList();

            var valores = entidadEstructuraTablaValorRepository.ConsultarValoresPorCampos(idUsuario, clavesCampos, fueraRango);

            var padecimientos = new List<ValoresFueraRangoGridDTO>();

            foreach (var valor in valores)
            {
                var columnaCorrespondiente = columnas.FirstOrDefault(c => c.IdSeccionVariable == valor.IdSeccion);

                if (columnaCorrespondiente != null)
                {
                    string valorReferencia = "";
                    if (columnaCorrespondiente.ValorMinimo != null || columnaCorrespondiente.ValorMaximo != null)
                    {
                        valorReferencia = columnaCorrespondiente.ValorMinimo.ToString() + " - " + columnaCorrespondiente.ValorMaximo.ToString();
                    }
                    padecimientos.Add(new ValoresFueraRangoGridDTO
                    {
                        NombrePadecimiento = valor.IdEntidadEstructuraNavigation.Nombre,
                        Variable = columnaCorrespondiente.Variable,
                        Parametro = columnaCorrespondiente.Parametro,
                        FechaHora = valor.FechaMuestra,
                        ValorRegistrado = valor.Valor,
                        ValorReferencia = valorReferencia
                    });
                }
            }

            return padecimientos;
        }

        public IEnumerable<ValoresFueraRangoGridDTO> ConsultarValoresFueraRangoGeneral(int idUsuario)
        {
            var valoresFueraRangoGridDTOs = new List<ValoresFueraRangoGridDTO>();
            var padecimientos = expedientePadecimientoRepository.ConsultarPorUsuario(idUsuario);

            if (padecimientos == null)
            {
                return valoresFueraRangoGridDTOs;
            }
            foreach (var padecimiento in padecimientos)
            {
                valoresFueraRangoGridDTOs.AddRange(ConsultarValores(padecimiento.IdPadecimiento, idUsuario, true).Distinct());
            }
            // Filtrar duplicados en función de propiedades específicas
            valoresFueraRangoGridDTOs = valoresFueraRangoGridDTOs
                .GroupBy(dto => new { dto.NombrePadecimiento, dto.Variable, dto.Parametro, dto.FechaHora, dto.ValorRegistrado })
                .Select(group => group.First())
                .ToList();
            return valoresFueraRangoGridDTOs;
        }

        public Dictionary<string, List<ValoresHistogramaDTO>> ConsultarValoresPorClaveCampo(int idSeccionVariable, int idUsuario, string fechaFiltro)
        {
            DateTime fecha = DateTime.Now;

            switch (fechaFiltro.ToLower())
            {
                case "hoy":
                    fecha = fecha.AddHours(-24); // Desde las últimas 24 horas
                    var valoresHoy = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, fecha);
                    return AgruparPorHoy(valoresHoy);
                case "1 semana":
                    fecha = fecha.AddDays(-7); // Desde los últimos 7 días
                    var valoresSemana = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, fecha);
                    return AgruparPorSemana(valoresSemana);
                case "2 semanas":
                    fecha = fecha.AddDays(-14); // Desde los últimos 14 días
                    var valoresDosSemanas = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, fecha);
                    return AgruparPorSemana(valoresDosSemanas);
                case "3 semanas":
                    fecha = fecha.AddDays(-21); // Desde las últimas 3 semanas
                    var valoresTresSemanas = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, fecha);
                    return AgruparPorSemana(valoresTresSemanas);
                case "1 mes":
                    fecha = fecha.AddMonths(-1); // Desde el último mes
                    var valoresUnMes = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, fecha);
                    return AgruparPorSemana(valoresUnMes);
                case "2 meses":
                    fecha = fecha.AddMonths(-2); // Desde los últimos 2 meses
                    var valoresDosMeses = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, fecha);
                    return AgruparPorSemana(valoresDosMeses);
                default:
                    throw new CdisException("Filtro de fecha no reconocido");
            }
        }

        public ValoresPorCampoGridDTO ConsultarValoresPorClaveCampoParaGrid(int idSeccionVariable, int idUsuario, string fechaFiltro)
        {
            DateTime fecha = DateTime.Now;
            var valoresGrid = new ValoresPorCampoGridDTO(){ 
                unidadMedida = seccionCampoRepository.ConsultarUnidadDeMedidaPorClaveCampo(idSeccionVariable) };

            switch (fechaFiltro.ToLower())
            {
                case "hoy":
                    fecha = fecha.AddHours(-24); // Desde las últimas 24 horas
                    valoresGrid.valores = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, fecha);
                    return valoresGrid;
                case "1 semana":
                    fecha = fecha.AddDays(-7); // Desde los últimos 7 días
                    valoresGrid.valores = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, fecha);
                    return valoresGrid;
                case "2 semanas":
                    fecha = fecha.AddDays(-14); // Desde los últimos 14 días
                    valoresGrid.valores = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, fecha);
                    return valoresGrid;
                case "3 semanas":
                    fecha = fecha.AddDays(-21); // Desde las últimas 3 semanas
                    valoresGrid.valores = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, fecha);
                    return valoresGrid;
                case "1 mes":
                    fecha = fecha.AddDays(-30); // Desde el último mes
                    valoresGrid.valores = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, fecha);
                    return valoresGrid;
                case "2 meses":
                    fecha = fecha.AddDays(-60); // Desde los últimos 2 meses
                    valoresGrid.valores = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(idSeccionVariable, idUsuario, fecha);
                    return valoresGrid;
                default:
                    throw new CdisException("Filtro de fecha no reconocido");
            }
        }

        public static int ObtenerSemanaDelAnio(DateTime fecha)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(fecha);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                fecha = fecha.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(fecha, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public Dictionary<string, List<ValoresHistogramaDTO>> AgruparPorSemana(IEnumerable<ValoresHistogramaDTO> valores)
        {
            var diasDeLaSemana = new[] { "Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa" };
            return valores.GroupBy(v => diasDeLaSemana[(int)v.FechaMuestra.GetValueOrDefault().DayOfWeek])
                          .ToDictionary(g => g.Key, g => g.ToList());
        }
        public Dictionary<string, List<ValoresHistogramaDTO>> AgruparPorHoy(IEnumerable<ValoresHistogramaDTO> valores)
        {
            return valores.GroupBy(v => $"{v.FechaMuestra.GetValueOrDefault().Hour / 3 * 3}:00 - {(v.FechaMuestra.GetValueOrDefault().Hour / 3 + 1) * 3}:00")
                          .ToDictionary(g => g.Key, g => g.ToList());
        }

        public IEnumerable<ExpedienteMuestrasGridDTO> ConsultarGridMuestras(int idUsuario)
        {
            return entidadEstructuraTablaValorRepository.ConsultarGridMuestras(idUsuario);
        }


    }
}
