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

namespace TrackrAPI.Services.GestionEntidad
{
    public class EntidadEstructuraTablaValorService
    {
        private readonly IEntidadEstructuraTablaValorRepository entidadEstructuraTablaValorRepository;
        private readonly ISeccionCampoRepository seccionCampoRepository;
        private readonly IExpedientePadecimientoRepository expedientePadecimientoRepository;
        private readonly IEntidadEstructuraRepository entidadEstructuraRepository;

        public EntidadEstructuraTablaValorService(
            IEntidadEstructuraTablaValorRepository entidadEstructuraTablaValorRepository,
            ISeccionCampoRepository seccionCampoRepository,
            IExpedientePadecimientoRepository expedientePadecimientoRepository,
            IEntidadEstructuraRepository entidadEstructuraRepository
            )
        {
            this.entidadEstructuraTablaValorRepository = entidadEstructuraTablaValorRepository;
            this.seccionCampoRepository = seccionCampoRepository;
            this.expedientePadecimientoRepository = expedientePadecimientoRepository;
            this.entidadEstructuraRepository = entidadEstructuraRepository;
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
                    ClaveCampo = valorDto.ClaveCampo,
                    Valor = valorDto.Valor,
                    FueraDeRango = valorDto.FueraDeRango
                };

                entidadEstructuraTablaValorRepository.Agregar(valor);
            }

            ts.Complete();
        }

        public void AgregarMuestra(TablaValorMuestraDTO[] muestraDTO, int idUsuario)
        {
            using var ts = new TransactionScope();
            var entidadEstructuraMuestra = entidadEstructuraRepository.ConsultarPorClave(GeneralConstant.ClaveEntidadEstructuraMuestra);
            if(entidadEstructuraMuestra == null)
            {
                throw new CdisException("No existe la entidad estructura con clave 006");
            }
            var entidadEstructuraMuestraHijo = entidadEstructuraRepository.ConsultarHijos(entidadEstructuraMuestra.IdEntidadEstructura).FirstOrDefault();
            if (entidadEstructuraMuestra == null)
            {
                throw new CdisException("No existe la entidad estructura hijo de la entidad estructura con clave 006");
            }
            foreach (var muestra in muestraDTO){

                var muestraAAgregar = new EntidadEstructuraTablaValor()
                {
                    IdEntidadEstructura = entidadEstructuraMuestraHijo.IdEntidadEstructura,
                    ClaveCampo = muestra.ClaveCampo,
                    IdTabla = idUsuario,
                    Valor = muestra.Valor,
                    FechaMuestra = muestra.FechaMuestra,
                    FueraDeRango = muestra.FueraDeRango
                };
            var ultimoRegistro = entidadEstructuraTablaValorRepository.ConsultarUltimoRegistro(muestraAAgregar.IdEntidadEstructura, idUsuario);
            muestraAAgregar.Numero = ultimoRegistro + 1;
            entidadEstructuraTablaValorRepository.Agregar(muestraAAgregar);

            }
            ts.Complete();
        }

        public void Editar(EntidadTablaRegistroDto registro)
        {
            using var ts = new TransactionScope();

            var valoresDto = registro.Valores;

            var valoresDb = entidadEstructuraTablaValorRepository.ConsultarPorNumeroRegistro(
                registro.IdEntidadEstructura,
                registro.IdTabla,
                registro.Numero);

            var clavesDto = valoresDto.ConvertAll(v => v.ClaveCampo);
            var clavesDb = valoresDb.Select(v => v.ClaveCampo);

            var valoresEditar = valoresDb
                .Where(v => clavesDto.Contains(v.ClaveCampo))
                .ToList();

            foreach (var valor in valoresEditar)
            {
                var valorDto = valoresDto.Find(v => v.ClaveCampo == valor.ClaveCampo)!;

                valor.Valor = valorDto.Valor;
                valor.FueraDeRango = valorDto.FueraDeRango;

                entidadEstructuraTablaValorRepository.Editar(valor);
            }

            var valoresAgregar = valoresDto
                .Where(v => !clavesDb.Contains(v.ClaveCampo))
                .ToList();

            foreach (var valorDto in valoresAgregar)
            {
                var valor = new EntidadEstructuraTablaValor()
                {
                    IdEntidadEstructura = registro.IdEntidadEstructura,
                    IdTabla = registro.IdTabla,
                    Numero = registro.Numero,
                    ClaveCampo = valorDto.ClaveCampo,
                    Valor = valorDto.Valor,
                    FueraDeRango = valorDto.FueraDeRango
                };

                entidadEstructuraTablaValorRepository.Agregar(valor);
            }

            var valoresEliminar = valoresDb
                .Where(v => !clavesDto.Contains(v.ClaveCampo))
                .ToList();

            foreach (var valor in valoresEliminar)
            {
                entidadEstructuraTablaValorRepository.Eliminar(valor);
            }

            ts.Complete();
        }

        public void Eliminar(EntidadTablaRegistroDto registro)
        {
            using var ts = new TransactionScope();

            var valores = entidadEstructuraTablaValorRepository.ConsultarPorNumeroRegistro(
                registro.IdEntidadEstructura,
                registro.IdTabla,
                registro.Numero);

            foreach (var valor in valores)
            {
                entidadEstructuraTablaValorRepository.Eliminar(valor);
            }

            ts.Complete();
        }

        public IEnumerable<ValoresFueraRangoGridDTO> ConsultarValores(int idPadecimiento, int idUsuario, bool? fueraRango)
        {
            var columnas = this.seccionCampoRepository.ConsultarSeccionesPadecimientos(idPadecimiento);
            var clavesCampos = columnas.Select(c => c.ClaveCampo).ToList();

            var valores = entidadEstructuraTablaValorRepository.ConsultarValoresPorCampos(idUsuario, clavesCampos, fueraRango);

            var padecimientos = new List<ValoresFueraRangoGridDTO>();

            foreach (var valor in valores)
            {
                var columnaCorrespondiente = columnas.FirstOrDefault(c => c.ClaveCampo == valor.ClaveCampo);

                if (columnaCorrespondiente != null)
                {
                    string valorReferencia = "";
                    if(columnaCorrespondiente.ValorMinimo != null || columnaCorrespondiente.ValorMaximo != null)
                    {
                        valorReferencia = columnaCorrespondiente.ValorMinimo.ToString() + " - " + columnaCorrespondiente.ValorMaximo.ToString();
                    }
                    padecimientos.Add(new ValoresFueraRangoGridDTO
                    {
                        NombrePadecimiento = valor.IdEntidadEstructuraNavigation.Nombre,
                        Variable = columnaCorrespondiente.Variable,
                        Parametro = columnaCorrespondiente.Parametro,
                        FechaHora = valor.FechaMuestra.ToString(),
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

            if(padecimientos == null)
            {
                return valoresFueraRangoGridDTOs;
            }
            foreach(var padecimiento in padecimientos)
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

        public Dictionary<string, List<ValoresHistogramaDTO>> ConsultarValoresPorClaveCampo(string claveCampo, int idUsuario, string fechaFiltro)
        {
            DateTime fecha = DateTime.Now;

            switch (fechaFiltro.ToLower())
            {
                case "hoy":
                    fecha = fecha.AddHours(-24); // Desde las últimas 24 horas
                    var valoresHoy = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(claveCampo, idUsuario, fecha);
                    return AgruparPorHoy(valoresHoy);
                case "1 semana":
                    fecha = fecha.AddDays(-7); // Desde los últimos 7 días
                    var valoresSemana = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(claveCampo, idUsuario, fecha);
                    return AgruparPorSemana(valoresSemana);
                case "2 semanas":
                    fecha = fecha.AddDays(-14); // Desde los últimos 14 días
                    var valoresDosSemanas = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(claveCampo, idUsuario, fecha);
                    return AgruparPorSemana(valoresDosSemanas);
                case "3 semanas":
                    fecha = fecha.AddDays(-21); // Desde las últimas 3 semanas
                    var valoresTresSemanas = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(claveCampo, idUsuario, fecha);
                    return AgruparPorSemana(valoresTresSemanas);
                case "1 mes":
                    fecha = fecha.AddMonths(-1); // Desde el último mes
                    var valoresUnMes = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(claveCampo, idUsuario, fecha);
                    return AgruparPorSemana(valoresUnMes);
                case "2 meses":
                    fecha = fecha.AddMonths(-2); // Desde los últimos 2 meses
                    var valoresDosMeses = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(claveCampo, idUsuario, fecha);
                    return AgruparPorSemana(valoresDosMeses);
                default:
                    throw new CdisException("Filtro de fecha no reconocido");
            }
        }

        public IEnumerable<ValoresHistogramaDTO> ConsultarValoresPorClaveCampoParaGrid(string claveCampo, int idUsuario, string fechaFiltro)
        {
            DateTime fecha = DateTime.Now;

            switch (fechaFiltro.ToLower())
            {
                case "hoy":
                    fecha = fecha.AddHours(-24); // Desde las últimas 24 horas
                    var valoresHoy = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(claveCampo, idUsuario, fecha);
                    return valoresHoy;
                case "1 semana":
                    fecha = fecha.AddDays(-7); // Desde los últimos 7 días
                    var valoresSemana = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(claveCampo, idUsuario, fecha);
                    return valoresSemana;
                case "2 semanas":
                    fecha = fecha.AddDays(-14); // Desde los últimos 14 días
                    var valoresDosSemanas = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(claveCampo, idUsuario, fecha);
                    return valoresDosSemanas;
                case "3 semanas":
                    fecha = fecha.AddDays(-21); // Desde las últimas 3 semanas
                    var valoresTresSemanas = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(claveCampo, idUsuario, fecha);
                    return valoresTresSemanas;
                case "1 mes":
                    fecha = fecha.AddDays(-30); // Desde el último mes
                    var valoresUnMes = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(claveCampo, idUsuario, fecha);
                    return valoresUnMes;
                case "2 meses":
                    fecha = fecha.AddDays(-60); // Desde los últimos 2 meses
                    var valoresDosMeses = entidadEstructuraTablaValorRepository.ConsultarValoresPorClaveCampo(claveCampo, idUsuario, fecha);
                    return valoresDosMeses;
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

    }
}
