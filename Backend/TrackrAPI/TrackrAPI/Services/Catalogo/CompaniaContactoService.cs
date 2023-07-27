using System.Collections.Generic;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class CompaniaContactoService
    {
        private readonly ICompaniaContactoRepository companiaContactoRepository;
        private readonly CompaniaContactoValidatorService companiaContactoValidatorService;

        public CompaniaContactoService(
            ICompaniaContactoRepository companiaContactoRepository,
            CompaniaContactoValidatorService companiaContactoValidatorService)
        {
            this.companiaContactoRepository = companiaContactoRepository;
            this.companiaContactoValidatorService = companiaContactoValidatorService;
        }

        public CompaniaContacto Consultar(int idCompania)
        {
            return companiaContactoRepository.Consultar(idCompania);
        }

        public IEnumerable<CompaniaContacto> ConsultarPorCompania(int idCompania)
        {
            return companiaContactoRepository.ConsultarPorCompania(idCompania);
        }

        public void Agregar(CompaniaContacto companiaContacto)
        {
            companiaContactoValidatorService.ValidarAgregar(companiaContacto);
            companiaContactoRepository.Agregar(companiaContacto);
        }

        public void Editar(CompaniaContacto companiaContacto)
        {
            companiaContactoValidatorService.ValidarEditar(companiaContacto);
            companiaContactoRepository.Editar(companiaContacto);
        }

        public void Eliminar(int idCompania)
        {
            companiaContactoValidatorService.ValidarEliminar(idCompania);

            CompaniaContacto companiaContacto = companiaContactoRepository.Consultar(idCompania);
            companiaContactoRepository.Eliminar(companiaContacto);
        }
    }
}