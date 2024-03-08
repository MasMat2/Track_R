using TrackrAPI.Dtos;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Linq;


namespace TrackrAPI.Services.Catalogo
{
    public class SubtipoCuentaContableValidatorService
    {
        private ISubtipoCuentaContableRepository subtipoCuentaContableRepository;

        public SubtipoCuentaContableValidatorService(ISubtipoCuentaContableRepository subtipoCuentaContableRepository)
        {
            this.subtipoCuentaContableRepository = subtipoCuentaContableRepository;
        }

        private readonly string MensajeExistencia = "El subtipo cuenta contable no existe";


        public void ValidarExistencia(SubtipoCuentaContableDto subtipoCuentaContable)
        {
            if (subtipoCuentaContable == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        //public void ValidarExistencia(int idSubtipoCuentaContable)
        //{
        //    var estado = subtipoCuentaContableRepository.Consultar(idSubtipoCuentaContable);

        //    if (estado == null)
        //    {
        //        throw new CdisException(MensajeExistencia);
        //    }
        //}
    }
}
