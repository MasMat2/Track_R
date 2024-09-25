using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using OfficeOpenXml;
using System.Collections.Concurrent;

namespace TrackrAPI.Services.Catalogo
{
    public class CodigoPostalService
    {
        private ICodigoPostalRepository codigoPostalRepository;
        private CodigoPostalValidatorService codigoPostalValidatorService;
        private readonly IEstadoRepository _estadoRepository;
        private readonly IMunicipioRepository _municipioRepository;

        public CodigoPostalService(ICodigoPostalRepository codigoPostalRepository,
             CodigoPostalValidatorService codigoPostalValidatorService,
             IEstadoRepository estadoRepository,
             IMunicipioRepository municipioRepository)
        {
            this.codigoPostalRepository = codigoPostalRepository;
            this.codigoPostalValidatorService = codigoPostalValidatorService;
            _estadoRepository = estadoRepository;
            _municipioRepository = municipioRepository;
        }

        public CodigoPostalDto ConsultarDto(int idCodigoPostal)
        {
            var codigoPostal = codigoPostalRepository.ConsultarDto(idCodigoPostal);
            codigoPostalValidatorService.ValidarExistencia(codigoPostal);
            return codigoPostal;
        }
        public IEnumerable<CodigoPostalGridDto> ConsultarTodosParaGrid()
        {
            return codigoPostalRepository.ConsultarTodosParaGrid();
        }

        public IEnumerable<CodigoPostalDto> ConsultarPorCodigoPostal(string codigoPostal)
        {
            return codigoPostalRepository.ConsultarPorCodigoPostal(codigoPostal);
        }

        public IEnumerable<CodigoPostalDto> ConsultarPorMunicipio(int idMunicipio)
        {
            return codigoPostalRepository.ConsultarPorMunicipio(idMunicipio);

        }



        public IEnumerable<CodigoPostalDto> ConsultarPorPaisBusqueda(string codigoPostal, int idPais)
        {
            return codigoPostalRepository.ConsultarPorPaisBusqueda(codigoPostal, idPais);
        }

        public void Agregar(CodigoPostal codigoPostal)
        {
            codigoPostalValidatorService.ValidarAgregar(codigoPostal);
            codigoPostalRepository.Agregar(codigoPostal);
        }

        public void Editar(CodigoPostal codigoPostal)
        {
            codigoPostalValidatorService.ValidarEditar(codigoPostal);
            codigoPostalRepository.Editar(codigoPostal);
        }

        public void Eliminar(int idCodigoPostal)
        {
            var codigoPostal = codigoPostalRepository.Consultar(idCodigoPostal);
            codigoPostalValidatorService.ValidarEliminar(idCodigoPostal);
            codigoPostalRepository.Eliminar(codigoPostal);
        }

        public List<EntidadFederativaExcelDto> ConsultarEstadosExcel()
        {
            string path = Path.Combine("Archivos", "Excel", "ENTIDAD_FEDERATIVA_201602.xlsx");
            var entidadFederativaList = new List<EntidadFederativaExcelDto>();

            // Load the Excel file
            FileInfo fileInfo = new(path);

                // Verificar si el archivo existe
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"El archivo Excel no se encontró en la ruta especificada: {path}");
            }
            // Set the license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new(fileInfo))
            {
                // Get the first worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // Iterate over each row starting from the second row
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var entidadFederativa = new EntidadFederativaExcelDto
                    {
                        CATALOG_KEY = worksheet.Cells[row, 1].Text,
                        ENTIDAD_FEDERATIVA = worksheet.Cells[row, 2].Text,
                        ABREVIATURA = worksheet.Cells[row, 3].Text
                    };

                    entidadFederativaList.Add(entidadFederativa);
                }
            }

            return entidadFederativaList;


        }


        // Existing methods...

        public List<MunicipioExcelDto> ConsultarMunicipioExcel()
        {
               string path = Path.Combine("Archivos", "Excel", "MUNICIPIOS_202407.xlsx");

            var municipioList = new List<MunicipioExcelDto>();

            // Load the Excel file
            FileInfo fileInfo = new(path);

                // Verificar si el archivo existe
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"El archivo Excel no se encontró en la ruta especificada: {path}");
            }
            // Set the license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new(fileInfo))
            {
                // Get the first worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // Iterate over each row starting from the second row
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var municipio = new MunicipioExcelDto
                    {
                        CVE_ENT = worksheet.Cells[row, 1].Text,
                        CVE_MUN = worksheet.Cells[row, 2].Text,
                        NOM_MUN = worksheet.Cells[row, 3].Text
                    };

                    municipioList.Add(municipio);
                }
            }

            return municipioList;
        }

        private List<EntidadFederativaExcelDto> ActualizarEstados()
        {
            var entidadesExcel = ConsultarEstadosExcel();
            var entidadesBdd = _estadoRepository.ConsultarTodos().ToList();

            foreach (var entidadExcel in entidadesExcel)
            {
                var entidadBdd = entidadesBdd.FirstOrDefault(e => e.Nombre.Equals(entidadExcel.ENTIDAD_FEDERATIVA, StringComparison.OrdinalIgnoreCase));
                if (entidadBdd == null)
                {
                    entidadBdd = new Estado
                    {
                        Nombre = entidadExcel.ENTIDAD_FEDERATIVA,
                        IdPais = 1, // México
                        Clave = entidadExcel.ABREVIATURA
                    };
                    var estadoAgregado = _estadoRepository.Agregar(entidadBdd);

                    entidadExcel.IdEstado = estadoAgregado.IdEstado;
                }
                else
                {
                    entidadExcel.IdEstado = entidadBdd.IdEstado;
                }

            }


            return entidadesExcel;
        }


        private List<MunicipioExcelDto> MapearMunicipios()
        {
            var municipiosExcel = ConsultarMunicipioExcel();
            var municipiosBdd = _municipioRepository.ConsultarTodos().ToList();

            var estadosExcel = ActualizarEstados();

            var municipiosConNombreEstado = new List<MunicipioExcelDto>();

            foreach (var municipio in municipiosExcel)
            {

                var municipioBdd = municipiosBdd.FirstOrDefault(m => m.Nombre.Equals(municipio.NOM_MUN, StringComparison.OrdinalIgnoreCase));
                if (municipioBdd == null)
                {
                    var estado = estadosExcel.FirstOrDefault(e => e.CATALOG_KEY == municipio.CVE_ENT);
                    if (estado != null)
                    {
                        var municipioAgregado = new Municipio
                        {
                            Nombre = municipio.NOM_MUN.Length > 50 ? municipio.NOM_MUN[..50] : municipio.NOM_MUN,
                            IdEstado = (int)estado.IdEstado,
                            Clave = municipio.CVE_MUN
                        };
                        var municipioAgregadoBdd = _municipioRepository.Agregar(municipioAgregado);
                        municipio.IdMunicipio = municipioAgregadoBdd.IdMunicipio;
                        municipio.IdEstado = municipioAgregadoBdd.IdEstado;

                        municipiosConNombreEstado.Add(municipio);
                    }
                }
                else
                {
                    municipio.IdMunicipio = municipioBdd.IdMunicipio;
                    municipio.IdEstado = municipioBdd.IdEstado;

                    municipiosConNombreEstado.Add(municipio);
                }


            }

            return municipiosConNombreEstado;
        }
        private List<CodigoPostalExcelDto> ConsultarCodigoPostalExcel()
        {
            string path = Path.Combine("Archivos", "Excel", "CODIGO_POSTAL_20240819.xlsx");
            var codigoPostalList = new List<CodigoPostalExcelDto>();

            // Load the Excel file
            FileInfo fileInfo = new FileInfo(path);

                // Verificar si el archivo existe
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"El archivo Excel no se encontró en la ruta especificada: {path}");
            }
            // Set the license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                // Iterate over each worksheet starting from the second one
                for (int i = 1; i < package.Workbook.Worksheets.Count; i++)
                {
                    var worksheet = package.Workbook.Worksheets[i];
                    var localList = new ConcurrentBag<CodigoPostalExcelDto>();

                    // Use Parallel.For to iterate over each row starting from the second row
                    Parallel.For(2, worksheet.Dimension.End.Row + 1, row =>
                    {
                        // Verificar si la celda clave (d_codigo) no está vacía
                        if (!string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Text))
                        {

                            var codigoPostal = new CodigoPostalExcelDto
                            {
                                d_codigo = worksheet.Cells[row, 1].Text,
                                d_asenta = worksheet.Cells[row, 2].Text,
                                d_tipo_asenta = worksheet.Cells[row, 3].Text,
                                D_mnpio = worksheet.Cells[row, 4].Text,
                                d_estado = worksheet.Cells[row, 5].Text,
                                d_ciudad = worksheet.Cells[row, 6].Text,
                                d_CP = worksheet.Cells[row, 7].Text,
                                c_estado = worksheet.Cells[row, 8].Text,
                                c_oficina = worksheet.Cells[row, 9].Text,
                                c_CP = worksheet.Cells[row, 10].Text,
                                c_tipo_asenta = worksheet.Cells[row, 11].Text,
                                c_mnpio = worksheet.Cells[row, 12].Text,
                                id_asenta_cpcons = worksheet.Cells[row, 13].Text,
                                d_zona = worksheet.Cells[row, 14].Text,
                                c_cve_ciudad = worksheet.Cells[row, 15].Text
                            };


                            localList.Add(codigoPostal);
                        }
                    });

                    codigoPostalList.AddRange(localList);
                }
            }
            return codigoPostalList;
        }
        public void CargaExcel()
        {
            var municipiosExcel = MapearMunicipios();
            var codigoPostalExcel = ConsultarCodigoPostalExcel();

            var codigoPostalList = new ConcurrentBag<CodigoPostal>();
        
            var municipiosDict = municipiosExcel
            .GroupBy(m => m.CVE_MUN)
            .ToDictionary(g => g.Key, g => g.First());

            // Usar Parallel.ForEach para procesar los códigos postales en paralelo
            Parallel.ForEach(codigoPostalExcel, codigoPostal =>
            {
                if (municipiosDict.TryGetValue(codigoPostal.c_mnpio, out var municipio))
                {
                    codigoPostal.idMunicipio = municipio.IdMunicipio;

                    var codigoPostalAAgregar = new CodigoPostal
                    {
                        CodigoPostal1 = codigoPostal.d_codigo,
                        Colonia = codigoPostal.d_asenta,
                        IdMunicipio = (int)codigoPostal.idMunicipio
                    };

                    codigoPostalList.Add(codigoPostalAAgregar);
                }
                else
                {
                    Console.WriteLine($"Error: Municipio con CVE_MUN {codigoPostal.c_mnpio} no encontrado para el código postal {codigoPostal.d_codigo}");
                }
            });

            var finalCodigoPostalList = codigoPostalList.ToList();
            codigoPostalRepository.Truncate();
            codigoPostalRepository.BulkInsert(finalCodigoPostalList);
        }



    }
}
