using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Catalogo
{
    public class TituloAcademicoValidatorService
    {
        private ITituloAcademicoRepository tituloAcademicoRepository;

        public TituloAcademicoValidatorService(ITituloAcademicoRepository tituloAcademicoRepository)
        {
            this.tituloAcademicoRepository = tituloAcademicoRepository;
        }

        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeClaveRequerida = "La clave es requerida";
        private readonly string MensajeExistencia = "El título académico no existe";
        private readonly string MensajeDuplicado = "El título académico  ya existe";

        private static readonly int LongitudNombre = 50;
        private static readonly int LongitudClave = 20;

        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son {LongitudNombre } caracteres";
        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave } caracteres";


        private readonly string MensajeUsuarioDependencia = "El titulo académico esta asociado al menos a un usuario y no se puede eliminar";

        public void ValidarAgregar(TituloAcademico tituloAcademico)
        {
            ValidarRequerido(tituloAcademico);
            ValidarRango(tituloAcademico);
            ValidarDuplicado(tituloAcademico);
        }

        public void ValidarEditar(TituloAcademico tituloAcademico)
        {
            ValidarRequerido(tituloAcademico);
            ValidarRango(tituloAcademico);
            ValidarExistencia(tituloAcademico.IdTituloAcademico);
            ValidarDuplicado(tituloAcademico);
        }

        public void ValidarEliminar(int idTituloAcademico)
        {
            var tituloAcademico = tituloAcademicoRepository.Consultar(idTituloAcademico);

            ValidarExistencia(idTituloAcademico);
            ValidarDependencia(idTituloAcademico);
        }

        public void ValidarRequerido(TituloAcademico tituloAcademico)
        {
            Validator.ValidarRequerido(tituloAcademico.Nombre, MensajeNombreRequerido);
        }

        public void ValidarRango(TituloAcademico tituloAcademico)
        {
            Validator.ValidarLongitudRangoString(tituloAcademico.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        public void ValidarExistencia(TituloAcademicoDto tituloAcademico)
        {
            if (tituloAcademico == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idTituloAcademico)
        {
            var tituloAcademico = tituloAcademicoRepository.Consultar(idTituloAcademico);

            if (tituloAcademico == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(TituloAcademico tituloAcademico)
        {
            var tituloAcademicoDuplicado = tituloAcademicoRepository.Consultar(tituloAcademico.Clave);

            if (tituloAcademicoDuplicado != null && tituloAcademico.IdTituloAcademico != tituloAcademicoDuplicado.IdTituloAcademico)
            {
                throw new CdisException(MensajeDuplicado);
            }

            var tituloAcademicoDuplicado2 = tituloAcademicoRepository.ConsultarPorNombre(tituloAcademico.Nombre);

            if (tituloAcademicoDuplicado2 != null && tituloAcademico.IdTituloAcademico != tituloAcademicoDuplicado2.IdTituloAcademico)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }

        public void ValidarDependencia(int idTituloAcademico)
        {
            var tituloAcademico = tituloAcademicoRepository.ConsultarDependencias(idTituloAcademico);

            if (tituloAcademico.Usuario.Any())
            {
                throw new CdisException(MensajeUsuarioDependencia);
            }
        }
    }
}