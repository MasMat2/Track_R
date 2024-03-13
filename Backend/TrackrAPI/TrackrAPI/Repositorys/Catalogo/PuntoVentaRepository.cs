using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Inventario;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class PuntoVentaRepository: Repository<PuntoVenta>, IPuntoVentaRepository
    {
        public PuntoVentaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public PuntoVenta Consultar(int idPuntoVenta)
        {
            return context.PuntoVenta.Include(p => p.IdAlmacenNavigation).Where(e => e.IdPuntoVenta == idPuntoVenta).FirstOrDefault();
        }

        public PuntoVentaDto ConsultarDto(int idPuntoVenta)
        {
            return context.PuntoVenta
                      .Where(e => e.IdPuntoVenta == idPuntoVenta)
                      .Select(e => new PuntoVentaDto
                      {
                          IdPuntoVenta = e.IdPuntoVenta,
                          Descripcion = e.Descripcion,
                          Nombre = e.Nombre,
                          Clave = e.Clave,
                          IdAlmacen = e.IdAlmacen,
                          IdUbicacionVenta = e.IdUbicacionVenta,
                          IdTipoPuntoVenta = e.IdTipoPuntoVenta,
                          ClaveTipoPuntoVenta = e.IdTipoPuntoVentaNavigation.Clave,
                          IdConcepto = e.IdConcepto,
                      })
                      .FirstOrDefault();
        }

        public IEnumerable<PuntoVentaGridDto> ConsultarTodosParaGrid(int idCompania)
        {
            return context.PuntoVenta
                      .Include(e => e.IdConceptoNavigation)
                      .Where(e => e.IdAlmacenNavigation.IdCompania == idCompania)
                      .OrderBy(e => e.Clave)
                      .Select(e => new PuntoVentaGridDto
                      {
                          IdPuntoVenta = e.IdPuntoVenta,
                          Descripcion = e.Descripcion,
                          Nombre = e.Nombre,
                          Clave = e.Clave,
                          IdAlmacen = e.IdAlmacen,
                          IdUbicacionVenta = e.IdUbicacionVenta,
                          NombreUbicacionVenta = e.IdUbicacionVentaNavigation.Nombre,
                          NombreConcepto = e.IdConcepto != null ? e.IdConceptoNavigation.Nombre : "Sin especificar"
                      })
                      .ToList();
        }

        public IEnumerable<PuntoVentaDto> ConsultarTodosParaSelector(int idCompania)
        {
            return context.PuntoVenta
                      .Where(e => e.IdAlmacenNavigation.IdCompania == idCompania)
                      .OrderBy(e => e.Nombre)
                      .Select(e => new PuntoVentaDto
                      {
                          IdPuntoVenta = e.IdPuntoVenta,
                          Nombre = e.Nombre
                      })
                      .ToList();
        }

        public PuntoVenta ConsultarPorNombre(string nombre, int idCompania)
        {
            return context.PuntoVenta
                      .Where(e => e.Nombre.ToLower() == nombre.ToLower() && e.IdAlmacenNavigation.IdCompania == idCompania)
                      .FirstOrDefault();
        }
        public PuntoVenta ConsultarPorClave(string clave, int idCompania)
        {
            return context.PuntoVenta
                      .Where(e => e.Clave.ToLower() == clave.ToLower() && e.IdAlmacenNavigation.IdCompania == idCompania)
                      .FirstOrDefault();
        }

        public PuntoVenta ConsultarDependencias(int idPuntoVenta)
        {
            return context.PuntoVenta
                      .Include(e => e.NotaVenta)
                      .Include(e => e.UbicacionVenta)
                      .Include(e => e.Usuario)
                      .Where(e => e.IdPuntoVenta == idPuntoVenta)
                      .FirstOrDefault();
        }

        public IEnumerable<UsuarioDto> ConsultarUsuariosAsignados(int idPuntoVenta)
        {
            return context.Usuario
                      .Where(u => u.IdPuntoVenta == idPuntoVenta)
                      .OrderBy(e => e.Nombre)
                      .Select(u => new UsuarioDto
                      {
                          IdUsuario = u.IdUsuario,
                          NombreCompleto = u.Nombre + " " + u.ApellidoPaterno + " " + (u.ApellidoMaterno != null ? u.ApellidoMaterno : " ")
                      })
                      .ToList();
        }
    }
}
