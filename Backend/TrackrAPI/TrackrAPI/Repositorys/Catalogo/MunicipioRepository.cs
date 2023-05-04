using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class MunicipioRepository : Repository<Municipio>, IMunicipioRepository
    {
        public MunicipioRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Municipio Consultar(int idMunicipio)
        {
            return context.Municipio.Where(e => e.IdMunicipio == idMunicipio).FirstOrDefault();
        }

        public Municipio Consultar(string nombre, int idEstado)
        {
            return context.Municipio.
                Where(e => e.Nombre.ToLower() == nombre.ToLower() && e.IdEstado == idEstado)
                .FirstOrDefault();
        }

        public Municipio ConsultarPorClave(string Clave)
        {
            return context.Municipio.
                Where(e => e.Clave.ToLower() == Clave.ToLower() )
                .FirstOrDefault();
        }

        public MunicipioDto ConsultarDto(int idMunicipio)
        {
            return context.Municipio
                .Where(e => e.IdMunicipio == idMunicipio)
                .Select(e => new MunicipioDto
                {
                    IdMunicipio = e.IdMunicipio,
                    Nombre = e.Nombre,
                    IdEstado = e.IdEstado,
                    Clave = e.Clave

                })
                .FirstOrDefault();
        }

        public IEnumerable<MunicipioDto> ConsultarTodosParaSelector()
        {
            return context.Municipio
                .OrderBy(cc => cc.Nombre)
                .Select(cc => new MunicipioDto
                 {
                  IdMunicipio = cc.IdMunicipio,
                    Nombre = cc.Nombre,
                    IdEstado = cc.IdEstado

                })
                .ToList();
        }

        public IEnumerable<EstadoSelectorDto> ConsultarPorPaisParaSelector(int idPais)
        {
            return context.Estado
                .Where(e => e.IdPais == idPais)
                .OrderBy(e => e.Nombre)
                .Select(e => new EstadoSelectorDto(
                    e.IdEstado,
                    e.Nombre
                    )).ToList();
        }

        public IEnumerable<MunicipioGridDto> ConsultarTodosParaGrid()
        {
            return context.Municipio
                .OrderBy(e => e.IdEstadoNavigation.Nombre)
                .Select(e => new MunicipioGridDto(
                    e.IdMunicipio,
                    e.Nombre,
                    e.IdEstadoNavigation.Nombre,
                    e.IdEstadoNavigation.IdPaisNavigation.Nombre,
                    e.Clave
                    )).ToList();
        }

        public IEnumerable<MunicipioDto> ConsultarPorEstadoParaSelector(int idEstado)
        {
            return context.Municipio
                .OrderBy(m => m.Nombre)
                .Where(m => m.IdEstado == idEstado)
                .Select(m => new MunicipioDto
                {
                    IdMunicipio = m.IdMunicipio,
                    Nombre = m.Nombre
                })
                .ToList();
        }

        public bool ConsultarDependencias(int idMunicipio)
        {
            bool tieneDependencia = false;

            var municipio = context.Municipio
                .Where(m => m.IdMunicipio == idMunicipio)
                .FirstOrDefault();

            for (int i = 1; i < 4; i++) {
                tieneDependencia = ComprobarDependencias(municipio, i);

                if (tieneDependencia)
                    break;
            }

            return tieneDependencia;
        }

        public bool ComprobarDependencias(Municipio municipio, int caso) {
            bool tieneDependencia = false;
            object dependencia = null;

            switch (caso)
            {
                case 1:
                    dependencia = context.CodigoPostal
                        .Where(cp => cp.IdMunicipio == municipio.IdMunicipio)
                        .FirstOrDefault();
                    break;
                case 2:
                    dependencia = context.ExpedienteDatoSocial
                        .Where(ed => ed.IdCiudadNacimiento == municipio.IdMunicipio)
                        .FirstOrDefault();
                    break;
                case 3:
                    dependencia = context.ExpedientePacienteInformacion
                        .Where(ei => ei.IdMunicipio == municipio.IdMunicipio)
                        .FirstOrDefault();
                    break;
            }

            if (dependencia != null)
                tieneDependencia = true;

            return tieneDependencia;
        }
    }
}
