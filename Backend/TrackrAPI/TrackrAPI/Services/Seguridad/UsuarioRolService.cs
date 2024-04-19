using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Catalogo;
using System.Collections.Generic;
using System.Transactions;

namespace TrackrAPI.Services.Seguridad
{
    public class UsuarioRolService
    {
        private IUsuarioRolRepository usuarioRolRepository;
        private UsuarioRolValidatorService usuarioRolValidatorService;
        private IRolRepository rolRepository;
        private IUsuarioRepository usuarioRepository;

        public UsuarioRolService(
            IUsuarioRolRepository usuarioRolRepository,
            UsuarioRolValidatorService usuarioRolValidatorService,
            IRolRepository rolRepository,
            IUsuarioRepository usuarioRepository
        )
        {
            this.usuarioRolRepository = usuarioRolRepository;
            this.usuarioRolValidatorService = usuarioRolValidatorService;
            this.rolRepository = rolRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public IEnumerable<UsuarioRolDto> ConsultarPorUsuario(int idUsuario)
        {
            return usuarioRolRepository.ConsultarPorUsuario(idUsuario);
        }

        public IEnumerable<UsuarioRolGridDto> ConsultarPorUsuarioParaGrid(int idUsuario)
        {
            return usuarioRolRepository.ConsultarPorUsuarioParaGrid(idUsuario);
        }

        public void Guardar(List<UsuarioRol> usuarioRolList)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var idUsuario = usuarioRolList[0].IdUsuario;

                var usuarioRolActual = usuarioRolRepository.ConsultarPorUsuario(idUsuario);

                foreach (var usuarioRol in usuarioRolActual)
                {
                    UsuarioRol ur = usuarioRolRepository.Consultar(usuarioRol.IdUsuarioRol);

                    if (!usuarioRolList.Exists(l => l.IdUsuario == ur.IdUsuario && l.IdRol == ur.IdRol))
                    {
                        usuarioRolRepository.Eliminar(ur);
                    }
                    else
                    {
                        usuarioRolList.RemoveAll(l => l.IdUsuario == ur.IdUsuario && l.IdRol == ur.IdRol);
                    }
                }


                foreach (var usuarioRol in usuarioRolList)
                {
                    //AgregarConceptoCuentaDefault(usuarioRol);

                    usuarioRolRepository.Agregar(usuarioRol);
                }

                scope.Complete();
            }
        }

        public void Agregar(UsuarioRol usuarioRol)
        {
            //AgregarConceptoCuentaDefault(usuarioRol);

            usuarioRolValidatorService.ValidarAgregar(usuarioRol);
            usuarioRolRepository.Agregar(usuarioRol);
        }

        public void Editar(UsuarioRol usuarioRol)
        {
            usuarioRolValidatorService.ValidarEditar(usuarioRol);
            usuarioRolRepository.Editar(usuarioRol);
        }

        public void Eliminar(int idUsuarioRol)
        {
            var usuarioRol = usuarioRolRepository.Consultar(idUsuarioRol);
            usuarioRolRepository.Eliminar(usuarioRol);
        }

        //private void AgregarConceptoCuentaDefault(UsuarioRol usuarioRol)
        //{
        //    // Sólo se agrega el concepto y cuenta default si no están definidas en el objeto
        //    if (usuarioRol.IdConcepto != null || usuarioRol.IdCuentaContable != null)
        //        return;

        //    Usuario usuario = usuarioRepository.Consultar(usuarioRol.IdUsuario);
        //    Rol rol = rolRepository.Consultar(usuarioRol.IdRol);

        //    // La clave del concepto default depende del rol del usuario
        //    (string claveConceptoDefault, string nombreConceptoDefault) = rol.Clave switch
        //    {
        //        GeneralConstant.ClaveRolProveedor => (GeneralConstant.ClaveConceptoCuentaPorPagar, "Cuenta por pagar"),
        //        GeneralConstant.ClaveRolCliente => (GeneralConstant.ClaveConceptoCuentaPorCobrar, "Cuenta por cobrar"),
        //        _ => ("", ""),
        //    };

        //    // Si el rol no coincide con alguno de los roles con conceptos default definidos, no se
        //    // modifica el concepto ni la cuenta contable.
        //    if (claveConceptoDefault == "")
        //        return;

        //    Concepto conceptoDefault = conceptoRepository.ConsultarPorClave(claveConceptoDefault, (int)usuario.IdCompania);

        //    if (conceptoDefault == null || conceptoDefault.IdCuentaContable == null)
        //    {
        //        throw new CdisException($"Es necesario configurar concepto {nombreConceptoDefault} - {claveConceptoDefault}");
        //    }

        //    usuarioRol.IdConcepto = conceptoDefault.IdConcepto;
        //    usuarioRol.IdCuentaContable = conceptoDefault.IdCuentaContable;
        //}
    }
}
