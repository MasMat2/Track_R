using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Inventario;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Inventario
{
    public class AlmacenRepository : Repository<Almacen>, IAlmacenRepository
    {
        public AlmacenRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public AlmacenDto ConsultarDto(int idAlmacen)
        {
            return context.Almacen
                .Where(a => a.IdAlmacen == idAlmacen)
                .Select(a => new AlmacenDto
                {
                    IdAlmacen = a.IdAlmacen,
                    Numero = a.Numero,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion,
                    FechaAlta = a.FechaAlta,
                    Calle = a.Calle,
                    NumeroExterior = a.NumeroExterior,
                    NumeroInterior = a.NumeroInterior,
                    Colonia = a.Colonia,
                    Localidad = a.Localidad,
                    CodigoPostal = a.CodigoPostal,
                    TelefonoUno = a.TelefonoUno,
                    TelefonoDos = a.TelefonoDos,
                    IdEstatusAlmacen = a.IdEstatusAlmacen,
                    IdUsuarioResponsable = a.IdUsuarioResponsable,
                    IdEstado = a.IdEstado,
                    IdCompania = a.IdCompania,
                    IdCuentaContable = a.IdCuentaContable
                })
                .FirstOrDefault();
        }

        public Almacen Consultar(int idAlmacen)
        {
            return context.Almacen
                .Where(a => a.IdAlmacen == idAlmacen)
                .FirstOrDefault();
        }

        public IEnumerable<AlmacenGridDto> ConsultarGeneral(int idUsuario)
        {
            return context.Almacen
                .Where(a => a.UsuarioAlmacen.Any(ua => ua.IdUsuario == idUsuario))
                .Include(a => a.IdUsuarioResponsableNavigation)
                .Include(a => a.IdEstadoNavigation)
                .Include(a => a.IdCuentaContableNavigation)
                .OrderBy(a => a.Numero)
                .Select(a => new AlmacenGridDto
                {
                    IdAlmacen = a.IdAlmacen,
                    Numero = a.Numero,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion,
                    FechaAlta = a.FechaAlta,
                    Calle = a.Calle,
                    NumeroExterior = a.NumeroExterior,
                    NumeroInterior = a.NumeroInterior,
                    Colonia = a.Colonia,
                    Localidad = a.Localidad,
                    CodigoPostal = a.CodigoPostal,
                    TelefonoUno = a.TelefonoUno,
                    TelefonoDos = a.TelefonoDos,
                    IdEstatusAlmacen = a.IdEstatusAlmacen,
                    IdUsuarioResponsable = a.IdUsuarioResponsable,
                    IdEstado = a.IdEstado,
                    IdCompania = a.IdCompania,
                    Direccion = a.ObtenerDireccionAlmacen(),
                    ResponsableNombre = a.IdUsuarioResponsableNavigation.Nombre
                    + " " + a.IdUsuarioResponsableNavigation.ApellidoPaterno
                    + " " + a.IdUsuarioResponsableNavigation.ApellidoMaterno,
                    IdCuentaContable = a.IdCuentaContable,
                    NombreCuentaContable = a.IdCuentaContable != null ? a.IdCuentaContableNavigation.Nombre : "Sin especificar"
                }).ToList();
        }

        public IEnumerable<AlmacenGridDto> ConsultarPorEstado(int idEstado)
        {
            return context.Almacen
                .Where(a => a.IdEstado == idEstado)
                .OrderBy(a => a.Nombre)
                .Select(a => new AlmacenGridDto
                {
                    IdAlmacen = a.IdAlmacen,
                    Numero = a.Numero,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion,
                    FechaAlta = a.FechaAlta,
                    Calle = a.Calle,
                    NumeroExterior = a.NumeroExterior,
                    NumeroInterior = a.NumeroInterior,
                    Colonia = a.Colonia,
                    Localidad = a.Localidad,
                    CodigoPostal = a.CodigoPostal,
                    TelefonoUno = a.TelefonoUno,
                    TelefonoDos = a.TelefonoDos,
                    IdEstatusAlmacen = a.IdEstatusAlmacen,
                    IdUsuarioResponsable = a.IdUsuarioResponsable,
                    IdEstado = a.IdEstado,
                    IdCompania = a.IdCompania,
                    Direccion = a.ObtenerDireccionAlmacen(),
                    ResponsableNombre = a.IdUsuarioResponsableNavigation.Nombre
                    + " " + a.IdUsuarioResponsableNavigation.ApellidoPaterno
                    + " " + a.IdUsuarioResponsableNavigation.ApellidoMaterno
                })
                .ToList();
        }

        public IEnumerable<AlmacenGridDto> ConsultarPorEstatus(int idEstatusAlmacen)
        {
            return context.Almacen
                .Where(a => a.IdEstatusAlmacen == idEstatusAlmacen)
                .Select(a => new AlmacenGridDto
                {
                    IdAlmacen = a.IdAlmacen,
                    Numero = a.Numero,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion,
                    FechaAlta = a.FechaAlta,
                    Calle = a.Calle,
                    NumeroExterior = a.NumeroExterior,
                    NumeroInterior = a.NumeroInterior,
                    Colonia = a.Colonia,
                    Localidad = a.Localidad,
                    CodigoPostal = a.CodigoPostal,
                    TelefonoUno = a.TelefonoUno,
                    TelefonoDos = a.TelefonoDos,
                    IdEstatusAlmacen = a.IdEstatusAlmacen,
                    IdUsuarioResponsable = a.IdUsuarioResponsable,
                    IdEstado = a.IdEstado,
                    IdCompania = a.IdCompania,
                    Direccion = a.ObtenerDireccionAlmacen(),
                    ResponsableNombre = a.IdUsuarioResponsableNavigation.Nombre
                    + " " + a.IdUsuarioResponsableNavigation.ApellidoPaterno
                    + " " + a.IdUsuarioResponsableNavigation.ApellidoMaterno
                })
                .ToList();
        }

        public IEnumerable<AlmacenGridDto> ConsultarPorUsuario(int idUsuarioResponsable)
        {
            return context.Almacen
                .Where(a => a.IdUsuarioResponsable == idUsuarioResponsable)
                .Select(a => new AlmacenGridDto
                {
                    IdAlmacen = a.IdAlmacen,
                    Numero = a.Numero,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion,
                    FechaAlta = a.FechaAlta,
                    Calle = a.Calle,
                    NumeroExterior = a.NumeroExterior,
                    NumeroInterior = a.NumeroInterior,
                    Colonia = a.Colonia,
                    Localidad = a.Localidad,
                    CodigoPostal = a.CodigoPostal,
                    TelefonoUno = a.TelefonoUno,
                    TelefonoDos = a.TelefonoDos,
                    IdEstatusAlmacen = a.IdEstatusAlmacen,
                    IdUsuarioResponsable = a.IdUsuarioResponsable,
                    IdEstado = a.IdEstado,
                    IdCompania = a.IdCompania,
                    Direccion = a.ObtenerDireccionAlmacen(),
                    ResponsableNombre = a.IdUsuarioResponsableNavigation.Nombre
                    + " " + a.IdUsuarioResponsableNavigation.ApellidoPaterno
                    + " " + a.IdUsuarioResponsableNavigation.ApellidoMaterno
                })
                .ToList();
        }

        public IEnumerable<AlmacenDto> ConsultarPorCompania(int idCompania, int idUsuario)
        {
            return context.Almacen
                .Where(a => a.IdCompania == idCompania
                && a.UsuarioAlmacen.Any(ua => ua.IdUsuario == idUsuario))
                .Select(a => new AlmacenDto
                {
                    IdAlmacen = a.IdAlmacen,
                    Numero = a.Numero,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion,
                    FechaAlta = a.FechaAlta,
                    Calle = a.Calle,
                    NumeroExterior = a.NumeroExterior,
                    NumeroInterior = a.NumeroInterior,
                    Colonia = a.Colonia,
                    Localidad = a.Localidad,
                    CodigoPostal = a.CodigoPostal,
                    TelefonoUno = a.TelefonoUno,
                    TelefonoDos = a.TelefonoDos,
                    IdEstatusAlmacen = a.IdEstatusAlmacen,
                    IdUsuarioResponsable = a.IdUsuarioResponsable,
                    IdEstado = a.IdEstado,
                    IdCompania = a.IdCompania
                })
                .ToList();
        }

        public IEnumerable<AlmacenDto> ConsultarPorCompania(int idCompania)
        {
            return context.Almacen
                .Where(a => a.IdCompania == idCompania)
                .Select(a => new AlmacenDto
                {
                    IdAlmacen = a.IdAlmacen,
                    Numero = a.Numero,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion,
                    FechaAlta = a.FechaAlta,
                    Calle = a.Calle,
                    NumeroExterior = a.NumeroExterior,
                    NumeroInterior = a.NumeroInterior,
                    Colonia = a.Colonia,
                    Localidad = a.Localidad,
                    CodigoPostal = a.CodigoPostal,
                    TelefonoUno = a.TelefonoUno,
                    TelefonoDos = a.TelefonoDos,
                    IdEstatusAlmacen = a.IdEstatusAlmacen,
                    IdUsuarioResponsable = a.IdUsuarioResponsable,
                    IdEstado = a.IdEstado,
                    IdCompania = a.IdCompania
                })
                .ToList();
        }

        public IEnumerable<AlmacenDto> ConsultarTodosParaSelector(int idCompania)
        {
            return context.Almacen
                      .Where(e => e.IdCompania == idCompania)
                      .OrderBy(e => e.Nombre)
                      .Select(e => new AlmacenDto
                      {
                          IdAlmacen = e.IdAlmacen,
                          Nombre = e.Nombre
                      })
                      .ToList();
        }
        public Almacen ConsultarConDependencias(int idAlmacen)
        {
            return context.Almacen
                   .Where(a => a.IdAlmacen == idAlmacen)
                   .Include(a => a.InventarioFisico)
                   .Include(a => a.Kardex)
                   .Include(a => a.MovimientoMaterial)
                   .Include(a => a.OrdenCompra)
                   .Include(a => a.PuntoVenta)
                   .Include(a => a.TraspasoMovimientoMaterialIdAlmacenDestinoNavigation)
                   .Include(a => a.TraspasoMovimientoMaterialIdAlmacenOrigenNavigation)
                   .Include(a => a.Ubicacion)
                   .FirstOrDefault();
        }

        public Almacen ConsultarPorNumero(string numero, int idCompania)
        {
            return context.Almacen
                    .Where(e => e.Numero.ToLower().Equals(numero.ToLower()) && e.IdCompania == idCompania)
                    .FirstOrDefault();
        }

        public Almacen ConsultarPorNombre(string nombre, int idCompania)
        {
            return context.Almacen
                    .Where(e => e.Nombre.ToLower().Equals(nombre.ToLower()) && e.IdCompania == idCompania)
                    .FirstOrDefault();
        }
    }
}
