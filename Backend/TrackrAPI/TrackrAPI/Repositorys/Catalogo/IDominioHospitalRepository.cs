using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo;

public interface IDominioHospitalRepository : IRepository<DominioHospital>
{
    public DominioHospitalDto Consultar(int idHospital, int idDominio);
}

