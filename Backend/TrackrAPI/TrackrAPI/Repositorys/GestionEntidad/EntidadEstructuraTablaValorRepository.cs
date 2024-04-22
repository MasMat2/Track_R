using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using DocumentFormat.OpenXml.Office2016.Drawing;
using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionEntidad;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public class EntidadEstructuraTablaValorRepository : Repository<EntidadEstructuraTablaValor>, IEntidadEstructuraTablaValorRepository
    {
        public EntidadEstructuraTablaValorRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<EntidadEstructuraTablaValor> ConsultarPorTabulacion(int idEntidadEstructura, int idTabla)
        {
            return context.EntidadEstructuraTablaValor
                    .Where(e => e.IdEntidadEstructuraNavigation.IdEntidadEstructuraPadre == idEntidadEstructura &&
                                e.IdTabla == idTabla);
        }

        public EntidadEstructuraTablaValor ConsultarPorId(int id)
        {
            return context.EntidadEstructuraTablaValor
                .Include(e => e.IdEntidadEstructuraNavigation)
                .FirstOrDefault(e => e.IdEntidadEstructuraTablaValor == id);
        }

        public IEnumerable<EntidadEstructuraTablaValor> ConsultarPorPestanaSeccion(int idEntidadEstructura, int idTabla)
        {
            return context.EntidadEstructuraTablaValor
                .Where(e => e.IdEntidadEstructura == idEntidadEstructura && e.IdTabla == idTabla);
        }

        public IEnumerable<EntidadEstructuraTablaValor> ConsultarPorNumeroRegistro(int idEntidadEstructura, int idTabla, int numeroRegistro)
        {
            return context.EntidadEstructuraTablaValor
                .Where(e => e.IdEntidadEstructura == idEntidadEstructura &&
                    e.IdTabla == idTabla &&
                    e.Numero == numeroRegistro);
        }

        public int ConsultarUltimoRegistro(int idEntidadEstructura, int idTabla)
        {
            var ultimoRegistro = context.EntidadEstructuraTablaValor
                .Where(e => e.IdEntidadEstructura == idEntidadEstructura && e.IdTabla == idTabla)
                .OrderByDescending(e => e.Numero)
                .FirstOrDefault();

            return ultimoRegistro?.Numero ?? 0;
        }

        public string ConsultarUltimoValor(int idUsuario  , int idSeccionVariable)
        {
            return context.EntidadEstructuraTablaValor
                    .Where(eetv => eetv.IdTabla == idUsuario && eetv.IdSeccion == idSeccionVariable)
                    .OrderByDescending(eetv => eetv.FechaMuestra)
                    .Take(1).FirstOrDefault().Valor; // Tomar solo la muestra más reciente
        }

        public bool ExisteValorEnEntidadEstructura(int idUsuario, int idSeccionVariable)
        {
            return context.EntidadEstructuraTablaValor.Any(eetv => eetv.IdTabla == idUsuario && eetv.IdSeccion == idSeccionVariable);
        }

        public IEnumerable<EntidadEstructuraTablaValor> ConsultarValoresPorCampos(int idExpediente, List<int> claveCampos, bool? fueraRango)
        {
            // Inicia la consulta
            var currentDay = DateTime.UtcNow.Date;
            var queryValoresCampos = context.EntidadEstructuraTablaValor
                .Include(ep => ep.IdEntidadEstructuraNavigation)
                .Where(ep => claveCampos.Contains( (int) ep.IdSeccion)
                    && ep.IdTabla == idExpediente && ep.FechaMuestra.Value.Date == currentDay);

            // Si fueraRango tiene un valor, añade la condición a la consulta
            if (fueraRango.HasValue)
            {
                queryValoresCampos = queryValoresCampos.Where(ep => ep.FueraDeRango == fueraRango);
            }

            // Retorna el resultado de la consulta
            return queryValoresCampos;
        }

        public IEnumerable<ValoresHistogramaDTO> ConsultarValoresPorClaveCampo(int idSeccionVariable, int idUsuario, DateTime fechaFiltro)
        {
            return context.EntidadEstructuraTablaValor
                .Include(etv => etv.IdEntidadEstructuraNavigation)
                .Where(etv => etv.IdSeccion == idSeccionVariable
                    && etv.IdTabla == idUsuario
                    && etv.FechaMuestra >= fechaFiltro)
                .Select(etv => new ValoresHistogramaDTO
                {
                    FechaMuestra = etv.FechaMuestra ?? new DateTime(),
                    Valor = int.Parse(etv.Valor)
                });
        }

        public IEnumerable<ExpedienteMuestrasGridDTO> ConsultarGridMuestras(int idUsuario)
        {
            return context.EntidadEstructuraTablaValor
                        .Where(eetv => eetv.IdTabla == idUsuario)
                        .GroupBy(eetv => eetv.FechaMuestra)
                        .Select(grupo => new ExpedienteMuestrasGridDTO
                        {
                            IdEntidadEstructuraTablaValor = grupo.FirstOrDefault().IdEntidadEstructuraTablaValor,
                            IdEntidadEstructura = grupo.FirstOrDefault().IdEntidadEstructura,
                            FechaMuestra = grupo.Key,
                            FueraDeRango = grupo.Any(registro => registro.FueraDeRango == true),
                            Registro = grupo.Select(registro => new ExpedienteMuestrasRegistroDTO
                            {
                                IdEntidadEstructuraTablaValor = registro.IdEntidadEstructuraTablaValor,
                                Numero = registro.Numero,
                                IdEntidadEstructura = registro.IdEntidadEstructura,
                                IdSeccionVariable = (int) registro.IdSeccion,
                                Valor = registro.Valor,
                                IdTabla = registro.IdTabla,
                                FueraDeRango = registro.FueraDeRango,
                                FechaMuestra = grupo.Key
                            }).ToList()
                        });
        }

    }
}
