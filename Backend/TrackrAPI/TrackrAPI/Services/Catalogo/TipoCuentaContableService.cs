using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Services.Catalogo
{
    public class TipoCuentaContableService
    {

        private ITipoCuentaContableRepository tipoCuentaContableRepository;
        private TipoCuentaContableValidatorService tipoCuentaContableValidatorService;

        public TipoCuentaContableService(ITipoCuentaContableRepository tipoCuentaContableRepository,
            TipoCuentaContableValidatorService tipoCuentaContableValidatorService)
        {
            this.tipoCuentaContableRepository = tipoCuentaContableRepository;
            this.tipoCuentaContableValidatorService = tipoCuentaContableValidatorService;
        }

        public IEnumerable<TipoCuentaContableDto> ConsultarTodosParaGrid()
        {
            return tipoCuentaContableRepository.ConsultarTodosParaGrid();
        }

        public IEnumerable<TipoCuentaContableDto> ConsultarTodosParaSelector()
        {
            return tipoCuentaContableRepository.ConsultarTodosParaSelector();
        }

        public TipoCuentaContableDto ConsultarDto(int idTipoCuentaContable)
        {
            return tipoCuentaContableRepository.ConsultarDto(idTipoCuentaContable);
        }

        public void Agregar(TipoCuentaContable tipoCuentaContable)
        {
            tipoCuentaContableValidatorService.ValidarAgregar(tipoCuentaContable);
            tipoCuentaContableRepository.Agregar(tipoCuentaContable);
        }

        public void Editar(TipoCuentaContable tipoCuentaContable)
        {
            tipoCuentaContableValidatorService.ValidarEditar(tipoCuentaContable);
            tipoCuentaContableRepository.Editar(tipoCuentaContable);
        }

        public void Eliminar(int idTipoCuentaContable)
        {
            tipoCuentaContableValidatorService.ValidarEliminar(idTipoCuentaContable);
            var tipoCuentaContable = tipoCuentaContableRepository.Consultar(idTipoCuentaContable);
            tipoCuentaContableRepository.Eliminar(tipoCuentaContable);
        }


    }
}
