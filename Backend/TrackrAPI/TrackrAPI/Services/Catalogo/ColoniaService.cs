using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using OfficeOpenXml;
using System.Collections.Concurrent;
using System.Transactions;

namespace TrackrAPI.Services.Catalogo
{
    public class ColoniaService
    {
        private IColoniaRepository coloniaRepository;
        private ColoniaValidatorService coloniaValidatorService;
        private readonly CodigoPostalService _codigoPostalService;
        private readonly MunicipioService _municipioService;

        public ColoniaService(
            IColoniaRepository coloniaRepository,
            ColoniaValidatorService coloniaValidatorService,
            CodigoPostalService codigoPostalService,
            MunicipioService municipioService
        )
        {
            this.coloniaRepository = coloniaRepository;
            this.coloniaValidatorService = coloniaValidatorService;
            _codigoPostalService = codigoPostalService;
            _municipioService = municipioService;
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
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var codigosPostalesExcel = _codigoPostalService.ConsultarCodigoPostalExcel();
                var codigosPostalesBdd = _codigoPostalService.ConsultarTodos();
                var coloniasBdd = coloniaRepository.Consultar();
                var municipios = _municipioService.ConsultarTodos();

                var coloniasAAgregar = new ConcurrentBag<Colonia>();
                var coloniasAEditar = new ConcurrentBag<Colonia>();


                // Crear un ConcurrentDictionary para almacenar colonias únicas por nombre
                var coloniasBddDict = new ConcurrentDictionary<string, Colonia>(
                    coloniasBdd.GroupBy(x => new { x.Nombre, x })
                            .Select(g => g.First())
                            .ToDictionary(x => $"{x.Nombre}_{x.IdCodigoPostalNavigation.IdMunicipio}", x => x)
                );


                var codigoPostalDict = new ConcurrentDictionary<string, CodigoPostal>(
                    codigosPostalesBdd.GroupBy(x => new { x.CodigoPostal1, x })
                            .Select(g => g.First())
                            .ToDictionary(x => x.CodigoPostal1, x => x)
                );

                var coloniasExcel = codigosPostalesExcel.Select(x => new ColoniaDto
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
                            int idCodigoPostal;
                            codigoPostalDict.TryGetValue(coloniaExcel.CodigoPostal, out var codigoPostalDto);

                            if (codigoPostalDto == null)
                            {
                                Console.WriteLine($"No se encontró el código postal {coloniaExcel.CodigoPostal}");
                                idCodigoPostal = codigoPostalDict.FirstOrDefault().Value.IdCodigoPostal;
                            }
                            else
                            {
                                idCodigoPostal = codigoPostalDto.IdCodigoPostal;
                            }

                            coloniaBdd.Clave = coloniaExcel.Clave;
                            coloniaBdd.IdCodigoPostal = idCodigoPostal;
                            coloniasAEditar.Add(coloniaBdd);
                        }
                    }
                    else
                    {
                        int idCodigoPostal;
                        codigoPostalDict.TryGetValue(coloniaExcel.CodigoPostal, out var codigoPostalDto);

                        if (codigoPostalDto == null)
                        {
                            Console.WriteLine($"No se encontró el código postal {coloniaExcel.CodigoPostal}");
                            idCodigoPostal = codigoPostalDict.FirstOrDefault().Value.IdCodigoPostal;
                        }
                        else
                        {
                            idCodigoPostal = codigoPostalDto.IdCodigoPostal;
                        }

                        var coloniaAAgregar = new Colonia
                        {
                            Nombre = coloniaExcel.Nombre,
                            Clave = coloniaExcel.Clave,
                            IdCodigoPostal = idCodigoPostal
                        };

                        coloniasAAgregar.Add(coloniaAAgregar);
                    }
                });

                coloniaRepository.Agregar(coloniasAAgregar.ToList());
                coloniaRepository.Editar(coloniasAEditar.ToList());
                scope.Complete();
            }
        }
    }
}
