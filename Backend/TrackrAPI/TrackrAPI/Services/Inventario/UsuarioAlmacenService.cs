using TrackrAPI.Dtos.Inventario;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Inventario;
using System.Collections.Generic;
using System.Transactions;

namespace TrackrAPI.Services.Inventario
{
    public class UsuarioAlmacenService
    {
        private IUsuarioAlmacenRepository usuarioAlmacenRepository;

        public UsuarioAlmacenService(IUsuarioAlmacenRepository usuarioAlmacenRepository)
        {
            this.usuarioAlmacenRepository = usuarioAlmacenRepository;
        }

        public IEnumerable<UsuarioAlmacenDto> ConsultarPorAlmacen(int idUsuario)
        {
            return usuarioAlmacenRepository.ConsultarPorAlmacenDto(idUsuario);
        }

        public void Agregar(UsuarioAlmacen usuarioAlmacen)
        {
            usuarioAlmacenRepository.Agregar(usuarioAlmacen);
        }

        public void Guardar(List<UsuarioAlmacen> usuarioAlmacenList)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var idAlmacen = usuarioAlmacenList[0].IdAlmacen;

                var almacenUsuarioActual = usuarioAlmacenRepository.ConsultarPorAlmacenDto(idAlmacen);

                foreach (var almacenUsuario in almacenUsuarioActual)
                {
                    UsuarioAlmacen ua = usuarioAlmacenRepository.Consultar(almacenUsuario.IdUsuarioAlmacen);

                    if (!usuarioAlmacenList.Exists(l => l.IdUsuario == ua.IdAlmacen && l.IdUsuario == ua.IdUsuario))
                    {
                        usuarioAlmacenRepository.Eliminar(ua);
                    }
                    else
                    {
                        usuarioAlmacenList.RemoveAll(l => l.IdAlmacen == ua.IdAlmacen && l.IdUsuario == ua.IdUsuario);
                    }
                }

                foreach (var almacenUsuario in usuarioAlmacenList)
                {
                    usuarioAlmacenRepository.Agregar(almacenUsuario);
                }

                scope.Complete();
            }
        }
    }
}
