using TrackrAPI.Dtos.GestionEgresos;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.GestionEgresos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Services.GestionEgresos
{
    public class TipoAuxiliarService
    {
        private ITipoAuxiliarRepository tipoAuxiliarRepository;
        private ISubtipoCuentaContableRepository subtipoCuentaContableRepository;
        private TipoAuxiliarValidatorService tipoAuxiliarValidatorService;

        public TipoAuxiliarService(ITipoAuxiliarRepository tipoAuxiliarRepository,
            ISubtipoCuentaContableRepository subtipoCuentaContableRepository,
            TipoAuxiliarValidatorService tipoAuxiliarValidatorService)
        {
            this.tipoAuxiliarRepository = tipoAuxiliarRepository;
            this.subtipoCuentaContableRepository = subtipoCuentaContableRepository;
            this.tipoAuxiliarValidatorService = tipoAuxiliarValidatorService;
        }

        public IEnumerable<TipoAuxiliarDto> ConsultarParaSelector()
        {
            return tipoAuxiliarRepository.ConsultarParaSelector();
        }

        public IEnumerable<TipoAuxiliarDto> ConsultarPorTipoCuentaParaSelector(int idTipoCuentaContable)
        {
            return tipoAuxiliarRepository.ConsultarPorTipoCuentaParaSelector(idTipoCuentaContable);
        }

        public TipoAuxiliar ConsultarPorClave(string clave)
        {
            return tipoAuxiliarRepository.ConsultarPorClave(clave);
        }

        public IEnumerable<TipoAuxiliar> ConsultarTodosParaGrid()
        {
            return tipoAuxiliarRepository.ConsultarTodos();
        }

        public TipoAuxiliar Consultar(int idTipoAuxiliar)
        {
            return tipoAuxiliarRepository.Consultar(idTipoAuxiliar);
        }

        public TipoAuxiliar GetByAccountSubtype(int accountSubtypeId)
        {
            var accountSubtype = subtipoCuentaContableRepository.Consultar(accountSubtypeId);

            switch (accountSubtype.Clave)
            {
                case GeneralConstant.SubtypeAccountCodeActivo:
                    return tipoAuxiliarRepository.ConsultarPorClave(GeneralConstant.TypeAuxiliaryCodeActivo);

                case GeneralConstant.SubtypeAccountCodePasivo:
                    return tipoAuxiliarRepository.ConsultarPorClave(GeneralConstant.TypeAuxiliaryCodePasivo);

                case GeneralConstant.SubtypeAccountCodeCapital:
                    return tipoAuxiliarRepository.ConsultarPorClave(GeneralConstant.TypeAuxiliaryCodeCapital);

                case GeneralConstant.SubtypeAccountCodeIngreso:
                    return tipoAuxiliarRepository.ConsultarPorClave(GeneralConstant.TypeAuxiliaryCodeCentroCosto);

                case GeneralConstant.SubtypeAccountCodeCosto:
                    return tipoAuxiliarRepository.ConsultarPorClave(GeneralConstant.TypeAuxiliaryCodeCentroCosto);

                case GeneralConstant.SubtypeAccountCodeGasto:
                    return tipoAuxiliarRepository.ConsultarPorClave(GeneralConstant.TypeAuxiliaryCodeCentroCosto);

                default:
                    return null;
            }

        }

        public int Agregar(TipoAuxiliar tipoAuxiliar)
        {
            tipoAuxiliarValidatorService.ValidarAgregar(tipoAuxiliar);
            tipoAuxiliarRepository.Agregar(tipoAuxiliar);

            return tipoAuxiliar.IdTipoAuxiliar;
        }

        public void Editar(TipoAuxiliar tipoAuxiliar)
        {
            tipoAuxiliarValidatorService.ValidarEditar(tipoAuxiliar);
            tipoAuxiliarRepository.Editar(tipoAuxiliar);
        }

        public void Eliminar(int idTipoAuxiliar)
        {
            TipoAuxiliar tipoAuxiliar = tipoAuxiliarRepository.Consultar(idTipoAuxiliar);
            tipoAuxiliarValidatorService.ValidarEliminar(idTipoAuxiliar);
            tipoAuxiliarRepository.Eliminar(tipoAuxiliar);
        }

    }
}
