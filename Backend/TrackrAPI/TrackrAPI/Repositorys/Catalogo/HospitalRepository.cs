using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class HospitalRepository : Repository<Hospital>, IHospitalRepository
    {
        public HospitalRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<HospitalGridDto> ConsultarPorCompaniaParaGrid(int idCompania)
        {
            return context.Hospital
                .OrderBy(h => h.IdHospital)
                .Where(h => h.IdCompania == idCompania)
                .Select(h => new HospitalGridDto
                {
                    IdHospital = h.IdHospital,
                    Nombre = h.Nombre,
                    Ciudad = h.IdMunicipioNavigation.Nombre,
                    Gerente = h.IdUsuarioGerenteNavigation.Nombre + " " +
                    h.IdUsuarioGerenteNavigation.ApellidoPaterno + " " +
                    h.IdUsuarioGerenteNavigation.ApellidoMaterno,
                    EsPredeterminada = h.Predeterminada ? "Sí" : "No"
                })
                .ToList();
        }

        public HospitalDto ConsultarPorID(int idHospital)
        {
            return context.Hospital
                .Include(h => h.IdBancoNavigation)
                .Where(h => h.IdHospital == idHospital)
                .Select(h => new HospitalDto
                {
                    IdHospital = h.IdHospital,
                    Nombre = h.Nombre,
                    NombreBanco = h.IdBancoNavigation.Nombre,
                    Cuenta = h.Cuenta,
                    Clabe = h.Clabe
                })
                .FirstOrDefault();
        }

        public HospitalDto ConsultarDefaultPorCompania(int idCompania)
        {
            return context.Hospital
                .Where(h => h.IdCompania == idCompania && h.Predeterminada)
                .Select(h => new HospitalDto
                {
                    IdHospital = h.IdHospital,
                    Nombre = h.Nombre,
                    NombreComercial = h.NombreComercial,
                    Calle = h.Calle,
                    NumeroExterior = h.NumeroExterior,
                    NumeroInterior = h.NumeroInterior,
                    Colonia = h.Colonia,
                    CodigoPostal = h.CodigoPostal,
                    Telefono = h.Telefono,
                    Correo = h.Correo,
                    FechaContableActual = h.FechaContableActual,
                    IdUsuarioGerente = h.IdUsuarioGerente,
                    IdCompania = h.IdCompania,
                    IdEstado = h.IdEstado,
                    IdPais = h.IdEstadoNavigation.IdPais,
                    Ciudad = h.Ciudad,
                    IdBanco = h.IdBanco,
                    Cuenta = h.Cuenta,
                    Clabe = h.Clabe,
                    PortalWeb = h.PortalWeb,
                    Rfc = h.Rfc,
                    IdRegimenFiscal = h.IdRegimenFiscal,
                    IdLada = h.IdLada,
                    IdMunicipio = h.IdMunicipio,
                    EntreCalles = h.EntreCalles,
                    IdListaPrecioDefault = h.IdListaPrecioDefault,
                    IdListaPrecioLinea = h.IdListaPrecioLinea,
                    IdAlmacenCaduco = h.IdAlmacenCaduco,
                    IdAlmacenProduccion = h.IdAlmacenProduccion,
                    Predeterminada = h.Predeterminada
                })
                .FirstOrDefault();
        }

        public HospitalDto ConsultarDto(int idHospital)
        {
            return context.Hospital
                .OrderBy(h => h.IdHospital)
                .Where(h => h.IdHospital == idHospital)
                .Select(h => new HospitalDto
                {
                    IdHospital = h.IdHospital,
                    Nombre = h.Nombre,
                    NombreComercial = h.NombreComercial,
                    Calle = h.Calle,
                    NumeroExterior = h.NumeroExterior,
                    NumeroInterior = h.NumeroInterior,
                    Colonia = h.Colonia,
                    CodigoPostal = h.CodigoPostal,
                    Telefono = h.Telefono,
                    Correo = h.Correo,
                    FechaContableActual = h.FechaContableActual,
                    IdUsuarioGerente = h.IdUsuarioGerente,
                    IdCompania = h.IdCompania,
                    IdEstado = h.IdEstado,
                    IdPais = h.IdEstadoNavigation.IdPais,
                    Ciudad = h.Ciudad,
                    IdBanco = h.IdBanco,
                    Cuenta = h.Cuenta,
                    Clabe = h.Clabe,
                    PortalWeb = h.PortalWeb,
                    Rfc = h.Rfc,
                    IdRegimenFiscal = h.IdRegimenFiscal,
                    IdLada = h.IdLada,
                    IdMunicipio = h.IdMunicipio,
                    EntreCalles = h.EntreCalles,
                    IdListaPrecioDefault = h.IdListaPrecioDefault,
                    IdListaPrecioLinea = h.IdListaPrecioLinea,
                    IdAlmacenCaduco = h.IdAlmacenCaduco,
                    IdAlmacenProduccion = h.IdAlmacenProduccion,
                    Predeterminada = h.Predeterminada
                })
                .FirstOrDefault();
        }

        public Hospital ConsultarConDependencias(int idHospital)
        {
            return context.Hospital
                .Where(h => h.IdHospital == idHospital)
                .Select(h => new Hospital
                {
                    Caja = h.Caja,
                    Cita = h.Cita,
                    ListaPrecioClinica = h.ListaPrecioClinica,
                    Usuario = h.Usuario,
                    HospitalLogotipo = h.HospitalLogotipo,
                    EntradaPersonal = h.EntradaPersonal
                })
                .FirstOrDefault();
        }

        public Hospital Consultar(int idHospital)
        {
            return context.Hospital
                .Where(h => h.IdHospital == idHospital)
                .Include(h => h.HospitalLogotipo)
                .FirstOrDefault();
        }

        public Hospital Consultar(string rfc)
        {
            return context.Hospital
                .OrderBy(h => h.IdHospital)
                .Where(h => h.Rfc.ToLower() == rfc.ToLower())
                .Select(h => new Hospital
                {
                    IdHospital = h.IdHospital,
                    IdCompania = h.IdCompania
                })
                .FirstOrDefault();
        }

        public Hospital ConsultarPorUsuario(int idUsuario)
        {
            return context.Usuario
                .Where(u => u.IdUsuario == idUsuario)
                .Select(u => u.IdHospitalNavigation)
                .FirstOrDefault();
        }

        public IEnumerable<HospitalGridDto> ConsultarGeneral(int idCompania)
        {
            return context.Hospital
                .Where(h => h.IdCompania == idCompania)
                .OrderBy(h => h.IdHospital)
                .Select(h => new HospitalGridDto
                {
                    IdHospital = h.IdHospital,
                    Nombre = h.Nombre,
                    Ciudad = h.Ciudad,
                    Gerente = h.IdUsuarioGerenteNavigation.Nombre + " " +
                    h.IdUsuarioGerenteNavigation.ApellidoPaterno + " " +
                    h.IdUsuarioGerenteNavigation.ApellidoMaterno
                })
                .ToList();
        }

        public IEnumerable<HospitalDto> ConsultarPorCompania(int idCompania)
        {
            return context.Hospital
                .OrderBy(h => h.IdHospital)
                .Where(h => h.IdCompania == idCompania)
                .Select(h => new HospitalDto
                {
                    IdHospital = h.IdHospital,
                    IdSucursal = h.IdHospital,
                    Nombre = h.Nombre,
                    NombreComercial = h.NombreComercial,
                    Calle = h.Calle,
                    NumeroExterior = h.NumeroExterior,
                    NumeroInterior = h.NumeroInterior,
                    Colonia = h.Colonia,
                    CodigoPostal = h.CodigoPostal,
                    Telefono = h.Telefono,
                    Correo = h.Correo,
                    FechaContableActual = h.FechaContableActual,
                    IdUsuarioGerente = h.IdUsuarioGerente,
                    IdCompania = h.IdCompania,
                    IdEstado = h.IdEstado,
                    IdPais = h.IdEstadoNavigation.IdPais,
                    Ciudad = h.Ciudad,
                    IdBanco = h.IdBanco,
                    Cuenta = h.Cuenta,
                    Clabe = h.Clabe,
                    PortalWeb = h.PortalWeb,
                    Rfc = h.Rfc,
                    IdRegimenFiscal = h.IdRegimenFiscal,
                    IdLada = h.IdLada,
                    IdMunicipio = h.IdMunicipio,
                    EntreCalles = h.EntreCalles,
                    IdListaPrecioDefault = h.IdListaPrecioDefault,
                    IdListaPrecioLinea = h.IdListaPrecioLinea,
                    Predeterminada = h.Predeterminada
                })
                .ToList();
        }

        public IEnumerable<HospitalDto> ConsultarTodosParaSelector(int idDominio)
        {
            return context.Hospital
                .OrderBy(h => h.Nombre)
                .Include(h => h.DominioHospital)
                .Select(h => new HospitalDto
                {
                    IdHospital = h.IdHospital,
                    Nombre = h.DominioHospital.Any( dH => dH.IdDominio == idDominio) ?   h.Nombre : h.Nombre + " ( Indefinido )"
                })
                .ToList();
        }

        public IEnumerable<HospitalDto> ConsultarDisponiblesParaListaPrecio(int? idListaPrecioSeleccionada)
        {
            return context.Hospital
                .OrderBy(h => h.Nombre)
                .Where(h => !h.ListaPrecioClinica.Any(lpc => lpc.IdClinica == h.IdHospital) || h.ListaPrecioClinica.Any(lpc => lpc.IdListaPrecio == idListaPrecioSeleccionada))
                .Select(h => new HospitalDto
                {
                    IdHospital = h.IdHospital,
                    Nombre = h.Nombre
                })
                .ToList();
        }
    }
}
