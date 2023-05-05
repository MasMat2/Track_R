using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class BancoService
    {
        private IBancoRepository bancoRepository;

        public BancoService(IBancoRepository bancoRepository)
        {
            this.bancoRepository = bancoRepository;
        }

        public IEnumerable<BancoDto> ConsultarTodosParaSelector()
        {
            return bancoRepository.ConsultarTodosParaSelector();
        }
    }
}
