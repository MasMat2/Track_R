using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Seguridad;
using System.Transactions;

namespace TrackrAPI.Services.Seguridad
{
    public class PerfilService
    {
        private IPerfilRepository perfilRepository;
        private IAccesoPerfilRepository accesoPerfilRepository;
        private ICompaniaRepository companiaRepository;
        private PerfilValidatorService perfilValidatorService;
        private AccesoService accesoService;
        private IJerarquiaAccesoEstructuraRepository jerarquiaAccesoEstructuraRepository;

        public PerfilService(IPerfilRepository perfilRepository,
            IAccesoPerfilRepository accesoPerfilRepository,
            ICompaniaRepository companiaRepository,
            PerfilValidatorService perfilValidatorService,
            AccesoService accesoService,
            IJerarquiaAccesoEstructuraRepository jerarquiaAccesoEstructuraRepository)
        {

            this.perfilRepository = perfilRepository;
            this.accesoPerfilRepository = accesoPerfilRepository;
            this.companiaRepository = companiaRepository;
            this.perfilValidatorService = perfilValidatorService;
            this.accesoService = accesoService;
            this.jerarquiaAccesoEstructuraRepository = jerarquiaAccesoEstructuraRepository;
        }

        public IEnumerable<PerfilDto> ConsultarGeneral(int idCompania)
        {
            return perfilRepository.ConsultarGeneral(idCompania);
        }

        public PerfilDto Consultar(int idPerfil)
        {
            var perfil = perfilRepository.ConsultarDto(idPerfil);

            if (perfil == null)
            {
                perfil = new PerfilDto();
            }

            perfil.IdsAcceso = accesoService.ConsultarPorPerfil(idPerfil).Select(a => a.IdAcceso).ToArray();
            return perfil;
        }

        public IEnumerable<PerfilDto> ConsultarTodosParaSelector(int idCompania)
        {
            return perfilRepository.ConsultarTodosParaSelector(idCompania);
        }

        //Consulta los perfiles de la compania y la compania base
        public IEnumerable<Perfil> ConsultarPorCompaniaBase(int idCompania)
        {
            return perfilRepository.ConsultarPorCompaniaBase(idCompania);
        }

        public Perfil Agregar(PerfilDto perfilDto)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var perfil = new Perfil();
                perfil.Nombre = perfilDto.Nombre;
                perfil.IdCompania = perfilDto.IdCompania;
                perfil.Clave = GenerarClave(perfilDto.IdCompania);
                perfil.IdTipoCompania = perfilDto.IdTipoCompania;
                perfil.IdJerarquiaAcceso = perfilDto.IdJerarquiaAcceso;

                perfilValidatorService.ValidarAgregar(perfil);
                perfilRepository.Agregar(perfil);

                /*if (perfilDto.IdsAcceso.Length == 0)
                {
                    throw new CdisException("El perfil debe de tener por lo menos un acceso seleccionado");
                }*/

                foreach (var IdAcceso in perfilDto.IdsAcceso)
                {
                    var accesoPerfil = new AccesoPerfil();
                    accesoPerfil.IdPerfil = perfil.IdPerfil;
                    accesoPerfil.IdAcceso = IdAcceso;
                    accesoPerfilRepository.Agregar(accesoPerfil);
                }

                scope.Complete();

                return perfil;
            }
        }

        public void Editar(PerfilDto perfilDto)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

            var perfil = new Perfil
            {
                IdPerfil = perfilDto.IdPerfil,
                Nombre = perfilDto.Nombre,
                IdCompania = perfilDto.IdCompania,
                IdTipoCompania = perfilDto.IdTipoCompania,
                Clave = perfilDto.Clave,
                IdJerarquiaAcceso = perfilDto.IdJerarquiaAcceso
            };

            perfilValidatorService.ValidarEditar(perfil);
            perfilRepository.Editar(perfil);

            var accesosJerarquia = jerarquiaAccesoEstructuraRepository
                .ConsultarPorJerarquiaArbol((int)perfilDto.IdJerarquiaAcceso)
                .Select(j => j.IdAcceso)
                .ToList();
            var idsAccesoFormulario = perfilDto.IdsAcceso
                .Where(idAcceso => accesosJerarquia.Contains(idAcceso))
                .ToList();

            // Se consultan los accesos que actualmente están guardados
            IEnumerable<AccesoPerfil> accesosActuales = accesoPerfilRepository
                .ConsultarPorPerfil(perfil.IdPerfil)
                .ToList();

            var idsAccesoActuales = accesosActuales
                .Select(accesoPerfil => accesoPerfil.IdAcceso)
                .ToList();

            // Se eliminan los accesos de la base de datos que no se encuentran en el formulario
            var accesosEliminados = idsAccesoActuales
                .Where(idAcceso => !idsAccesoFormulario.Contains(idAcceso))
                .ToList();

            foreach (int idAcceso in accesosEliminados)
            {
                AccesoPerfil accesoPerfil = accesosActuales.FirstOrDefault(accesoPerfil => accesoPerfil.IdAcceso == idAcceso);
                accesoPerfilRepository.Eliminar(accesoPerfil);
            }

            // Se agregan los accesos del formulario que no se encuentran en la base de datos
            var accesosNuevos = idsAccesoFormulario
                .Where(idAcceso => !idsAccesoActuales.Contains(idAcceso))
                .ToList();

            foreach (int idAcceso in accesosNuevos)
            {
                var accesoPerfil = new AccesoPerfil
                {
                    IdPerfil = perfil.IdPerfil,
                    IdAcceso = idAcceso
                };

                accesoPerfilRepository.Agregar(accesoPerfil);
            }

            scope.Complete();
        }

        public void Eliminar(int idPerfil)
        {
            perfilValidatorService.ValidarEliminar(idPerfil);

            IEnumerable<AccesoPerfil> accesoPerfilList = accesoPerfilRepository.ConsultarPorPerfil(idPerfil);

            foreach (AccesoPerfil accesoPerfil in accesoPerfilList)
            {
                accesoPerfilRepository.Eliminar(accesoPerfil);
            }

            Perfil perfil = perfilRepository.Consultar(idPerfil);
            perfilRepository.Eliminar(perfil);
        }

        private string GenerarClave(int idCompania)
        {
            var esCompaniaBase = companiaRepository.Consultar(idCompania).Clave.Contains(GeneralConstant.ClaveCompaniaBase);
            var ultimoPerfil = perfilRepository.ConsultarUltimoAgregado(esCompaniaBase, idCompania);
            string clavePerfil;

            if (ultimoPerfil == null && esCompaniaBase)
            {
                return "BASE001";
            }
            else if(ultimoPerfil == null && !esCompaniaBase)
            {
                return "001";
            }

            if (esCompaniaBase)
            {
                string clave = ultimoPerfil.Clave;
                int ultimoconsecutivo = Int32.Parse(clave.Substring(clave.Length - 3));
                int consecutivo = ultimoconsecutivo + 1;
                clavePerfil = "BASE" + consecutivo.ToString("D3");

            }
            else
            {
                int ultimoConsecutivo = Int32.Parse(ultimoPerfil.Clave);
                int consecutivo = ultimoConsecutivo + 1;
                clavePerfil = consecutivo.ToString("D3");
            }

            return clavePerfil;
        }

    }
}
