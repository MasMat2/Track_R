using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Inventario;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class ExpedienteTrackrService
    {
        private IExpedienteTrackrRepository expedienteTrackrRepository;
        private IDomicilioRepository domicilioRepository;
        private IUsuarioRepository usuarioRepository;
        private IExpedientePadecimientoRepository expedientePadecimientoRepository;

        public ExpedienteTrackrService(
            IExpedienteTrackrRepository expedienteTrackrRepository,
            IDomicilioRepository domicilioRepository,
            IUsuarioRepository usuarioRepository,
            IExpedientePadecimientoRepository expedientePadecimientoRepository
            ) 
        {
            this.expedienteTrackrRepository = expedienteTrackrRepository;
            this.domicilioRepository = domicilioRepository;
            this.usuarioRepository = usuarioRepository;
            this.expedientePadecimientoRepository = expedientePadecimientoRepository;
        }
        /// <summary>
        /// Consulta el expediente de un usuario
        /// </summary>
        /// <param name="idUsuario">Id del Usuario a Consultar</param>
        /// <returns>ExpedienteTrackR consultado</returns>
        public ExpedienteTrackr ConsultarPorUsuario(int idUsuario)
        {
            return expedienteTrackrRepository.ConsultarPorUsuario(idUsuario);
        }

        /// <summary>
        /// Agrega un expedienteWrapper.Primero asigna cada una de las propiedades del expedienteWrapper a sus respectivos modelos.
        /// Para después utilizar el Repositorio de cada modelo y agregarlos.
        /// </summary>
        /// <param name="expedienteWrapper">El expediente, el domicilio y el usuario a agregar</param>
        /// <returns>El id del expediente agregado</returns>
        public void AgregarWrapper(ExpedienteWrapper expedienteWrapper)
        {
            ExpedienteTrackr expedienteTrackr = expedienteWrapper.expediente;
            expedienteTrackrRepository.Agregar(expedienteTrackr);

            Domicilio domicilio =  expedienteWrapper.domicilio;
            domicilio.IdUsuario = expedienteWrapper.expediente.IdUsuario;
            domicilioRepository.Agregar(expedienteWrapper.domicilio);

            Usuario paciente = expedienteWrapper.paciente;
            usuarioRepository.Agregar(paciente);

            // TODO: Agregar Padecimientos
        }

        /// <summary>
        /// Edita un expedienteWrapper. Primero asigna cada una de las propiedades del expedienteWrapper a sus respectivos modelos.
        /// Para después utilizar el Repositorio de cada modelo y editarlos.
        /// </summary>
        /// <param name="expedienteWrapper">El expediente, el domicilio y el usuario a editar</param>
        /// <returns>El id del expediente editado</returns>
        public void EditarWrapper(ExpedienteWrapper expedienteWrapper)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var idUsuario = expedienteWrapper.paciente.IdUsuario;
                // Agregar Usuario
                Usuario paciente = expedienteWrapper.paciente;
                if (paciente.IdUsuario == 0)
                {
                    usuarioRepository.Agregar(paciente);
                }
                else
                {
                    usuarioRepository.Editar(paciente);
                }
                // Agregar ExpedienteTrackR
                ExpedienteTrackr expedienteTrackr = expedienteWrapper.expediente;
                expedienteTrackr.IdUsuario = idUsuario;
                if (expedienteTrackr.IdExpediente == 0)
                {
                    ExpedienteTrackr expedienteAgregado = expedienteTrackrRepository.Agregar(expedienteTrackr);
                    expedienteTrackr.Numero = expedienteAgregado.IdExpediente.ToString().PadLeft(6, '0');
                    expedienteTrackrRepository.Editar(expedienteTrackr);
                }
                else
                {
                    expedienteTrackr.Numero = expedienteTrackr.IdExpediente.ToString().PadLeft(6, '0');
                    expedienteTrackrRepository.Editar(expedienteTrackr);
                }
                // Agregar Domicilio
                Domicilio domicilio = expedienteWrapper.domicilio;
                domicilio.IdUsuario = idUsuario;
                if (domicilio.IdDomicilio == 0)
                {
                    domicilioRepository.Agregar(domicilio);
                }
                else
                {
                    domicilioRepository.Editar(domicilio);
                }
                // Agregar Padecimientos
                foreach (var padecimientoDTO in expedienteWrapper.padecimientos)
                {
                    var padecimiento  = new ExpedientePadecimiento();
                    padecimiento.IdPadecimiento = padecimientoDTO.IdPadecimiento;
                    padecimiento.FechaDiagnostico = padecimientoDTO.FechaDiagnostico;
                    padecimiento.IdPadecimiento = padecimientoDTO.IdPadecimiento;
                    padecimiento.IdExpediente = expedienteTrackr.IdExpediente;


                    if (padecimiento.IdPadecimiento == 0)
                    {
                        expedientePadecimientoRepository.Agregar(padecimiento);
                    }
                    else
                    {
                        expedientePadecimientoRepository.Editar(padecimiento);
                    }
                }
                // TODO: Agregar Padecimientos
                scope.Complete();
            }
        }

        /// <summary>
        /// Consulta un expedienteWrapper por usuario. Construye el ExpedienteWrapper a base de 4 modelos:
        /// Expediente, Domicilio, Usuario y Padecimientos.
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>El expedienteWrapper del usuario</returns>
        public ExpedienteWrapper ConsultarWrapperPorUsuario(int idUsuario)
        {
            ExpedienteWrapper expedienteWrapper = new ExpedienteWrapper
            {
                expediente = ConsultarPorUsuario(idUsuario),
                domicilio = domicilioRepository.ConsultarPorUsuario(idUsuario).FirstOrDefault(),
                paciente = usuarioRepository.Consultar(idUsuario),
                padecimientos = expedientePadecimientoRepository.ConsultarPorUsuario(idUsuario)
            };
            return expedienteWrapper;
        }

    }
}
