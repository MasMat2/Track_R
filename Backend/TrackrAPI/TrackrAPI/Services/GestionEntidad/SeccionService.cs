using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace TrackrAPI.Services.GestionEntidad
{
    public class SeccionService
    {
        private readonly ISeccionRepository seccionRepository;
        private readonly SeccionValidatorService seccionValidatorService;
        private readonly ISeccionCampoRepository seccionCampoRepository;
        public SeccionService(ISeccionRepository seccionRepository,
            SeccionValidatorService seccionValidatorService,
            ISeccionCampoRepository seccionCampoRepository)
        {
            this.seccionRepository = seccionRepository;
            this.seccionValidatorService = seccionValidatorService;
            this.seccionCampoRepository = seccionCampoRepository;
        }

        public Seccion Consultar(int idSeccion)
        {
            var seccion = seccionRepository.Consultar(idSeccion);
            seccionValidatorService.ValidarExistencia(seccion);
            return seccion;
        }

        public Seccion ConsultarPorClave(string clave)
        {
            var seccion = seccionRepository.ConsultarPorClave(clave);
            return seccion;
        }

        public IEnumerable<SeccionGridDto> ConsultarGeneral()
        {
            return seccionRepository.ConsultarGeneral();
        }

        public IEnumerable<SeccionDto> ConsultarTodosParaSelector()
        {
            return seccionRepository.consultarTodosParaSelector();
        }

        public void Agregar(Seccion seccion)
        {
            seccion.Clave = GenerarClave();
            seccionValidatorService.ValidarAgregar(seccion);
            seccionRepository.Agregar(seccion);
        }

        public void Editar(Seccion seccion)
        {
            seccionValidatorService.ValidarEditar(seccion);
            seccionRepository.Editar(seccion);
        }
        public void Eliminar(int idSeccion)
        {
            var seccion = seccionRepository.Consultar(idSeccion);
            seccionValidatorService.ValidarEliminar(idSeccion);

            if (seccion != null)
            {
                using var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

                var camposHijos = seccionCampoRepository.ConsultarPorSeccion(idSeccion);

                foreach (var campo in camposHijos)
                {
                    var seccionCampo = seccionCampoRepository.Consultar(campo.IdSeccionCampo);

                    if (seccionCampo != null)
                    {
                        seccionCampoRepository.Eliminar(seccionCampo);
                    }
                }

                seccionRepository.Eliminar(seccion);

                scope.Complete();
            }
        }

        private string GenerarClave()
        {
            string ultimaClave = seccionRepository.ConsultarUltimaClave();
            ultimaClave ??= "0";
            ultimaClave = ultimaClave.Split(GeneralConstant.SeparadorFolio).Last();

            return "SE" + GeneralConstant.SeparadorFolio + (int.Parse(ultimaClave) + 1).ToString("D3");
        }
    }
}
