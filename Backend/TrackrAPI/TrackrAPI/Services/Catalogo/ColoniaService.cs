using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using OfficeOpenXml;
using System.Collections.Concurrent;

namespace TrackrAPI.Services.Catalogo
{
    public class ColoniaService
    {
        private IColoniaRepository coloniaRepository;
        private ColoniaValidatorService coloniaValidatorService;
        private readonly CodigoPostalService _codigoPostalService;
        private readonly IMunicipioRepository _municipioRepository;

        public ColoniaService(
            IColoniaRepository coloniaRepository,
            ColoniaValidatorService coloniaValidatorService,
            CodigoPostalService codigoPostalService,
            IMunicipioRepository municipioRepository
        )
        {
            this.coloniaRepository = coloniaRepository;
            this.coloniaValidatorService = coloniaValidatorService;
            _codigoPostalService = codigoPostalService;
            _municipioRepository = municipioRepository;
        }

        public IEnumerable<Colonia> ConsultarPorCodigoParaSelector(string codigoPostal)
        {
            return coloniaRepository.ConsultarPorCodigoParaSelector(codigoPostal);
        }

        public IEnumerable<ColoniaGridDto> ConsultarParaGrid()
        {
            return coloniaRepository.ConsultarParaGrid();
        }

        public void Agregar(Colonia colonia)
        {
            this.coloniaValidatorService.ValidarAgregar(colonia);
            this.coloniaRepository.Agregar(colonia);
        }

        public void Editar(Colonia colonia)
        {
            this.coloniaValidatorService.ValidarEditar(colonia);
            this.coloniaRepository.Editar(colonia);
        }

        public void Eliminar(int idColonia)
        {
            var colonia = coloniaRepository.Consultar(idColonia);
            this.coloniaValidatorService.ValidarEliminar(colonia.IdColonia);
            this.coloniaRepository.Eliminar(colonia);
        }

        public void ActualizarPlantillaExcel()
        {
            var codigosPostalesExcel = _codigoPostalService.ConsultarCodigoPostalExcel();
            var municipios = _municipioRepository.ConsultarTodosDto();

            var coloniasAAgregar = new ConcurrentBag<Colonia>();
            var coloniasAEditar = new ConcurrentBag<Colonia>();

            var coloniasBdd = coloniaRepository.Consultar();

            // Crear un ConcurrentDictionary para almacenar colonias únicas por nombre
            var coloniasBddDict = new ConcurrentDictionary<string, Colonia>(
                coloniasBdd.GroupBy(x => new { x.Nombre, x })
                        .Select(g => g.First())
                        .ToDictionary(x => $"{x.Nombre}_{x.IdMunicipio}", x => x)
            );

            var municipiosBddDict = new ConcurrentDictionary<string, MunicipioDto>(
                municipios.GroupBy(x => new { x.ClaveEstado, x })
                        .Select(g => g.First())
                        .ToDictionary(m => $"{m.Nombre}_{m.ClaveEstado}_{m.Clave}", x => x)
            );

            var coloniasExcel = codigosPostalesExcel.DistinctBy(x => new { x.d_asenta, x.c_mnpio }).Select(x => new ColoniaDto
            {
                Nombre = x.d_asenta,
                Clave = new string(x.d_asenta.Take(3).ToArray()) + new string(x.c_estado.Take(1).ToArray()),
                CodigoPostal = x.d_codigo,
                ClaveEstado = x.c_estado,
                ClaveMunicipio = x.c_mnpio,
                NombreMunicipio = x.D_mnpio
            });

            Parallel.ForEach(coloniasExcel, coloniaExcel =>
            {
            if (coloniasBddDict.TryGetValue($"{coloniaExcel.Nombre}_{coloniaExcel.IdMunicipio}", out var coloniaBdd))
            {
                if (coloniaBdd.Nombre != coloniaExcel.Nombre || coloniaBdd.Clave != coloniaExcel.Clave)
                {
                    int idMunicipio;
                    municipiosBddDict.TryGetValue($"{coloniaExcel.NombreMunicipio.ToUpper()}_{coloniaExcel.ClaveEstado}_{coloniaExcel.ClaveMunicipio}", out var municipioDto);
                   

                    if(municipioDto == null)
                    {
                        Console.WriteLine($"No se encontró el municipio con clave {coloniaExcel.ClaveMunicipio}");
                        idMunicipio = municipiosBddDict.FirstOrDefault().Value.IdMunicipio;
                    }else
                    {
                        idMunicipio = municipioDto.IdMunicipio;
                    }

                    coloniaBdd.Clave = coloniaExcel.Clave;
                    coloniaBdd.CodigoPostal = coloniaExcel.CodigoPostal;
                    coloniaBdd.IdMunicipio = idMunicipio;
                    coloniasAEditar.Add(coloniaBdd);
                }
            }
                else
                {
                    int idMunicipio;
                    municipiosBddDict.TryGetValue($"{coloniaExcel.NombreMunicipio.ToUpper()}_{coloniaExcel.ClaveEstado}_{coloniaExcel.ClaveMunicipio}", out var municipioDto);
                   

                    if(municipioDto == null)
                    {
                        Console.WriteLine($"No se encontró {coloniaExcel.NombreMunicipio.ToUpper()}_{coloniaExcel.ClaveEstado}_{coloniaExcel.ClaveMunicipio}");
                        idMunicipio = municipiosBddDict.FirstOrDefault().Value.IdMunicipio;
                    }else
                    {
                        idMunicipio = municipioDto.IdMunicipio;
                    }

                    var coloniaAAgregar = new Colonia
                    {
                        Nombre = coloniaExcel.Nombre,
                        Clave = coloniaExcel.Clave,
                        CodigoPostal = coloniaExcel.CodigoPostal,
                        IdMunicipio = idMunicipio
                    };

                    coloniasAAgregar.Add(coloniaAAgregar);
                }
            });

            coloniaRepository.Agregar(coloniasAAgregar.ToList());
            coloniaRepository.Editar(coloniasAEditar.ToList());
        }
    }
}
