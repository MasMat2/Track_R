using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo;

public class MunicipioValidatorService
{
    private readonly IMunicipioRepository _municipioRepository;

    public MunicipioValidatorService(IMunicipioRepository municipioRepository) {
        this._municipioRepository = municipioRepository;
    }

    private readonly string MensajeNombreRequerido = "El nombre es requerido";
    private readonly string MensajeEstadoRequerido = "El estado es requerido";
    private readonly string MensajeExistencia = "El municipio no existe";
    private readonly string MensajeDuplicado = "El municipio ya existe";
    private readonly string MensajeClaveDuplicado = "La clave ya existe";

    private readonly string MensajeDependencia = "El municipio esta asociado al menos a una localidad y no se puede eliminar";

    private static readonly int LongitudNombre = 50;

    private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";

    public void ValidarAgregar(MunicipioFormularioCapturaDto municipio)
    {
        ValidarRequerido(municipio);
        ValidarRango(municipio);
        ValidarDuplicado(municipio);
    }

    public void ValidarEditar(MunicipioFormularioCapturaDto municipio)
    {
        ValidarRequerido(municipio);
        ValidarRango(municipio);
        ValidarExistencia(municipio.IdMunicipio);
        ValidarDuplicado(municipio);
    }

    public void ValidarEliminar(int idMunicipio)
    {
        var municipio = _municipioRepository.Consultar(idMunicipio);

        ValidarExistencia(idMunicipio);
        ValidarDependencia(idMunicipio);
    }

    public void ValidarRequerido(MunicipioFormularioCapturaDto municipio)
    {
        Validator.ValidarRequerido(municipio.Nombre, MensajeNombreRequerido);
        Validator.ValidarRequerido(municipio.IdEstado, MensajeEstadoRequerido);
    }

    public void ValidarRango(MunicipioFormularioCapturaDto municipio)
    {
        Validator.ValidarLongitudRangoString(municipio.Nombre, LongitudNombre, MensajeNombreLongitud);
    }

    public void ValidarExistencia(MunicipioDto municipio)
    {
        if (municipio == null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }

    public void ValidarExistencia(int idMunicipio)
    {
        var municipio = _municipioRepository.Consultar(idMunicipio);

        if (municipio == null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }

    public void ValidarDuplicado(MunicipioFormularioCapturaDto municipio)
    {
        var municipioDuplicado = _municipioRepository.Consultar(municipio.Nombre, municipio.IdEstado);

        if (municipioDuplicado != null && municipio.IdMunicipio != municipioDuplicado.IdMunicipio)
        {
            throw new CdisException(MensajeDuplicado);
        }

        var municipioClaveDuplicada = _municipioRepository.ConsultarPorClave(municipio.Clave);

        if (municipioClaveDuplicada != null && municipio.IdMunicipio != municipioClaveDuplicada.IdMunicipio)
        {
            throw new CdisException(MensajeClaveDuplicado);
        }

    }

    public void ValidarDependencia(int idMunicipio)
    {
        var municipio = _municipioRepository.ConsultarDependencias(idMunicipio)!;

        if (municipio.CodigoPostal.Any())
        {
            throw new CdisException(MensajeDependencia);
        }
    }
}
