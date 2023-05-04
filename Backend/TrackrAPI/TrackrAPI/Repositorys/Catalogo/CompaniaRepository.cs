using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class CompaniaRepository : Repository<Compania>, ICompaniaRepository
    {
        public CompaniaRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Compania Consultar(int idCompania)
        {
            return context.Compania
                .Include(c => c.IdTipoCompaniaNavigation)
                .Include(c => c.MercadoCompania)
                .Where(c => c.IdCompania == idCompania)
                .FirstOrDefault();
        }

        public Compania Consultar(string rfc)
        {
            return context.Compania
                .Where(c => c.Rfc == rfc)
                .FirstOrDefault();
        }

        public CompaniaDto ConsultarPorIdentificadorUrl(string identificadorUrl)
        {
            return context.Compania
                .Include(c => c.CompaniaLogotipo)
                .Where(c => c.Clave == identificadorUrl)
                .Select(c => new CompaniaDto
                {
                    IdCompania = c.IdCompania,
                    Clave = c.Clave,
                    Nombre = c.Nombre,
                    Correo = c.Correo,
                    PortalWeb = c.PortalWeb,
                    Calle = c.Calle,
                    NumeroExterior = c.NumeroExterior,
                    NumeroInterior = c.NumeroInterior,
                    Colonia = c.Colonia,
                    CodigoPostal = c.CodigoPostal,
                    Telefono = c.Telefono,
                    Ciudad = c.Ciudad,
                    IdEstado = c.IdEstado,
                    IdPais = c.IdEstadoNavigation.IdPais,
                    Rfc = c.Rfc,
                    IdLada = c.IdLada,
                    IdRegimenFiscal = c.IdRegimenFiscal,
                    Existe = true,
                    Timbrado = c.Timbrado,
                    IdGiroComercial = c.IdGiroComercial,
                    IdLocacionDefault = c.Hospital.First().IdHospital,
                    Logotipo = c.CompaniaLogotipo.FirstOrDefault() != null ?
                               GeneralConstant.RutaArchivoCompaniaLogotipo + c.CompaniaLogotipo.First().IdCompaniaLogotipo : null
                })
                .FirstOrDefault();
        }

        public IEnumerable<CompaniaDto> ConsultarGeneral()
        {
            return context.Compania
                .OrderBy(c => c.Clave)
                .Select(c => new CompaniaDto
                {
                    IdCompania = c.IdCompania,
                    Clave = c.Clave,
                    Nombre = c.Nombre,
                    Correo = c.Correo,
                    PortalWeb = c.PortalWeb,
                    Calle = c.Calle,
                    NumeroExterior = c.NumeroExterior,
                    NumeroInterior = c.NumeroInterior,
                    Colonia = c.Colonia,
                    CodigoPostal = c.CodigoPostal,
                    Telefono = c.Telefono,
                    Ciudad = c.Ciudad,
                    IdEstado = c.IdEstado,
                    IdPais = c.IdEstadoNavigation.IdPais,
                    Rfc = c.Rfc,
                    IdLada = c.IdLada,
                    IdRegimenFiscal = c.IdRegimenFiscal,
                    Timbrado = c.Timbrado,
                    IdGiroComercial = c.IdGiroComercial
                })
                .ToList();
        }
        public IEnumerable<CompaniaSelectorDto> ConsultarTodosParaSelector()
        {
            return context.Compania
                .OrderBy(c => c.Clave)
                .Select(c => new CompaniaSelectorDto
                {
                    IdCompania = c.IdCompania,
                    Clave = c.Clave,
                    Nombre = c.Nombre,
                })
                .ToList();
        }
        public IEnumerable<CompaniaDto> ConsultarTodosParaGrid(CompaniaFiltroDto filtro, string claveCompania)
        {
            if(claveCompania == GeneralConstant.ClaveCompaniaBase)
            {
                return context.Compania
                .Where(c =>
                    (c.Rfc.Contains(filtro.Rfc) || string.IsNullOrWhiteSpace(filtro.Rfc)) &&
                    (c.Nombre.Contains(filtro.Nombre) || string.IsNullOrWhiteSpace(filtro.Nombre)) &&
                    (filtro.IdTipoCompania == 0 || c.IdTipoCompania == filtro.IdTipoCompania)
                )
                .OrderBy(c => c.Clave)
                .Select(c => new CompaniaDto
                {
                    IdCompania = c.IdCompania,
                    Clave = c.Clave,
                    Nombre = c.Nombre,
                    Correo = c.Correo,
                    PortalWeb = c.PortalWeb,
                    Calle = c.Calle,
                    NumeroExterior = c.NumeroExterior,
                    NumeroInterior = c.NumeroInterior,
                    Colonia = c.Colonia,
                    CodigoPostal = c.CodigoPostal,
                    Telefono = c.Telefono,
                    Ciudad = c.Ciudad,
                    IdEstado = c.IdEstado,
                    IdPais = c.IdEstadoNavigation.IdPais,
                    Rfc = c.Rfc,
                    IdLada = c.IdLada,
                    IdRegimenFiscal = c.IdRegimenFiscal,
                    Timbrado = c.Timbrado,
                    IdGiroComercial = c.IdGiroComercial
                })
                .ToList();
            }
            return context.Compania
                .Where(c => c.Clave == claveCompania)
                .Select(c => new CompaniaDto
                {
                    IdCompania = c.IdCompania,
                    Clave = c.Clave,
                    Nombre = c.Nombre,
                    Correo = c.Correo,
                    PortalWeb = c.PortalWeb,
                    Calle = c.Calle,
                    NumeroExterior = c.NumeroExterior,
                    NumeroInterior = c.NumeroInterior,
                    Colonia = c.Colonia,
                    CodigoPostal = c.CodigoPostal,
                    Telefono = c.Telefono,
                    Ciudad = c.Ciudad,
                    IdEstado = c.IdEstado,
                    IdPais = c.IdEstadoNavigation.IdPais,
                    Rfc = c.Rfc,
                    IdLada = c.IdLada,
                    IdRegimenFiscal = c.IdRegimenFiscal,
                    Timbrado = c.Timbrado,
                    IdGiroComercial = c.IdGiroComercial
                })
                .ToList();

        }

        public IEnumerable<CompaniaDto> ConsultarPorUsuarioPermiso(int idUsuario)
        {
            if (idUsuario == GeneralConstant.UsuarioMaestroAtisc)
            {
                return context.Compania
                    .Select(c => new CompaniaDto
                    {
                        IdCompania = c.IdCompania,
                        Clave = c.Clave,
                        Nombre = c.Nombre,
                        Correo = c.Correo,
                        PortalWeb = c.PortalWeb,
                        Calle = c.Calle,
                        NumeroExterior = c.NumeroExterior,
                        NumeroInterior = c.NumeroInterior,
                        Colonia = c.Colonia,
                        CodigoPostal = c.CodigoPostal,
                        Telefono = c.Telefono,
                        Ciudad = c.Ciudad,
                        IdEstado = c.IdEstado,
                        IdPais = c.IdEstadoNavigation.IdPais,
                        Rfc = c.Rfc,
                        IdLada = c.IdLada,
                        IdRegimenFiscal = c.IdRegimenFiscal
                    })
                    .ToList();
            }

            return context.Compania
                .Where(c => c.Hospital.Any(h => h.UsuarioLocacion.Any(ul => ul.IdUsuario == idUsuario)))
                .Select(c => new CompaniaDto
                {
                    IdCompania = c.IdCompania,
                    Clave = c.Clave,
                    Nombre = c.Nombre,
                    Correo = c.Correo,
                    PortalWeb = c.PortalWeb,
                    Calle = c.Calle,
                    NumeroExterior = c.NumeroExterior,
                    NumeroInterior = c.NumeroInterior,
                    Colonia = c.Colonia,
                    CodigoPostal = c.CodigoPostal,
                    Telefono = c.Telefono,
                    Ciudad = c.Ciudad,
                    IdEstado = c.IdEstado,
                    IdPais = c.IdEstadoNavigation.IdPais,
                    Rfc = c.Rfc,
                    IdLada = c.IdLada,
                    IdRegimenFiscal = c.IdRegimenFiscal
                })
                .ToList();
        }

        public Compania ConsultarUltimaAgregada()
        {
            return context.Compania
                .Where(c => c.Clave != GeneralConstant.ClaveCompaniaBase)
                .OrderByDescending(c => c.IdCompania)
                .FirstOrDefault();
        }

        public CompaniaDto ConsultarDto(int idCompania)
        {
            return context.Compania
                .Include(c => c.IdTipoCompaniaNavigation)
                .Where(c => c.IdCompania == idCompania)
                .Select(c => new CompaniaDto
                {
                    IdCompania = c.IdCompania,
                    Clave = c.Clave,
                    Nombre = c.Nombre,
                    Correo = c.Correo,
                    PortalWeb = c.PortalWeb,
                    Calle = c.Calle,
                    NumeroExterior = c.NumeroExterior,
                    NumeroInterior = c.NumeroInterior,
                    Colonia = c.Colonia,
                    CodigoPostal = c.CodigoPostal,
                    Telefono = c.Telefono,
                    Ciudad = c.Ciudad,
                    IdEstado = c.IdEstado,
                    IdPais = c.IdEstadoNavigation.IdPais,
                    Rfc = c.Rfc,
                    IdLada = c.IdLada,
                    IdRegimenFiscal = c.IdRegimenFiscal,
                    IdAgrupadorCuentaContable = c.IdAgrupadorCuentaContable,
                    IdTipoCompania = c.IdTipoCompania,
                    IdMoneda = c.IdMoneda,
                    AfectacionContable = c.AfectacionContable,
                    ClaveTipoCompania = c.IdTipoCompaniaNavigation.Clave,
                    Timbrado = c.Timbrado,
                    IdGiroComercial = c.IdGiroComercial,
                    IdMunicipio = c.IdMunicipio,
                    UsoAlmacen = c.UsoAlmacen == null ? false : (bool)c.UsoAlmacen
                })
                .FirstOrDefault();
        }

        public Compania ConsultarPorClave(string clave)
        {
            return context.Compania
            .Where(c => c.Clave == clave)
            .FirstOrDefault();
        }
    }
}
