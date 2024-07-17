
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Helpers;

namespace TrackrAPI.Services.Catalogo;

public class UnidadMedidaValidatorService
{
    private readonly IUnidadesMedidaRepository unidadesMedidaRepository;

    public UnidadMedidaValidatorService(IUnidadesMedidaRepository unidadesMedidaRepository)
    {
        this.unidadesMedidaRepository = unidadesMedidaRepository;
    }

    private readonly string MensajeNombreRequerido = "El nombre de la unidad de medida es requerido";
    private readonly string MensajeDuplicado = "La unidad de medida ya existe";
    private static readonly int LongitudNombre = 50;
    private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre} caracteres";
    private readonly string MensajeNoExistencia = "La unidad de medida no existe";


    public async Task ValidarAgregar(UnidadMedidaFormularioCapturaDto unidadMedidaDto)
    {
        ValidarRequerido(unidadMedidaDto);
        ValidarRango(unidadMedidaDto);
        await ValidarDuplicado(unidadMedidaDto);
    }

    public async Task ValidarEditar(UnidadMedidaFormularioCapturaDto unidadMedidaDto)
    {
        ValidarRequerido(unidadMedidaDto);
        ValidarRango(unidadMedidaDto);
        await ValidarDuplicado(unidadMedidaDto);
    }

    public async Task ValidarEliminar(int idUnidadMedida)
    {
        await ValidarExistencia(idUnidadMedida);
    }

    public void ValidarRequerido(UnidadMedidaFormularioCapturaDto unidadMedidaDto)
    {
        Validator.ValidarRequerido(unidadMedidaDto.Nombre, MensajeNombreRequerido);
    }

    public void ValidarRango(UnidadMedidaFormularioCapturaDto unidadMedidaDto)
    {
        Validator.ValidarLongitudRangoString(unidadMedidaDto.Nombre, LongitudNombre, MensajeNombreLongitud);
    }

    public async Task ValidarDuplicado(UnidadMedidaFormularioCapturaDto unidadMedidaDto)
    {
        var unidadMedida = await unidadesMedidaRepository.ConsultarPorNombre(unidadMedidaDto.Nombre);

        if (unidadMedida is not null)
        {
            throw new CdisException(MensajeDuplicado);
        }
    }

    public async Task ValidarExistencia(int idUnidadMedida)
    {
        var unidadMedida = await unidadesMedidaRepository.Consultar(idUnidadMedida);

        if (unidadMedida is null)
        {
            throw new CdisException(MensajeNoExistencia);
        }
    }
}