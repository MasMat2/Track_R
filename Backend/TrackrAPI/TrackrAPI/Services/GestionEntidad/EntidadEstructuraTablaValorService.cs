using System.Transactions;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Services.GestionEntidad
{
    public class EntidadEstructuraTablaValorService
    {
        private readonly IEntidadEstructuraTablaValorRepository entidadEstructuraTablaValorRepository;

        public EntidadEstructuraTablaValorService(IEntidadEstructuraTablaValorRepository entidadEstructuraTablaValorRepository)
        {
            this.entidadEstructuraTablaValorRepository = entidadEstructuraTablaValorRepository;
        }

        public List<RegistroTablaDto> ConsultarRegistroTablaPorTabulacion(int idEntidadEstructura, int idTabla)
        {
            return entidadEstructuraTablaValorRepository
                .ConsultarPorTabulacion(idEntidadEstructura, idTabla)
                .GroupBy(e => e.Numero)
                .Select(registro =>
                {
                    return new RegistroTablaDto()
                    {
                        IdRegistroTabla = registro.Key,
                        IdEntidadEstructura = registro.First().IdEntidadEstructura,
                        Valores = registro.Where(ev => ev.Numero == registro.Key).ToList()
                    };
                })
                .OrderBy(r => r.IdRegistroTabla)
                .ToList();
        }

        public List<RegistroTablaDto> ConsultarPorPestanaSeccion(int idEntidadEstructura, int idTabla)
        {
            return entidadEstructuraTablaValorRepository
                .ConsultarPorPestanaSeccion(idEntidadEstructura, idTabla)
                .GroupBy(e => e.Numero)
                .Select(registro =>
                {
                    return new RegistroTablaDto()
                    {
                        IdRegistroTabla = registro.Key,
                        IdEntidadEstructura = registro.First().IdEntidadEstructura,
                        Valores = registro.Where(ev => ev.Numero == registro.Key).ToList()
                    };
                })
                .OrderBy(r => r.IdRegistroTabla)
                .ToList();
        }


        public void Agregar(EntidadTablaRegistroDto registro)
        {
            using var ts = new TransactionScope();

            var ultimoRegistro = entidadEstructuraTablaValorRepository.ConsultarUltimoRegistro(registro.IdEntidadEstructura, registro.IdTabla);

            foreach (var valorDto in registro.Valores)
            {
                var valor = new EntidadEstructuraTablaValor()
                {
                    IdEntidadEstructura = registro.IdEntidadEstructura,
                    IdTabla = registro.IdTabla,
                    Numero = ultimoRegistro + 1,
                    ClaveCampo = valorDto.ClaveCampo,
                    Valor = valorDto.Valor
                };

                entidadEstructuraTablaValorRepository.Agregar(valor);
            }

            ts.Complete();
        }

        public void Editar(EntidadTablaRegistroDto registro)
        {
            using var ts = new TransactionScope();

            var valoresDto = registro.Valores;

            var valoresDb = entidadEstructuraTablaValorRepository.ConsultarPorNumeroRegistro(
                registro.IdEntidadEstructura,
                registro.IdTabla,
                registro.Numero);

            var clavesDto = valoresDto.ConvertAll(v => v.ClaveCampo);
            var clavesDb = valoresDb.Select(v => v.ClaveCampo);

            var valoresEditar = valoresDb
                .Where(v => clavesDto.Contains(v.ClaveCampo))
                .ToList();

            foreach (var valor in valoresEditar)
            {
                var valorDto = valoresDto.Find(v => v.ClaveCampo == valor.ClaveCampo)!;

                valor.Valor = valorDto.Valor;

                entidadEstructuraTablaValorRepository.Editar(valor);
            }

            var valoresAgregar = valoresDto
                .Where(v => !clavesDb.Contains(v.ClaveCampo))
                .ToList();

            foreach (var valorDto in valoresAgregar)
            {
                var valor = new EntidadEstructuraTablaValor()
                {
                    IdEntidadEstructura = registro.IdEntidadEstructura,
                    IdTabla = registro.IdTabla,
                    Numero = registro.Numero,
                    ClaveCampo = valorDto.ClaveCampo,
                    Valor = valorDto.Valor
                };

                entidadEstructuraTablaValorRepository.Agregar(valor);
            }

            var valoresEliminar = valoresDb
                .Where(v => !clavesDto.Contains(v.ClaveCampo))
                .ToList();

            foreach (var valor in valoresEliminar)
            {
                entidadEstructuraTablaValorRepository.Eliminar(valor);
            }

            ts.Complete();
        }

        public void Eliminar(EntidadTablaRegistroDto registro)
        {
            using var ts = new TransactionScope();

            var valores = entidadEstructuraTablaValorRepository.ConsultarPorNumeroRegistro(
                registro.IdEntidadEstructura,
                registro.IdTabla,
                registro.Numero);

            foreach (var valor in valores)
            {
                entidadEstructuraTablaValorRepository.Eliminar(valor);
            }

            ts.Complete();
        }
    }
}
