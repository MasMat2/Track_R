using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo;

public class DominioHospitalService
{
    private readonly IDominioHospitalRepository _dominioHospitalRepository;

    public DominioHospitalService(IDominioHospitalRepository dominioHospitalRepository)
    {
        _dominioHospitalRepository = dominioHospitalRepository;
    }

    public void Agregar(DominioHospitalDto dominioHospitalDto)
    {
        var dominio = new DominioHospital
        {
            FechaMaxima = dominioHospitalDto.FechaMaxima,
            FechaMinima = dominioHospitalDto.FechaMinima,
            IdDominio = dominioHospitalDto.IdDominio,
            IdHospital = dominioHospitalDto.IdHospital,
            LongitudMaxima = dominioHospitalDto.LongitudMaxima,
            LongitudMinima = dominioHospitalDto.LongitudMinima,
            PermiteFueraDeRango = dominioHospitalDto.PermiteFueraDeRango,
            UnidadMedida = dominioHospitalDto.UnidadMedida,
            ValorMaximo = dominioHospitalDto.ValorMaximo,
            ValorMinimo = dominioHospitalDto.ValorMinimo
        };

        _dominioHospitalRepository.Agregar(dominio);
    }

    public DominioHospitalDto Consultar(int idDominioHospital)
    {
        return _dominioHospitalRepository.Consultar(idDominioHospital);
    }

    public void Modificar(DominioHospitalDto dominioHospitalDto)
    {
        var dominio = new DominioHospital
        {
            FechaMaxima = dominioHospitalDto.FechaMaxima,
            FechaMinima = dominioHospitalDto.FechaMinima,
            IdDominio = dominioHospitalDto.IdDominio,
            IdHospital = dominioHospitalDto.IdHospital,
            LongitudMaxima = dominioHospitalDto.LongitudMaxima,
            LongitudMinima = dominioHospitalDto.LongitudMinima,
            PermiteFueraDeRango = dominioHospitalDto.PermiteFueraDeRango,
            UnidadMedida = dominioHospitalDto.UnidadMedida,
            ValorMaximo = dominioHospitalDto.ValorMaximo,
            ValorMinimo = dominioHospitalDto.ValorMinimo,
            IdDominioHospital = (int)dominioHospitalDto.IdDominioHospital
        };

        _dominioHospitalRepository.Editar(dominio);
    }

    public void Eliminar(DominioHospitalDto dominioHospitalDto)
    {
        var dominio = new DominioHospital
        {
            FechaMaxima = dominioHospitalDto.FechaMaxima,
            FechaMinima = dominioHospitalDto.FechaMinima,
            IdDominio = dominioHospitalDto.IdDominio,
            IdHospital = dominioHospitalDto.IdHospital,
            LongitudMaxima = dominioHospitalDto.LongitudMaxima,
            LongitudMinima = dominioHospitalDto.LongitudMinima,
            PermiteFueraDeRango = dominioHospitalDto.PermiteFueraDeRango,
            UnidadMedida = dominioHospitalDto.UnidadMedida,
            ValorMaximo = dominioHospitalDto.ValorMaximo,
            ValorMinimo = dominioHospitalDto.ValorMinimo,
            IdDominioHospital = (int)dominioHospitalDto.IdDominioHospital
        };

        _dominioHospitalRepository.Eliminar(dominio);
    }
}

