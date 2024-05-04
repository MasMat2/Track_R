using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Inventario;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Inventario
{
    public class DomicilioRepository : Repository<Domicilio>, IDomicilioRepository
    {
        public DomicilioRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<Domicilio> ConsultarTodos(int idCompania)
        {
            return context.Domicilio
                .Where(d => d.IdCompania == idCompania);
        }

        public IEnumerable<DomicilioGridDto> ConsultarGeneral(int idCompania)
        {
            return context.Domicilio
                .Include(d => d.IdPaisNavigation)
                .Include(d => d.IdEstadoNavigation)
                .Include(d => d.IdLocalidadNavigation)
                .Include(d => d.IdColoniaNavigation)
                .Where(d => d.IdCompania == idCompania)
                .Select(d => new DomicilioGridDto
                {
                    IdDomicilio = d.IdDomicilio,
                    Calle = d.Calle,
                    NumeroExterior = d.NumeroExterior,
                    NumeroInterior = d.NumeroInterior,
                    Colonia = d.Colonia,
                    Localidad = d.Localidad,
                    CodigoPostal = d.CodigoPostal,
                    IdEstado = d.IdEstado,
                    Direccion = d.ObtenerDireccion(),
                    IdUsuario = d.IdUsuario,
                    NombreUsuario = d.IdUsuarioNavigation.ObtenerNombreCompleto()
                })
                .ToList();
        }
        public Domicilio Consultar(int idDomicilio)
        {
            return context.Domicilio
                .Where(d => d.IdDomicilio == idDomicilio)
                .FirstOrDefault();
        }

        public DomicilioDto ConsultarDto(int idDomicilio)
        {
            return context.Domicilio
                .Include(d => d.IdPaisNavigation)
                .Include(d => d.IdEstadoNavigation)
                .Include(d => d.IdLocalidadNavigation)
                .Include(d => d.IdColoniaNavigation)
                .Where(d => d.IdDomicilio == idDomicilio)
                .Select(d => new DomicilioDto
                {
                    IdDomicilio = d.IdDomicilio,
                    Calle = d.Calle,
                    NumeroExterior = d.NumeroExterior,
                    NumeroInterior = d.NumeroInterior,
                    CodigoPostal = d.CodigoPostal,
                    IdPais = d.IdPais != null ? (int)d.IdPais : 0,
                    IdEstado = d.IdEstado,
                    IdMunicipio = d.IdMunicipio != null ? (int)d.IdMunicipio : 0,
                    IdLocalidad = d.IdLocalidad != null ? (int)d.IdLocalidad : 0,
                    IdColonia = d.IdColonia != null ? (int)d.IdColonia : 0,
                    Direccion = d.ObtenerDireccion(),
                    IdUsuario = d.IdUsuario,
                    NombreUsuario = d.IdUsuarioNavigation.ObtenerNombreCompleto(),
                    NombreSucursal = d.NombreSucursal,
                    IdMetodoPago = d.IdMetodoPago,
                    IdUsuarioRepartidor = d.IdUsuarioRepartidor,
                    OtraReferencia = d.OtraReferencia,
                    EntreCalles = d.EntreCalles
                })
                .FirstOrDefault();
        }

        public IEnumerable<DomicilioGridDto> ConsultarPorEstado(int idEstado)
        {
            return context.Domicilio
                .Include(d => d.IdEstadoNavigation)
                .Where(d => d.IdEstado == idEstado)
                .Select(d => new DomicilioGridDto
                {
                    IdDomicilio = d.IdDomicilio,
                    Calle = d.Calle,
                    NumeroExterior = d.NumeroExterior,
                    NumeroInterior = d.NumeroInterior,
                    Colonia = d.Colonia,
                    Localidad = d.Localidad,
                    CodigoPostal = d.CodigoPostal,
                    IdEstado = d.IdEstado,
                    Direccion = d.ObtenerDireccion()
                })
                .ToList();
        }

        public Domicilio ConsultarPorDomicilio(Domicilio domicilio)
        {
            return context.Domicilio
                .Where(d => d.IdCompania == domicilio.IdCompania
                    && d.IdPais == domicilio.IdPais
                    && d.IdEstado == domicilio.IdEstado
                    && d.IdMunicipio == domicilio.IdMunicipio
                    && d.IdLocalidad == domicilio.IdLocalidad
                    && d.CodigoPostal == domicilio.CodigoPostal
                    && d.IdColonia == domicilio.IdColonia
                    && d.Calle.ToLower() == domicilio.Calle.ToLower()
                    && d.NumeroExterior.ToLower() == domicilio.NumeroExterior.ToLower()
                    && (string.IsNullOrEmpty(d.NumeroInterior) && string.IsNullOrEmpty(domicilio.NumeroInterior))               // Ambos son null
                        || ((!string.IsNullOrEmpty(d.NumeroInterior) && !string.IsNullOrEmpty(domicilio.NumeroInterior))        // Ambos son no null
                        && (d.NumeroInterior.ToLower() == domicilio.NumeroInterior.ToLower()))                                  // y la cadena es igual
                )
                .FirstOrDefault();
        }

        public IEnumerable<Domicilio> ConsultarPorUsuario(int idUsuario)
        {
            return context.Domicilio
                .Include(d => d.IdPaisNavigation)
                .Include(d => d.IdEstadoNavigation)
                .Include(d => d.IdColoniaNavigation)
                .Include(d => d.IdLocalidadNavigation)
                .Where(d => d.IdUsuario == idUsuario);
        }

        //public Domicilio ConsultarDependencias(int idDomicilio)
        //{
        //    return context.Domicilio
        //        .Include(d => d.OrdenCompra)
        //        .Include(d => d.Pedido)
        //        .Include(d => d.ExpedienteAdministrativoMercancia)
        //        .Include(d => d.ExpedienteAdministrativoViaje)
        //        .Where(d => d.IdDomicilio == idDomicilio)
        //        .FirstOrDefault();
        //}

        public IEnumerable<DomicilioSelectorDto> ConsultarParaSelector(int idCompania)
        {
            return context.Domicilio
                .Where(d => d.IdCompania == idCompania)
                .Include(d => d.IdPaisNavigation)
                .Include(d => d.IdEstadoNavigation)
                .Include(d => d.IdLocalidadNavigation)
                .Include(d => d.IdColoniaNavigation)
                .Select(d => new DomicilioSelectorDto
                {
                    IdDomicilio = d.IdDomicilio,
                    Direccion = d.ObtenerDireccion()
                })
                .ToList();
        }
    }
}
