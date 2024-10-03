using OfficeOpenXml;
using TrackrAPI.Dtos.Archivos;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo;

public class MunicipioService
{
    private readonly IMunicipioRepository _municipioRepository;
    private readonly MunicipioValidatorService _municipioValidatorService;
    private readonly EstadoService _estadoService;
    private readonly IWebHostEnvironment hostingEnvironment;

    public MunicipioService(
        IMunicipioRepository municipioRepository,
        MunicipioValidatorService municipioValidatorService,
        EstadoService estadoService,
        IWebHostEnvironment hostingEnvironment) {
        _municipioRepository = municipioRepository;
        _municipioValidatorService = municipioValidatorService;
        _estadoService = estadoService;
        this.hostingEnvironment = hostingEnvironment;
    }


    public IEnumerable<MunicipioSelectorDto> ConsultarTodosParaSelector()
    {
        var municipios = _municipioRepository.Consultar();

        var municipiosDto = municipios.Select(m => new MunicipioSelectorDto
        {
            IdMunicipio = m.IdMunicipio,
            Nombre = m.Nombre
        });

        return municipiosDto;
    }

    public MunicipioFormularioConsultaDto? ConsultarParaFormulario(int idMunicipio)
    {
        var municipio = _municipioRepository.ConsultarParaFormulario(idMunicipio);

        if (municipio == null)
        {
            return null;
        }

        var municipioDto = new MunicipioFormularioConsultaDto
        {
            IdMunicipio = municipio.IdMunicipio,
            IdPais = municipio.IdEstadoNavigation.IdPais,
            IdEstado = municipio.IdEstado,
            Nombre = municipio.Nombre,
            Clave = municipio.Clave ?? string.Empty,
        };

        return municipioDto;
    }

    public IEnumerable<MunicipioGridDto> ConsultarParaGrid()
    {
        var municipios = _municipioRepository.ConsultarParaGrid();

        var municipiosDto = municipios
            .Select(m => new MunicipioGridDto
            {
                IdMunicipio = m.IdMunicipio,
                Nombre = m.Nombre,
                Clave = m.Clave ?? string.Empty,
                NombreEstado = m.IdEstadoNavigation.Nombre,
                NombrePais = m.IdEstadoNavigation.IdPaisNavigation.Nombre
            })
            .OrderBy(m => m.NombrePais)
            .ThenBy(m => m.NombreEstado)
            .ThenBy(m => m.Nombre);

        return municipiosDto;
    }

    public IEnumerable<MunicipioSelectorDto> ConsultarPorEstadoParaSelector(int idEstado)
    {
        var municipios = _municipioRepository.ConsultarPorEstado(idEstado);

        var municipiosDto = municipios.Select(m => new MunicipioSelectorDto
        {
            IdMunicipio = m.IdMunicipio,
            Nombre = m.Nombre
        });

        return municipiosDto;
    }

    public void Agregar(MunicipioFormularioCapturaDto municipioDto)
    {
        _municipioValidatorService.ValidarAgregar(municipioDto);

        var municipio = new Municipio
        {
            Nombre = municipioDto.Nombre,
            IdEstado = municipioDto.IdEstado,
            Clave = municipioDto.Clave
        };

        _municipioRepository.Agregar(municipio);
    }

    public void Editar(MunicipioFormularioCapturaDto municipioDto)
    {
        _municipioValidatorService.ValidarEditar(municipioDto);

        var municipio = _municipioRepository.Consultar(municipioDto.IdMunicipio)!;

        municipio.Nombre = municipioDto.Nombre;
        municipio.IdEstado = municipioDto.IdEstado;

        _municipioRepository.Editar(municipio);
    }

    public void Eliminar(int idMunicipio)
    {
        _municipioValidatorService.ValidarEliminar(idMunicipio);

        var municipio = _municipioRepository.Consultar(idMunicipio)!;

        _municipioRepository.Eliminar(municipio);
    }

    
    private static void ValidarFormatoExcel(string excelBase64)
    {
        byte[] fileBytes = Convert.FromBase64String(excelBase64);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using MemoryStream stream = new(fileBytes);
        using ExcelPackage package = new(stream);
        try
        {
            // Obtener la primera hoja de cálculo
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            // Verificar la existencia de las columnas esperadas
            if (worksheet.Cells[1, 1].Text != "CVE_ENT" ||
                worksheet.Cells[1, 2].Text != "CVE_MUN" ||
                worksheet.Cells[1, 3].Text != "NOM_MUN")
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

    public async Task SubirArchivoExcel(ArchivoCarga archivo)
    {
        ValidarFormatoExcel(archivo.ArchivoBase64);

        if (archivo == null || string.IsNullOrEmpty(archivo.ArchivoBase64))
        {
            throw new CdisException("No se proporcionó un archivo para subir.");
        }

        var path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "Excel", GeneralConstant.NombreExcelMunicipio);

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

    public List<MunicipioExcelDto> SincronizarPlantillaExcel()
    {
        var municipiosExcel = ConsultarMunicipioExcel();
        var municipiosBdd = _municipioRepository.ConsultarTodos().ToList();

        var estadosExcel = _estadoService.SincronizarPlantillaExcel();

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
                        Nombre = municipio.NOM_MUN.Length > 50 ? municipio.NOM_MUN[..50] : municipio.NOM_MUN, // El limite de la bdd es 50
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
}
