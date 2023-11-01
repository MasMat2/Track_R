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

        public IEnumerable<EntidadEstructuraTablaValor> ConsultarValoresPorCampos(int idExpediente, IEnumerable<string> claveCampos, bool? fueraRango)
        {
            // Inicia la consulta
            var queryValoresCampos = context.EntidadEstructuraTablaValor
                .Include(ep => ep.IdEntidadEstructuraNavigation)
                .Where(ep => claveCampos.Contains(ep.ClaveCampo)
                    && ep.IdTabla == idExpediente);

            // Si fueraRango tiene un valor, añade la condición a la consulta
            if (fueraRango.HasValue)
            {
                queryValoresCampos = queryValoresCampos.Where(ep => ep.FueraDeRango == fueraRango);
            }

            // Retorna el resultado de la consulta
            return queryValoresCampos;
        }

        public IEnumerable<ValoresHistogramaDTO> ConsultarValoresPorClaveCampo(string claveCampo, int idUsuario, DateTime fechaFiltro)
        {
            return context.EntidadEstructuraTablaValor
                .Include(etv => etv.IdEntidadEstructuraNavigation)
                .Where(etv => etv.ClaveCampo == claveCampo
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
                                ClaveCampo = registro.ClaveCampo,
                                Valor = registro.Valor,
                                IdTabla = registro.IdTabla,
                                FueraDeRango = registro.FueraDeRango,
                                FechaMuestra = grupo.Key
                            }).ToList()
                        });
        }

    }
}
