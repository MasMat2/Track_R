using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TrackrAPI.Repositorys.Catalogo;

public class MunicipioRepository : Repository<Municipio>, IMunicipioRepository
{
    public MunicipioRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public Municipio? Consultar(int idMunicipio)
    {
        return context.Municipio
            .Find(idMunicipio);
    }

    public Municipio? ConsultarParaFormulario(int idMunicipio)
    {
        return context.Municipio
            .Include(m => m.IdEstadoNavigation)
            .Where(m => m.IdMunicipio == idMunicipio)
            .FirstOrDefault();
    }

    public Municipio? Consultar(string nombre, int idEstado)
    {
        return context.Municipio
            .Where(m => m.Nombre.ToLower() == nombre.ToLower() && m.IdEstado == idEstado)
            .FirstOrDefault();
    }

    public Municipio? ConsultarPorClave(string clave)
    {
        return context.Municipio.
            Where(m => (m.Clave ?? "").ToLower() == clave.ToLower() )
            .FirstOrDefault();
    }

    public IEnumerable<Municipio> Consultar()
    {
        return context.Municipio
            .OrderBy(m => m.IdEstado);
    }

    public IEnumerable<Municipio> ConsultarParaGrid()
    {
        return context.Municipio
            .Include(m => m.IdEstadoNavigation)
                .ThenInclude(e => e.IdPaisNavigation);
    }

    public IEnumerable<Municipio> ConsultarPorEstado(int idEstado)
    {
        return context.Municipio
            .Where(m => m.IdEstado == idEstado);
    }

    public Municipio? ConsultarDependencias(int idMunicipio)
    {
        return context.Municipio
            .Include(m => m.CodigoPostal)
            .Include(m => m.Domicilio)
            .Include(m => m.Hospital)
            .Include(m => m.Usuario)
            //.Include(m => m.ExpedienteDatoSocial)
            //.Include(m => m.ExpedientePacienteInformacion)
            .Include(m => m.Compania)
            .Include(m => m.Direccion)
            .Where(m => m.IdMunicipio == idMunicipio)
            .FirstOrDefault();
    }

    public Municipio? ConsultarPorNombre(string nombre)
    {
        return context.Municipio
            .Where(m => m.Nombre.ToLower() == nombre.ToLower())
            .FirstOrDefault();
    }

    public IEnumerable<Municipio> ConsultarTodos()
    {
        return context.Municipio;
    }
}
