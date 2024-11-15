using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Services.Catalogo
{
    public class ColoniaValidatorService
    {
        private IColoniaRepository coloniaRepository;

        public ColoniaValidatorService(IColoniaRepository coloniaRepository)
        {
            this.coloniaRepository = coloniaRepository;
        }

        private readonly string MensajeCodigoPostalRequerido = "El codigo postal de la colonia es requerido";
        private readonly string MensajeClaveRequerida = "La clave de la colonia es requerida";
        private readonly string MensajeNombreRequerido = "El nombre de la colonia es requerido";

        private readonly string MensajeExistencia = "La colonia no existe";

        private readonly string MensajeDuplicadoCodigoPostal = "El código postal ya se encuentra registrado.";


        public void ValidarAgregar(Colonia colonia)
        {
            ValidarRequerido(colonia);
            ValidarDuplicado(colonia);
        }
        public void ValidarEditar(Colonia colonia)
        {
            ValidarRequerido(colonia);
            ValidarExistencia(colonia.IdColonia);
            ValidarDuplicado(colonia);
        }

        public void ValidarEliminar(int idColonia)
        {
            ValidarExistencia(idColonia);
        }

        public void ValidarRequerido(Colonia colonia)
        {
            Validator.ValidarRequerido(colonia.Clave, MensajeClaveRequerida);
        }

        public void ValidarExistencia(int idColonia)
        {
            var colonia = coloniaRepository.Consultar(idColonia);

            if (colonia == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(Colonia colonia)
        {
      /*       Colonia duplicadoCodigoPostal = coloniaRepository.ConsultarPorCodigoPostal(colonia.Nombre);

            if (duplicadoCodigoPostal != null && colonia.IdColonia != duplicadoCodigoPostal.IdColonia)
            {
                throw new CdisException(MensajeDuplicadoCodigoPostal);
            } */
        }
    }
}
