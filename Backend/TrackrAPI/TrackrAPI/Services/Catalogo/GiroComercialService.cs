using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class GiroComercialService
    {
        private IGiroComercialRepository giroComercialRepository;
        private GiroComercialValidatorService giroComercialValidatorService;

        public GiroComercialService(
            IGiroComercialRepository giroComercialRepository,
            GiroComercialValidatorService giroComercialValidatorService
        )
        {
            this.giroComercialRepository = giroComercialRepository;
            this.giroComercialValidatorService = giroComercialValidatorService;
        }

        public IEnumerable<GiroComercial> ConsultarTodos()
        {
            return giroComercialRepository.ConsultarTodos();
        }

        public GiroComercial Consultar(int idGiroComercial)
        {
            return giroComercialRepository.Consultar(idGiroComercial);
        }

        public int Agregar(GiroComercial giroComercial)
        {
            giroComercialValidatorService.ValidarAgregar(giroComercial);
            giroComercialRepository.Agregar(giroComercial);
            return giroComercial.IdGiroComercial;
        }

        public void Editar(GiroComercial giroComercial)
        {
            giroComercialValidatorService.ValidarEditar(giroComercial);
            giroComercialRepository.Editar(giroComercial);
        }

        public void Eliminar(int idGiroComercial)
        {
            giroComercialValidatorService.ValidarEliminar(idGiroComercial);

            GiroComercial giroComercial = giroComercialRepository.Consultar(idGiroComercial);
            giroComercialRepository.Eliminar(giroComercial);
        }
    }
}
