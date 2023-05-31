using System.Data;
using System.Globalization;
using System.Text;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;

namespace TrackrAPI.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Content-Type", "text/plain");
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static string GenerarFolio()
        {
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            return milliseconds.ToString();
        }

        public static string ObtenerNombreCompleto(this Usuario usuario)
        {
            if (usuario == null)
            {
                return string.Empty;
            }

            return (usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno).Trim();
        }
        public static string ObtenerRoles(this ICollection<UsuarioRol> usuarioRoles)
        {
            string roles = "";
            if (usuarioRoles.Any())
            {
                int i = 1;

                foreach(var usuarioRol in usuarioRoles)
                {
                    roles += usuarioRol.IdRolNavigation.Nombre;

                    if (i < usuarioRoles.Count())
                    {
                        roles += ", ";
                    }
                    i++;
                }
            }
            else
            {
                roles = "Ninguno";
            }

            return roles;
        }
        public static string ObtenerPadecimientos(this ICollection<ExpedientePadecimiento> padecimientos)
        {
            string padecimientoString = "";
            if (padecimientos.Any())
            {
                int i = 1;

                foreach (var padecimiento in padecimientos)
                {
                    padecimientoString += padecimiento.IdPadecimientoNavigation.Nombre;

                    if (i < padecimientos.Count())
                    {
                        padecimientoString += ", ";
                    }
                    i++;
                }
            }
            else
            {
                padecimientoString = "Ninguno";
            }

            return padecimientoString;
        }

        public static string ObtenerNombrePaciente(this ExpedientePacienteInformacion expediente)
        {
            return (expediente.Nombre + " " + expediente.ApellidoPaterno + " " + expediente.ApellidoMaterno).Trim();
        }

        public static string ObtenerNombreCompleto(this Paciente paciente)
        {
            if (paciente == null)
            {
                return string.Empty;
            }

            return (paciente.Nombre + " " + paciente.ApellidoPaterno + " " + paciente.ApellidoMaterno).Trim();
        }

        public static string FormatoFecha(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static string FormatoHora(this DateTime date)
        {
            return date.ToString("HH:mm", CultureInfo.InvariantCulture);
        }

        public static string FormatoHora(this TimeSpan hora)
        {
            DateTime time = DateTime.Today.Add((TimeSpan)hora);
            string displayTime = time.ToString("hh:mm tt"); // It will give "03:00 AM"
            return displayTime;
        }

        public static string FormatoMonto(this double valor)
        {
            return string.Format("{0}{1:n}", "$", valor);
        }

        public static string FormatoMonto(this decimal valor)
        {
            return string.Format("{0}{1:n}", "$", valor);
        }

        public static double Redondear(this double valor)
        {
            return Math.Round(valor, 2, MidpointRounding.AwayFromZero);
        }

        public static decimal Redondear(this decimal valor)
        {
            return Math.Round(valor, 2, MidpointRounding.AwayFromZero);
        }

        public static string ObtenerDireccion(this Domicilio domicilio)
        {
            if (domicilio == null)
            {
                return string.Empty;
            }

            string sinEspecificar = "Sin Especificar";
            string pais = domicilio.IdPaisNavigation == null ? sinEspecificar : domicilio.IdPaisNavigation.Nombre;
            string estado = domicilio.IdEstadoNavigation == null ? sinEspecificar : domicilio.IdEstadoNavigation.Nombre;
            string localidad = domicilio.IdLocalidadNavigation == null ? sinEspecificar : domicilio.IdLocalidadNavigation.Nombre;
            string colonia = domicilio.IdColoniaNavigation == null ? sinEspecificar : domicilio.IdColoniaNavigation.Nombre;


            return domicilio.Calle + " #" + domicilio.NumeroExterior + ", Col. " + colonia + ", " +
                localidad + ", " + estado + ", " + pais + ", C.P. " + domicilio.CodigoPostal;
        }

        public static string ObtenerDireccionAlmacen(this Almacen almacen)
        {
            if (almacen == null)
            {
                return string.Empty;
            }

            return almacen.Calle + "#" + almacen.NumeroExterior + ", Col. " + almacen.Colonia + ", " + almacen.Localidad + ", " + almacen.IdEstadoNavigation.Nombre + " C.P." + almacen.CodigoPostal;
        }

        public static string ObtenerDireccionProveedor(this Proveedor proveedor)
        {
            if (proveedor == null)
            {
                return string.Empty;
            }

            return proveedor.Calle + " #" + proveedor.NumeroExterior + ", Col. " + proveedor.Colonia + ", " + proveedor.Localidad + ", " + proveedor.IdEstadoNavigation.Nombre + ", C.P. " + proveedor.CodigoPostal;
        }

        public static string OfertaEspecial(this ICollection<TipoDescuentoDetalle> lista)
        {
            string ofertaEspecial = "";

            if (lista.Any())
            {
                var dias = new List<string>();

                if (lista.Any(tdd => tdd.DiasSemana.Contains("Lu"))) dias.Add("Lu");
                if (lista.Any(tdd => tdd.DiasSemana.Contains("Ma"))) dias.Add("Ma");
                if (lista.Any(tdd => tdd.DiasSemana.Contains("Mi"))) dias.Add("Mi");
                if (lista.Any(tdd => tdd.DiasSemana.Contains("Ju"))) dias.Add("Ju");
                if (lista.Any(tdd => tdd.DiasSemana.Contains("Vi"))) dias.Add("Vi");
                if (lista.Any(tdd => tdd.DiasSemana.Contains("S�"))) dias.Add("S�");
                if (lista.Any(tdd => tdd.DiasSemana.Contains("Do"))) dias.Add("Do");

                if (dias.Count == 1)
                {
                    foreach (var tdd in dias)
                    {
                        ofertaEspecial += tdd;
                    }
                }
                else
                {
                    int j = 1;
                    foreach (var tdd in dias)
                    {
                        ofertaEspecial += tdd;

                        if (j != dias.Count())
                        {
                            ofertaEspecial += ", ";
                        }
                        j++;
                    }
                }

                ofertaEspecial += " | ";

                if (lista.Any(tdd => tdd.TodoElDia)) ofertaEspecial += "24 Hrs " + (lista.Any(tdd => !tdd.TodoElDia) ? "- " : "");

                var listaTmp = lista.Where(tdd => !tdd.TodoElDia).ToList();
                int i = 1;
                foreach (var tdd in listaTmp)
                {
                    if (!tdd.TodoElDia)
                    {
                        ofertaEspecial += tdd.HorarioInicial.ToString("hh\\:mm") + " a " + tdd.HorarioFinal.ToString("hh\\:mm") + " ";
                    }
                    else
                    {
                        i++;
                        continue;
                    }

                    if (i < listaTmp.Count())
                    {
                        ofertaEspecial += "- ";
                    }

                    i++;
                }

                ofertaEspecial += "| ";

                i = 1;
                foreach (var tdd in lista)
                {
                    ofertaEspecial += (int)tdd.Porcentaje + " % ";

                    if (i < lista.Count())
                    {
                        ofertaEspecial += "- ";
                    }

                    i++;
                }
            }
            else
            {
                ofertaEspecial = "Ninguna";
            }

            return ofertaEspecial;
        }

        public static string NumeroNombre(this CuentaContable cuentaContable)
        {
            if(cuentaContable == null)
            {
                return string.Empty;
            }

            return cuentaContable.Numero + " - " + cuentaContable.Nombre;
        }
        public static string ClaveNombre(this RegimenFiscal regimenFiscal)
        {
            if(regimenFiscal == null)
            {
                return string.Empty;
            }

            return regimenFiscal.Clave + " - " + regimenFiscal.Nombre;
        }
        public static string ClaveNombre(this AyudaSeccion ayudaSeccion)
        {
            if(ayudaSeccion == null)
            {
                return string.Empty;
            }

            return ayudaSeccion.Clave + " - " + ayudaSeccion.Nombre;
        }

        public static string NombreTipoCuentaContable(this TipoCuentaContable tipoCuentaContable)
        {
            return tipoCuentaContable.Nombre;
        }

        public static string ObtenerDetalle(this ICollection<TipoComisionDetalle> tipoComisionDetalles)
        {
            string detalle = "";


            if (tipoComisionDetalles.Any())
            {
                int i = 1;

                foreach (var tipoComisionDetalle in tipoComisionDetalles)
                {
                    detalle += tipoComisionDetalle.IdRolNavigation.Nombre + " - " +
                        (tipoComisionDetalle.Porcentaje != null ?
                            tipoComisionDetalle.Porcentaje + " %" : "$ " + tipoComisionDetalle.Monto.ToString());

                    if (i < tipoComisionDetalles.Count())
                    {
                        detalle += " | ";
                    }

                    i++;
                }


            }
            else
            {
                detalle = "Ninguna";
            }

            return detalle;
        }
        public static string ObtenerClinicas(this ICollection<ListaPrecioClinica> listaPrecioCLinicas)
        {
            string clinicas = "";
            int i = 1;

            foreach (var listaPrecioClinica in listaPrecioCLinicas)
            {
                clinicas += listaPrecioClinica.IdClinicaNavigation.Nombre;

                if (i == listaPrecioCLinicas.Count())
                {
                    clinicas += " ";
                }
                else
                {
                    clinicas += ", ";
                }
                i++;
            }

            return clinicas;
        }

        public static string ObtenerNombreUsuarios(this ICollection<Comision> listaComisiones)
        {
            string usuarios = "";
            int i = 1;

            foreach (var c in listaComisiones)
            {
                usuarios += c.IdUsuarioNavigation.ObtenerNombreCompleto();

                if (i == listaComisiones.Count())
                {
                    usuarios += " ";
                }
                else
                {
                    usuarios += ", ";
                }
                i++;
            }

            return usuarios;
        }

        public static string RemoveDiacritics(this String s)
        {
            String normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        public static decimal ObtenerPrecioUltimaCompra(this Kardex kardex)
        {
            var ultimaCompra = kardex.IdArticuloNavigation.OrdenCompraDetalle
                                                    .OrderByDescending(t => t.IdOrdenCompraNavigation.FechaEmision)
                                                    .FirstOrDefault();

            if(ultimaCompra == null)
            {
                return 0;
            }

            return ultimaCompra.PrecioUnitario;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey> (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static decimal ToNegative(this decimal valor)
        {
            return System.Math.Abs(valor) * (-1);
        }

        public static string ObtenerNombreCategoria(this Presentacion presentacion)
        {
            const string defaultString = "Sin Especificar";

            var categoria = presentacion.PresentacionArticulo?.FirstOrDefault()?.IdArticuloNavigation?.IdCategoriaNavigation;

            return categoria != null ? categoria.Nombre : defaultString;
        }

        public static string ObtenerNombreSubCategoria(this Presentacion presentacion)
        {
            const string defaultString = "Sin Especificar";

            var subCategoria = presentacion.PresentacionArticulo?.FirstOrDefault()?.IdArticuloNavigation?.IdSubCategoriaNavigation;

            return subCategoria != null ? subCategoria.Nombre : defaultString;
        }

        public static string ObtenerNombreSubSubCategoria(this Presentacion presentacion)
        {
            const string defaultString = "Sin Especificar";

            var subSubCategoria = presentacion.PresentacionArticulo?.FirstOrDefault()?.IdArticuloNavigation?.IdSubSubCategoriaNavigation;

            return subSubCategoria != null ? subSubCategoria.Nombre : defaultString;
        }

        public static List<UsuarioDto> ObtenerUsuariosResponsables(this FlujoDetalle flujoDetalle)
        {
            var flujoDetalleResponsable = flujoDetalle.FlujoDetalleResponsable;

            var responsables = flujoDetalleResponsable.Select(f => new UsuarioDto
            {
                IdUsuario = f.IdUsuario,
                NombreCompleto = f.IdUsuarioNavigation.ObtenerNombreCompleto()
            })
            .ToList();

            return responsables;
        }

        public static List<UsuarioDto> ObtenerUsuariosResponsables(this FlujoDetalleAplicado flujoDetalleAplicado)
        {
            var flujoDetalleAplicadoResponsable = flujoDetalleAplicado.FlujoDetalleAplicadoResponsable;

            var responsables = flujoDetalleAplicadoResponsable.Select(f => new UsuarioDto
            {
                IdUsuario = f.IdUsuario,
                NombreCompleto = f.IdUsuarioNavigation.ObtenerNombreCompleto()
            })
            .ToList();

            return responsables;
        }

        public static IEnumerable<T> Select<T>(this IDataReader reader, Func<IDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }

        public static bool TieneDomicilioCompleto(this Usuario usuario)
        {
            if (usuario == null)
                return false;

            return usuario.IdEstado != null && (int)usuario.IdEstado > 0
                && usuario.IdMunicipio != null && (int)usuario.IdMunicipio > 0
                && usuario.IdLocalidad != null && (int)usuario.IdLocalidad > 0
                && usuario.IdColonia != null && (int)usuario.IdColonia > 0
                && !string.IsNullOrEmpty(usuario.CodigoPostal)
                && !string.IsNullOrEmpty(usuario.Calle)
                && !string.IsNullOrEmpty(usuario.NumeroExterior);
        }

        public static bool IsEqualTo(this Domicilio domicilio, Domicilio other)
        {
            if (domicilio == null || other == null)
                return false;

            return domicilio.IdUsuario == other.IdUsuario
                && domicilio.IdPais == other.IdPais
                && domicilio.IdEstado == other.IdEstado
                && domicilio.IdMunicipio == other.IdMunicipio
                && domicilio.IdLocalidad == other.IdLocalidad
                && domicilio.CodigoPostal == other.CodigoPostal
                && domicilio.IdColonia == other.IdColonia
                && domicilio.Calle?.ToLower() == other.Calle?.ToLower()
                && domicilio.NumeroExterior?.ToLower() == other.NumeroExterior?.ToLower()
                && domicilio.NumeroInterior?.ToLower() == other.NumeroInterior?.ToLower();
        }

        public static IEnumerable<T1> ExceptBy<T1, T2>(this IEnumerable<T1> @base, IEnumerable<T2> diferencia, Func<T1, T2, bool> comparer)
        {
            return @base.Where((x) => !diferencia.Any((y) => comparer(x, y)));
        }
    }
}
