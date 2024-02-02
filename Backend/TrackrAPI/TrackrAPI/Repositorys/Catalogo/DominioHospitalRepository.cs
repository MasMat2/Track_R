using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo;

public class DominioHospitalRepository : Repository<DominioHospital>, IDominioHospitalRepository
{
    public DominioHospitalRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public DominioHospitalDto Consultar(int idHospital, int idDominio)
    {
        return context.DominioHospital
                      .Where(x => (x.IdDominio == idDominio) && (x.IdHospital == idHospital))
                      .Select(x => new DominioHospitalDto
                      {
                          IdDominioHospital = x.IdDominioHospital,
                          FechaMaxima = x.FechaMaxima,
                          FechaMinima = x.FechaMinima,
                          IdDominio = (int)x.IdDominio,
                          IdHospital = (int)x.IdHospital,
                          LongitudMaxima = x.LongitudMaxima,
                          LongitudMinima = x.LongitudMinima,
                          PermiteFueraDeRango = x.PermiteFueraDeRango,
                          UnidadMedida = x.UnidadMedida,
                          ValorMaximo = x.ValorMaximo,
                          ValorMinimo = x.ValorMinimo,

                      })
                      .FirstOrDefault();
    }
}


