using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class ConfiguracionRepository : Repository<Configuracion>, IConfiguracionRepository
    {
        public ConfiguracionRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<Configuracion> ConsultarPorCompaniaContable(int idCompania)
        {
            HashSet<string> claves = new()
            {
                GeneralConstant.ConfiguracionMesContableActual,
                GeneralConstant.ConfiguracionAnioContableActual,
                GeneralConstant.ConfiguracionNumeroPolizaActual,
                GeneralConstant.ConfiguracionMesContableAjuste,
                GeneralConstant.ConfiguracionAnioContableAjuste,
                GeneralConstant.ConfiguracionCuentaUtilidadFinanciera,
                GeneralConstant.ConfiguracionContraCuentaUtilidadFinanciera
            };

            return context.Configuracion
                .Where(c => c.IdCompania == idCompania && claves.Contains(c.Clave))
                .Select(c => new Configuracion()
                {
                    IdConfiguracion = c.IdConfiguracion,
                    Clave = c.Clave,
                    Descripcion = c.Descripcion,
                    Valor = c.Valor,
                    IdCompania = c.IdCompania,
                });
        }

        public IEnumerable<Configuracion> ConsultarPorCompaniaMedica(int idCompania)
        {
            HashSet<string> claves = new()
            {
                GeneralConstant.ConfiguracionRequierePagoCita
            };

            return context.Configuracion
                .Where(c => c.IdCompania == idCompania && claves.Contains(c.Clave))
                .Select(c => new Configuracion()
                {
                    IdConfiguracion = c.IdConfiguracion,
                    Clave = c.Clave,
                    Descripcion = c.Descripcion,
                    Valor = c.Valor,
                    IdCompania = c.IdCompania,
                });
        }

        public Configuracion ConsultarPorClave(string clave, int idCompania)
        {
            return context.Configuracion
                .Where(c => c.IdCompania == idCompania && c.Clave == clave)
                .FirstOrDefault();
        }

        public Configuracion Consultar(int idConfiguracion)
        {
            return context.Configuracion
                .Where(c => c.IdConfiguracion == idConfiguracion)
                .FirstOrDefault();
        }
    }
}
