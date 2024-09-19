using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using System.Collections.Generic;
using TrackrAPI.Repositorys.Catalogo;
using System.Transactions;

namespace TrackrAPI.Services.Seguridad
{
    public class EspecialidadUsuarioService
    {
        private readonly IEspecialidadUsuarioRepository _especialidadUsuarioRepository;

        public EspecialidadUsuarioService(IEspecialidadUsuarioRepository especialidadUsuarioRepository)
        {
            _especialidadUsuarioRepository = especialidadUsuarioRepository;
        }

        public async Task Guardar(EspecialidadUsuario especialidadUsuario)
        {
            var especialidadUsuarioExistente = await _especialidadUsuarioRepository.ConsultarPorUsuario((int) especialidadUsuario.IdUsuario, (int) especialidadUsuario.IdEspecialidad);

            if(especialidadUsuarioExistente != null)
            {
                await _especialidadUsuarioRepository.EditarAsync(especialidadUsuario);
            }else{
                await _especialidadUsuarioRepository.AgregarAsync(especialidadUsuario);
            }
        }

        public async Task Guardar(List<EspecialidadUsuario> especialidadUsuarioList)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                var idUsuario = (int)especialidadUsuarioList[0].IdUsuario;

                var especialidadesUsuarioBd = await _especialidadUsuarioRepository.ConsultarPorUsuario(idUsuario);

                foreach (var especialidadUsuariobBd in especialidadesUsuarioBd)
                {

                    if (!especialidadUsuarioList.Exists(l => l.IdUsuario == especialidadUsuariobBd.IdUsuario && l.IdEspecialidad == especialidadUsuariobBd.IdEspecialidad))
                    {
                        _especialidadUsuarioRepository.Eliminar(especialidadUsuariobBd);
                    }
                    else
                    {
                        especialidadUsuarioList.RemoveAll(l => l.IdUsuario == especialidadUsuariobBd.IdUsuario && l.IdEspecialidad == especialidadUsuariobBd.IdEspecialidad);
                    }
                }

                if (especialidadUsuarioList.All( eu => eu.IdEspecialidad > 0))
                {
                    foreach (var usuarioEspecialidad in especialidadUsuarioList)
                    {
                        //AgregarConceptoCuentaDefault(usuarioRol);
                        _especialidadUsuarioRepository.Agregar(usuarioEspecialidad);
                    }
                }

                scope.Complete();
            }
        }
    }
}
