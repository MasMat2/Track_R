using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using OfficeOpenXml;
using System.Collections.Concurrent;
using TrackrAPI.Helpers;
using TrackrAPI.Dtos.Archivos;

namespace TrackrAPI.Services.Catalogo
{
    public class CodigoPostalService
    {
        private ICodigoPostalRepository codigoPostalRepository;
        private CodigoPostalValidatorService codigoPostalValidatorService;
        private readonly IEstadoRepository _estadoRepository;
        private readonly IMunicipioRepository _municipioRepository;
        private readonly EstadoService _estadoService;
        private readonly MunicipioService _municipioService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public CodigoPostalService(ICodigoPostalRepository codigoPostalRepository,
             CodigoPostalValidatorService codigoPostalValidatorService,
             IEstadoRepository estadoRepository,
             IMunicipioRepository municipioRepository,
             EstadoService estadoService,
             MunicipioService municipioService,
             IWebHostEnvironment hostingEnvironment)
        {
            this.codigoPostalRepository = codigoPostalRepository;
            this.codigoPostalValidatorService = codigoPostalValidatorService;
            _estadoRepository = estadoRepository;
            _municipioRepository = municipioRepository;
            _estadoService = estadoService;
            _municipioService = municipioService;
            this.hostingEnvironment = hostingEnvironment;
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

        public IEnumerable<CodigoPostal> ConsultarTodos()
        {
            return codigoPostalRepository.ConsultarTodos();
        }

        // Existing methods...

        private static void ValidarFormatoExcel(string excelBase64)
        {
            byte[] fileBytes = Convert.FromBase64String(excelBase64);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using MemoryStream stream = new(fileBytes);
            using ExcelPackage package = new(stream);
            try
            {
                // Obtener la primera hoja de cálculo
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];


                // Verificar la existencia de las columnas esperadas
                if (worksheet.Cells[1, 1].Text != "d_codigo" ||
                    worksheet.Cells[1, 2].Text != "d_asenta" ||
                    worksheet.Cells[1, 3].Text != "d_tipo_asenta" ||
                    worksheet.Cells[1, 4].Text != "D_mnpio" ||
                    worksheet.Cells[1, 5].Text != "d_estado" ||
                    worksheet.Cells[1, 6].Text != "d_ciudad" ||
                    worksheet.Cells[1, 7].Text != "d_CP" ||
                    worksheet.Cells[1, 8].Text != "c_estado" ||
                    worksheet.Cells[1, 9].Text != "c_oficina" ||
                    worksheet.Cells[1, 10].Text != "c_CP" ||
                    worksheet.Cells[1, 11].Text != "c_tipo_asenta" ||
                    worksheet.Cells[1, 12].Text != "c_mnpio" ||
                    worksheet.Cells[1, 13].Text != "id_asenta_cpcons" ||
                    worksheet.Cells[1, 14].Text != "d_zona" ||
                    worksheet.Cells[1, 15].Text != "c_cve_ciudad")
                {
                    throw new FormatException("El formato del archivo Excel no es válido. Las columnas esperadas no están presentes.");
                }


    
            }
            catch
            {
                throw new CdisException("El archivo Excel no tiene el formato correcto. Verifique que el archivo tenga el formato correcto y vuelva a intentarlo.");
            }
        }

        public async Task SubirArchivoExcel(ArchivoCarga archivo)
        {
            ValidarFormatoExcel(archivo.ArchivoBase64);

            if (archivo == null || string.IsNullOrEmpty(archivo.ArchivoBase64))
            {
                throw new CdisException("No se proporcionó un archivo para subir.");
            }

            var path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "Excel", GeneralConstant.NombreExcelCodigoPostal);

            // Ensure the directory exists
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Delete the existing file if it exists
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            // Decode the Base64 string
            byte[] fileBytes = Convert.FromBase64String(archivo.ArchivoBase64);

            // Save the file
            await File.WriteAllBytesAsync(path, fileBytes);
        }

        public  List<CodigoPostalExcelDto> ConsultarCodigoPostalExcel()
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
            var municipiosExcel = _municipioService.SincronizarPlantillaExcel();
            var codigoPostalExcel = ConsultarCodigoPostalExcel();

            var codigosPostalesUnicos = codigoPostalExcel
                .GroupBy(c => c.d_codigo)
                .Select(g => g.First())
                .ToList();

            var codigoPostalBdd = codigoPostalRepository.ConsultarTodos();

            var codigoPostalAAgregar = new List<CodigoPostal>();

            

            var codigoPostalList = new ConcurrentBag<CodigoPostal>();
        
        var municipiosDict = municipiosExcel
            .GroupBy(m => new { m.CVE_ENT, m.CVE_MUN }) // Agrupa por la combinación de CVE_ENT y CVE_MUN
            .Select(g => g.First())  // Selecciona el primer elemento de cada grupo
            .ToDictionary(m => $"{m.CVE_ENT}_{m.CVE_MUN}", m => m); // Crea un diccionario con la clave formada por CVE_ENT y CVE_MUN

            // Usar Parallel.ForEach para procesar los códigos postales en paralelo
            Parallel.ForEach(codigosPostalesUnicos, codigoPostal =>
            {
                if (municipiosDict.TryGetValue($"{codigoPostal.c_estado}_{codigoPostal.c_mnpio}", out var municipio))
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
            codigoPostalRepository.EliminarSinDependencias();
            codigoPostalRepository.BulkInsert(finalCodigoPostalList);
        }



    }
}
