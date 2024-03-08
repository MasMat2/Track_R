using CanalDistAPI.Dtos.Seguridad;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace TrackrAPI.Services.Seguridad
{
    public class JerarquiaAccesoEstructuraService
    {
        private readonly IJerarquiaAccesoEstructuraRepository jerarquiaAccesoEstructuraRepository;
        private readonly JerarquiaAccesoEstructuraValidatorService jerarquiaAccesoEstructuraValidatorService;

        public JerarquiaAccesoEstructuraService
        (
            IJerarquiaAccesoEstructuraRepository jerarquiaAccesoEstructuraRepository,
            JerarquiaAccesoEstructuraValidatorService jerarquiaAccesoEstructuraValidatorService
        )
        {
            this.jerarquiaAccesoEstructuraRepository = jerarquiaAccesoEstructuraRepository;
            this.jerarquiaAccesoEstructuraValidatorService = jerarquiaAccesoEstructuraValidatorService;
        }

        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPorJerarquiaArbol(int idJerarquia)
        {
            return jerarquiaAccesoEstructuraRepository.ConsultarPorJerarquiaArbol(idJerarquia);
        }

        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarPorJerarquiaParaSelector(int idJerarquia)
        {
            return jerarquiaAccesoEstructuraRepository.ConsultarPorJerarquiaParaSelector(idJerarquia);
        }
        public IEnumerable<JerarquiaAccesoEstructura> ConsultarPorJerarquiaAcceso(int idJerarquiaAcceso)
        {
            return jerarquiaAccesoEstructuraRepository.ConsultarPorJerarquiaAcceso(idJerarquiaAcceso);
        }

        public IEnumerable<JerarquiaEstructuraArbolDto> ConsultarArbol(int idJerarquia)
        {
            var jerarquiaEstructuras = jerarquiaAccesoEstructuraRepository.ConsultarPorJerarquiaArbol(idJerarquia);
            var jerarquiasPadre = jerarquiaEstructuras.Where(je => je.IdJerarquiaEstructuraPadre == null).ToList();
            ConsultarHijos(jerarquiasPadre, jerarquiaEstructuras);
            return jerarquiasPadre;
        }

        public List<JerarquiaEstructuraArbolDto> ConsultarHijos(
            List<JerarquiaEstructuraArbolDto> jerarquiasPadre,
            IEnumerable<JerarquiaEstructuraArbolDto> jerarquiasEstructuras)
        {
            foreach (JerarquiaEstructuraArbolDto padre in jerarquiasPadre)
            {
                padre.Hijos = jerarquiasEstructuras.Where(j => j.IdJerarquiaEstructuraPadre == padre.IdJerarquiaEstructura).ToList();

                if (padre.Hijos.Any())
                {
                    ConsultarHijos(padre.Hijos, jerarquiasEstructuras);
                }
            }

            return jerarquiasPadre;
        }

        public void Agregar(List<JerarquiaAccesoEstructuraDto> jerarquiasDto)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                foreach (JerarquiaAccesoEstructuraDto jerarquiaDto in jerarquiasDto)
                {
                    JerarquiaAccesoEstructura jerarquiaEstructura = new()
                    {
                        IdJerarquiaAcceso = jerarquiaDto.IdJerarquiaAcceso,
                        IdAcceso = jerarquiaDto.IdAcceso,
                        IdJerarquiaAccesoEstructuraPadre = jerarquiaDto.IdJerarquiaAccesoEstructuraPadre
                    };

                    if (jerarquiaDto.IdAccesoPadre > 0)
                    {
                        var jerarquiaPadreSeleccionada = jerarquiasDto.Where(j => j.IdAcceso == jerarquiaDto.IdAccesoPadre).FirstOrDefault();
                        jerarquiaEstructura.IdJerarquiaAccesoEstructuraPadre =
                            jerarquiaPadreSeleccionada != null ?
                            jerarquiaPadreSeleccionada.IdJerarquiaAccesoEstructura :
                            jerarquiaDto.IdJerarquiaAccesoEstructuraPadre;

                        jerarquiaAccesoEstructuraValidatorService.ValidarAgregar(jerarquiaEstructura);
                        jerarquiaAccesoEstructuraRepository.Agregar(jerarquiaEstructura);
                        jerarquiaDto.IdJerarquiaAccesoEstructura = jerarquiaEstructura.IdJerarquiaAccesoEstructura;
                    }
                    else
                    {
                        jerarquiaAccesoEstructuraValidatorService.ValidarAgregar(jerarquiaEstructura);
                        jerarquiaAccesoEstructuraRepository.Agregar(jerarquiaEstructura);
                        jerarquiaDto.IdJerarquiaAccesoEstructura = jerarquiaEstructura.IdJerarquiaAccesoEstructura;
                    }
                }
                scope.Complete();
            }
        }

        public void Eliminar(int idJerarquiaEstructura)
        {
            JerarquiaAccesoEstructura jerarquiaEstructura = jerarquiaAccesoEstructuraRepository.Consultar(idJerarquiaEstructura);

            if (jerarquiaEstructura != null)
            {
                var hijos = jerarquiaAccesoEstructuraRepository.ConsultarHijosDeEstructura(jerarquiaEstructura.IdJerarquiaAccesoEstructura);
                EliminarHijos(hijos);

                jerarquiaAccesoEstructuraValidatorService.ValidarEliminar(jerarquiaEstructura.IdJerarquiaAccesoEstructura);
                jerarquiaAccesoEstructuraRepository.Eliminar(jerarquiaEstructura);
            }
        }

        private void EliminarHijos(List<JerarquiaAccesoEstructura> jerarquiaAccesoEstructuraList)
        {
            foreach (JerarquiaAccesoEstructura estructura in jerarquiaAccesoEstructuraList)
            {
                var hijos = jerarquiaAccesoEstructuraRepository.ConsultarHijosDeEstructura(estructura.IdJerarquiaAccesoEstructura);

                if (hijos.Any())
                {
                    EliminarHijos(hijos);
                }

                jerarquiaAccesoEstructuraValidatorService.ValidarEliminar(estructura.IdJerarquiaAccesoEstructura);
                jerarquiaAccesoEstructuraRepository.Eliminar(estructura);
            };
        }
    }
}
