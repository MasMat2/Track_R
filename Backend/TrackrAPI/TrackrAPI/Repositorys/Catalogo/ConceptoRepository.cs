using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class ConceptoRepository : Repository<Concepto>, IConceptoRepository
    {
        public ConceptoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<ConceptoGridDto> ConsultarTodosParaGrid(int idCompania)
        {
            return context.Concepto
                .Where(c => c.IdCompania == idCompania)
                .OrderBy(c => c.Clave)
                .Select(c => new ConceptoGridDto
                {
                    IdConcepto = c.IdConcepto,
                    Nombre = c.Nombre,
                    Clave = c.Clave,
                    TipoMovimiento = c.TipoMovimiento,
                    IdCuentaContable = c.IdCuentaContable,
                    IdCompania = c.IdCompania,
                    NombreCuentaContable = c.IdCuentaContableNavigation.Numero + GeneralConstant.DescriptionSeparator + c.IdCuentaContableNavigation.Nombre,
                    NombreProductoServicioSat = c.IdSatProductoServicioNavigation.Descripcion,
                    NombreUnidadSat = c.IdSatUnidadNavigation.Descripcion,
                    NombresTipoConcepto = string.Join(", ", c.ConfiguracionConcepto.Select(cc => cc.IdTipoConceptoNavigation.Nombre))
                })
                .ToList();
        }

        public IEnumerable<ConceptoGridDto> ConsultarTodosParaSelector(int idCompania)
        {
            return context.Concepto
                .Where(c => c.IdCompania == idCompania)
                .OrderBy(c => c.Nombre)
                .Select(c => new ConceptoGridDto
                {
                    IdConcepto = c.IdConcepto,
                    Nombre = c.Nombre,
                    Clave = c.Clave,
                    TipoMovimiento = c.TipoMovimiento,
                    IdCuentaContable = c.IdCuentaContable,
                    IdCompania = c.IdCompania
                })
                .ToList();
        }

        public IEnumerable<ConceptoSelectorDto> ConsultarSelectorParaPresentacion(int idCompania)
        {
            return context.Concepto
                .Include(c => c.IdCuentaContableNavigation)
                .Where(c => c.IdCompania == idCompania)
                .OrderBy(c => c.Nombre)
                .Select(c => new ConceptoSelectorDto
                {
                    IdConcepto = c.IdConcepto,
                    Nombre = c.Nombre,
                    IdCuentaContable = c.IdCuentaContable,
                    NumeroCuentaContable = c.IdCuentaContableNavigation.Numero,
                    SelectorLabel = c.Nombre + " - " + c.IdCuentaContableNavigation.Numero
                })
                .ToList();
        }

        public ConceptoDto ConsultarDto(int idConcepto)
        {
            return context.Concepto
                .Where(c => c.IdConcepto == idConcepto)
                .Select(c => new ConceptoDto
                {
                    IdConcepto = c.IdConcepto,
                    Clave = c.Clave,
                    Nombre = c.Nombre,
                    TipoMovimiento = c.TipoMovimiento,
                    IdCuentaContable = c.IdCuentaContable,
                    Operativo = c.Operativo,
                    NombreUnidadSat = c.IdSatUnidadNavigation.Descripcion,
                    NombreProductoServicioSat = c.IdSatProductoServicioNavigation.Descripcion,
                    IdSatProductoServicio = c.IdSatProductoServicio,
                    IdSatUnidad = c.IdSatUnidad,
                    IdTipoAuxiliar = c.IdTipoAuxiliar,
                    IdsTipoConcepto = c.ConfiguracionConcepto.Select(cc => cc.IdTipoConcepto).ToList(),
                    ClaveTipoCuentaContable = c.IdCuentaContable > 0
                        ? c.IdCuentaContableNavigation.IdTipoCuentaContableNavigation.Clave
                        : null,
                    RequiereAuxiliar = c.IdCuentaContable > 0
                        ? c.IdCuentaContableNavigation.Auxiliar
                        : false
                })
                .FirstOrDefault();
        }

        public Concepto Consultar(int idConcepto)
        {
            return context.Concepto.Where(c => c.IdConcepto == idConcepto).FirstOrDefault();
        }

        public Concepto Consultar(string nombre, int idCompania)
        {
            return context.Concepto
                      .Where(c => c.Nombre.ToLower() == nombre.ToLower() && c.IdCompania == idCompania)
                      .FirstOrDefault();
        }

        public Concepto ConsultarPorClave(string clave, int idCompania)
        {
            return context.Concepto
                      .Where(c => c.Clave == clave && (c.IdCompania == idCompania))
                      .FirstOrDefault();
        }

        public Concepto ConsultarPorUsuarioRol(int idUsuario, string claveRol)
        {
            var usuarioRol = context.UsuarioRol
                      .Include(ur => ur.IdConceptoNavigation)
                      .Include(ur => ur.IdRolNavigation)
                      .Where(ur => ur.IdUsuario == idUsuario && ur.IdRolNavigation.Clave == claveRol)
                      .FirstOrDefault();

            if (usuarioRol != null)
            {
                return usuarioRol.IdConceptoNavigation;
            }
            else
            {
                return null;
            }
        }

        //Conceptos que no sean operativos
        public IEnumerable<ConceptoGridDto> ConsultarParaDesgloseSelector(int idCompania)
        {
            return context.Concepto
                      .Where(c => (c.IdCompania == idCompania)
                         && c.Operativo == false)
                      .OrderBy(c => c.Nombre)
                      .Select(c => new ConceptoGridDto
                      {
                          IdConcepto = c.IdConcepto,
                          Nombre = c.Nombre,
                          Clave = c.Clave,
                          TipoMovimiento = c.TipoMovimiento,
                          IdCuentaContable = c.IdCuentaContable,
                          IdCompania = c.IdCompania,
                          IdTipoAuxiliar = c.IdTipoAuxiliar
                      })
            .ToList();
        }

        public IEnumerable<ConceptoGridDto> ConsultarOperativosParaSelector(int idCompania)
        {
            return context.Concepto
                    .Where(c => (c.IdCompania == idCompania)
                       && c.Operativo == true)
                    .OrderBy(c => c.Nombre)
                    .Select(c => new ConceptoGridDto
                    {
                        IdConcepto = c.IdConcepto,
                        Nombre = c.Nombre,
                        Clave = c.Clave,
                        TipoMovimiento = c.TipoMovimiento,
                        IdCuentaContable = c.IdCuentaContable,
                        IdCompania = c.IdCompania,
                        IdTipoAuxiliar = c.IdTipoAuxiliar
                    })
                    .ToList();
        }

        public IEnumerable<Concepto> ConsultarPorCompaniaBase()
        {
            int? idCompaniaBase = context.Compania.FirstOrDefault(cc => cc.Clave == GeneralConstant.ClaveCompaniaBase)?.IdCompania;

            return context.Concepto
                    .Where(c => c.IdCompania == idCompaniaBase)
                    .Include(c => c.IdCuentaContableNavigation)
                    .OrderBy(c => c.Nombre)
                    .ToList();
        }

        public Concepto ConsultarDependencias(int idConcepto)
        {
            return context.Concepto
                .Where(c => c.IdConcepto == idConcepto)
                .Include(c => c.FacturaConcepto)
                .Include(c => c.GastoConcepto)
                .Include(c => c.NotaGasto)
                .Include(c => c.NotaGastoDetalle)
                .Include(c => c.NotaVenta)
                .Include(c => c.NotaVentaDetalle)
                .Include(c => c.Presentacion)
                .Include(c => c.PuntoVenta)
                .Include(c => c.UsuarioRol)
                .FirstOrDefault();
        }

        public IEnumerable<Concepto> ConsultarPorTipo(string claveTipoConcepto, int idCompania)
        {
            return context.Concepto
              .Where(c =>
               c.IdCompania == idCompania &&
               c.ConfiguracionConcepto.Any(c => c.IdTipoConceptoNavigation.Clave == claveTipoConcepto))
              .OrderBy(c => c.Nombre)
              .ToList();
        }
    }
}
