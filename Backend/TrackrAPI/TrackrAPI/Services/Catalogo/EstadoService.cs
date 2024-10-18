using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using OfficeOpenXml;
using TrackrAPI.Helpers;
using TrackrAPI.Dtos.Archivos;
using Microsoft.AspNetCore.Mvc;

namespace TrackrAPI.Services.Catalogo
{
    public class EstadoService
    {
        private readonly IEstadoRepository estadoRepository;
        private readonly EstadoValidatorService estadoValidatorService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public EstadoService(
            IEstadoRepository estadoRepository,
            EstadoValidatorService estadoValidatorService,
            IWebHostEnvironment hostingEnvironment)
        {
            this.estadoRepository = estadoRepository;
            this.estadoValidatorService = estadoValidatorService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public EstadoDto? Consultar(int idEstado)
        {
            var estado = estadoRepository.Consultar(idEstado);

            if (estado is null)
            {
                return null;
            }

            var estadoDto = new EstadoDto
            {
                IdEstado = estado.IdEstado,
                Clave = estado.Clave ?? string.Empty,
                Nombre = estado.Nombre,
                IdPais = estado.IdPais
            };

            return estadoDto;
        }

        public EstadoFormularioConsultaDto? ConsultarParaFormulario(int idEstado)
        {
            var estado = estadoRepository.Consultar(idEstado);

            if (estado is null)
            {
                return null;
            }

            var estadoDto = new EstadoFormularioConsultaDto
            {
                IdEstado = estado.IdEstado,
                Clave = estado.Clave ?? string.Empty,
                Nombre = estado.Nombre,
                IdPais = estado.IdPais
            };

            return estadoDto;
        }

        public IEnumerable<EstadoGridDto> ConsultarParaGrid()
        {
            var estados = estadoRepository.ConsultarParaGrid();

            var estadosDto = estados
                .OrderBy(e => e.Nombre)
                .Select(e => new EstadoGridDto
                {
                    IdEstado = e.IdEstado,
                    Nombre = e.Nombre,
                    Clave = e.Clave ?? string.Empty,
                    NombrePais = e.IdPaisNavigation.Nombre
                });

            return estadosDto;
        }

        public IEnumerable<EstadoSelectorDto> ConsultarPorPaisParaSelector(int idPais)
        {
            var estados = estadoRepository.ConsultarPorPais(idPais);

            var estadosDto = estados
                .Select(e => new EstadoSelectorDto
                {
                    IdEstado = e.IdEstado,
                    Nombre = e.Nombre
                });

            return estadosDto;
        }

        public void Agregar(EstadoFormularioCapturaDto estadoDto)
        {
            estadoValidatorService.ValidarAgregar(estadoDto);

            var estado = new Estado
            {
                Clave = estadoDto.Clave,
                Nombre = estadoDto.Nombre,
                IdPais = estadoDto.IdPais
            };

            estadoRepository.Agregar(estado);
        }

        public void Editar(EstadoFormularioCapturaDto estadoDto)
        {
            estadoValidatorService.ValidarEditar(estadoDto);

            var estado = estadoRepository.Consultar(estadoDto.IdEstado)!;

            estado.Nombre = estadoDto.Nombre;
            estado.IdPais = estadoDto.IdPais;

            estadoRepository.Editar(estado);
        }

        public void Eliminar(int idEstado)
        {
            estadoValidatorService.ValidarEliminar(idEstado);

            var estado = estadoRepository.Consultar(idEstado)!;

            estadoRepository.Eliminar(estado);
        }

        private static void ValidarFormatoExcel(string excelBase64)
        {
            byte[] fileBytes = Convert.FromBase64String(excelBase64);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (MemoryStream stream = new (fileBytes))
            using (ExcelPackage package = new (stream))
            {
                try
                {
                    // Obtener la primera hoja de cálculo
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    // Verificar la existencia de las columnas esperadas
                    if (worksheet.Cells[1, 1].Text != "CATALOG_KEY" ||
                        worksheet.Cells[1, 2].Text != "ENTIDAD_FEDERATIVA" ||
                        worksheet.Cells[1, 3].Text != "ABREVIATURA")
                    {
                        throw new FormatException("El formato del archivo Excel no es válido. Las columnas esperadas no están presentes.");
                    }

                    // Verificar el tipo de datos en las celdas (opcional)
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text) ||
                            string.IsNullOrEmpty(worksheet.Cells[row, 2].Text) ||
                            string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                        {
                            throw new FormatException($"El formato del archivo Excel no es válido. Fila {row} contiene celdas vacías.");
                        }
                    }
                }
                catch
                {
                    throw new CdisException("El archivo Excel no tiene el formato correcto. Verifique que el archivo tenga el formato correcto y vuelva a intentarlo.");
                }
            }
        }

        public async Task SubirArchivoExcel(ArchivoCarga archivo)
        {
            ValidarFormatoExcel(archivo.ArchivoBase64);

            if (archivo == null || string.IsNullOrEmpty(archivo.ArchivoBase64))
            {
                throw new CdisException("No se proporcionó un archivo para subir.");
            }

            var path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "Excel", GeneralConstant.NombreExcelEstado);

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


        public List<EntidadFederativaExcelDto> ConsultarEstadosExcel()
        {
            string path = Path.Combine("Archivos", "Excel", GeneralConstant.NombreExcelEstado);
            var entidadFederativaList = new List<EntidadFederativaExcelDto>();

            // Load the Excel file
            FileInfo fileInfo = new(path);

                // Verificar si el archivo existe
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"El archivo Excel no se encontró en la ruta especificada: {path}");
            }

            
            byte[] fileBytes = File.ReadAllBytes(path);
            string excelBase64 = Convert.ToBase64String(fileBytes);
            ValidarFormatoExcel(excelBase64);

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

        public List<EntidadFederativaExcelDto> SincronizarPlantillaExcel()
        {
            var entidadesExcel = ConsultarEstadosExcel();
            var entidadesBdd = estadoRepository.ConsultarTodos().ToList();

            foreach (var entidadExcel in entidadesExcel)
            {
                var entidadBdd = entidadesBdd.FirstOrDefault(e => e.Nombre.Equals(entidadExcel.ENTIDAD_FEDERATIVA, StringComparison.OrdinalIgnoreCase));
                if (entidadBdd == null)
                {
                    entidadBdd = new Estado
                    {
                        Nombre = entidadExcel.ENTIDAD_FEDERATIVA,
                        IdPais = 1, // México
                        Clave = entidadExcel.CATALOG_KEY
                    };
                    var estadoAgregado = estadoRepository.Agregar(entidadBdd);

                    entidadExcel.IdEstado = estadoAgregado.IdEstado;
                }
                else
                {
                    entidadBdd.Nombre = entidadExcel.ENTIDAD_FEDERATIVA;
                    entidadBdd.Clave = entidadExcel.CATALOG_KEY;
                    estadoRepository.Editar(entidadBdd);                    
                    
                }

            }


            return entidadesExcel;
        }
    }
}
