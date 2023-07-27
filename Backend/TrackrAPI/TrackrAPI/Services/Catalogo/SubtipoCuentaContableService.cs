using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Services.Catalogo
{
    public class SubtipoCuentaContableService {

    private ISubtipoCuentaContableRepository subtipoCuentaContableRepository;
        private SubtipoCuentaContableValidatorService subtipoCuentaContableValidatorService;
    public SubtipoCuentaContableService(ISubtipoCuentaContableRepository subtipoCuentaContableRepository,
        SubtipoCuentaContableValidatorService subtipoCuentaContableValidatorService)
    {
        this.subtipoCuentaContableRepository = subtipoCuentaContableRepository;
            this.subtipoCuentaContableValidatorService = subtipoCuentaContableValidatorService;
    }

    public IEnumerable<SubtipoCuentaContableDto> ConsultarTodosParaSelector()
    {
        return subtipoCuentaContableRepository.ConsultarTodosParaSelector();
    }

        public IEnumerable<SubtipoCuentaContableDto> ConsultarPorTipoParaSelector(int idTipoCuentaContable)
        {
            return subtipoCuentaContableRepository.ConsultarPorTipoParaSelector(idTipoCuentaContable);
        }


        public SubtipoCuentaContableDto ConsultarDto(int idTipoCuentaContable)
        {
            var subtipoCuentaContable = subtipoCuentaContableRepository.ConsultarDto(idTipoCuentaContable);
            subtipoCuentaContableValidatorService.ValidarExistencia(subtipoCuentaContable);
            return subtipoCuentaContable;
        }
    }


}
