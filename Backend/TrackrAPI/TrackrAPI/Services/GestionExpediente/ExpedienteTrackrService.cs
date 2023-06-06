using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Repositorys.Inventario;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.GestionExpediente
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
        public int AgregarWrapper(ExpedienteWrapper expedienteWrapper)
        {
            ExpedienteTrackr expedienteTrackr = expedienteWrapper.expediente;
            expedienteTrackr.IdUsuario = expedienteWrapper.paciente.IdUsuario;

            expedienteTrackr = expedienteTrackrRepository.Agregar(expedienteTrackr);
            expedienteTrackr.Numero = expedienteTrackr.IdExpediente.ToString().PadLeft(6, '0');
            expedienteTrackrRepository.Editar(expedienteTrackr);

            AgregarPadecimientos(expedienteWrapper.padecimientos, expedienteTrackr.IdExpediente);
            return expedienteTrackr.IdExpediente;
        }

        /// <summary>
        /// Edita un expedienteWrapper. Primero asigna cada una de las propiedades del expedienteWrapper a sus respectivos modelos.
        /// Para después utilizar el Repositorio de cada modelo y editarlos.
        /// </summary>
        /// <param name="expedienteWrapper">El expediente, el domicilio y el usuario a editar</param>
        /// <returns>El id del expediente editado</returns>
        public int EditarWrapper(ExpedienteWrapper expedienteWrapper)
        {
            ExpedienteTrackr expedienteTrackr = expedienteWrapper.expediente;
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var idUsuario = expedienteWrapper.paciente.IdUsuario;

                // Agregar ExpedienteTrackR
                expedienteTrackr.IdUsuario = idUsuario;
                expedienteTrackr.FechaAlta = DateTime.Now;
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

                AgregarPadecimientos(expedienteWrapper.padecimientos, expedienteTrackr.IdExpediente);
                scope.Complete();
            }
            return expedienteTrackr.IdExpediente;
        }

        public void Eliminar(int IdExpediente)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                expedientePadecimientoRepository.EliminarPorExpediente(IdExpediente);
                var expedienteTrakcr = expedienteTrackrRepository.Consultar(IdExpediente);
                expedienteTrackrRepository.Eliminar(expedienteTrakcr);
                scope.Complete();
            }
        }

        public void AgregarPadecimientos(IEnumerable<ExpedientePadecimientoDTO> padecimientos, int idExpediente)
        {
            expedientePadecimientoRepository.EliminarPorExpediente(idExpediente);
            foreach (var padecimientoDTO in padecimientos)
            {
                var padecimiento = new ExpedientePadecimiento();

                padecimiento.IdPadecimiento = padecimientoDTO.IdPadecimiento;
                padecimiento.FechaDiagnostico = padecimientoDTO.FechaDiagnostico;
                padecimiento.IdExpediente = idExpediente;

                if (padecimiento.IdPadecimiento == 0)
                {
                    continue;
                }

                expedientePadecimientoRepository.Agregar(padecimiento);

            }
        }

        public IEnumerable<UsuarioExpedienteGridDTO> ConsultarParaGrid()
        {
            return expedienteTrackrRepository.ConsultarParaGrid();
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
                // domicilio = domicilioRepository.ConsultarPorUsuario(idUsuario).FirstOrDefault(),
                paciente = usuarioRepository.ConsultarDto(idUsuario),
                padecimientos = expedientePadecimientoRepository.ConsultarPorUsuario(idUsuario)
            };
            return expedienteWrapper;
        }

    }
}
