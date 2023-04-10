using System.Text.RegularExpressions;

namespace TrackrAPI.Helpers
{
    public static class Validator
    {
        /**
         * Valida el rango de valores que están para un campo de tipo entero
         * 
         * @param valor Valor a validar
         * @param minimo Valor minimo
         * @param maximo Valor maximo
         * @param mensaje Mensaje que muestra si no cumple la validacion
         */
        public static void ValidarRangoEntero(int? valor, int minimo, int maximo,
                String mensaje)
        {

            if (valor == null)
            {
                return;
            }

            if (valor < minimo)
            {
                throw new CdisException(mensaje);
            }
            if (valor > maximo)
            {
                throw new CdisException(mensaje);
            }
        }

        /**
         * Valida el rango de valores que están para un campo de tipo double
         * 
         * @param valor Valor a validar
         * @param minimo Valor minimo
         * @param maximo Valor maximo
         * @param mensaje Mensaje que muestra si no cumple la validacion
         */
        public static void ValidarRangoDecimal(decimal valor, decimal minimo, decimal maximo,
                String mensaje)
        {
            if (valor < minimo)
            {
                throw new CdisException(mensaje);
            }
            if (valor > maximo)
            {
                throw new CdisException(mensaje);
            }
        }

        /**
         * Valida que la longitud del texto no sea mayor a la longitud recibida como
         * parametro
         * 
         * @param texto Texto a validar
         * @param longitud a validar
         * @param mensaje que muestra
         */
        public static void ValidarLongitudMaximaString(String texto, int longitud, String mensaje)
        {

            if (string.IsNullOrWhiteSpace(texto))
            {
                return;
            }

            if (texto.Length > longitud)
            {
                throw new CdisException(mensaje);
            }
        }

        /**
         * Valida que la longitud del texto se encuentre entre un rango especifico
         * 
         * @param texto texto a validar su longitud
         * @param minimo rango minimo a validar
         * @param maximo rango máximo a valida
         * @param mensaje mensaje a mostrar cuando el texto no se encuentra en el rango
         */
        public static void ValidarLongitudRangoString(String texto, int minimo, int maximo,
                String mensaje)
        {

            if (string.IsNullOrWhiteSpace(texto))
            {
                return;
            }

            if (texto.Length > maximo)
            {
                throw new CdisException(mensaje);
            }

            if (texto.Length < minimo)
            {
                throw new CdisException(mensaje);
            }
        }

        public static void ValidarLongitudRangoString(String texto, int maximo, String mensaje)
        {

            if (string.IsNullOrWhiteSpace(texto))
            {
                return;
            }

            if (texto.Length > maximo)
            {
                throw new CdisException(mensaje);
            }
        }

        /**
         * Valida que un texto sea diferente de null y diferente de vacío
         * 
         * @param texto texto a validar
         * @param mensaje mensaje que se muestra al marcar error
         */
        public static void ValidarRequerido(string texto, String mensaje)
        {
            if (texto == null)
            {
                throw new CdisException(mensaje);
            }

            if (string.IsNullOrWhiteSpace(texto))
            {
                throw new CdisException(mensaje);
            }
        }

        /**
         * Valida que un numero entero sea mayor a 0
         * 
         * @param numero a validar
         * @param mensaje a mostrar
         */
        public static void ValidarRequerido(int numero, String mensaje)
        {
            if (numero <= 0)
            {
                throw new CdisException(mensaje);
            }
        }

        /**
         * Valida que una fecha sea requerida, realiza la comparación contra null
         * 
         * @param fecha a validar
         * @param mensaje a mostrar
         */
        public static void ValidarRequerido(DateTime? fecha, String mensaje)
        {
            if (fecha == null)
            {
                throw new CdisException(mensaje);
            }
        }

        /**
         * Valida que un numero de tipo double sea mayor que 0
         * 
         * @param numero a validar
         * @param mensaje a mostrar
         */
        public static void ValidarRequerido(decimal numero, String mensaje)
        {
            if (numero <= 0)
            {
                throw new CdisException(mensaje);
            }
        }

        /**
         * Valida que un objeto no sea nulo
         * 
         * @param objeto a validar
         * @param mensaje a mostrar
         */
        public static void ValidarRequerido(Object objeto, String mensaje)
        {
            if (objeto == null)
            {
                throw new CdisException(mensaje);
            }
        }

        /**
         * Valida que un objeto no sea nulo
         * 
         * @param objeto a validar
         * @param mensaje a mostrar
         */
        public static void ValidarRequerido<T>(ICollection<T> list, String mensaje)
        {
            if (list == null || list.Count == 0)
            {
                throw new CdisException(mensaje);
            }
        }

        /**
         * Valida el correo mediante una expresión regular
         * 
         * @param correo a validar
         * @param mensaje a mostar
         */
        public static void ValidarCorreo(String correo, String mensaje)
        {

            if (string.IsNullOrWhiteSpace(correo))
            {
                return;
            }

            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (!Regex.IsMatch(correo, pattern))
            {
                throw new CdisException(mensaje);
            }
        }

        public static void ValidarRFC(String rfc, String mensaje)
        {

            if (string.IsNullOrWhiteSpace(rfc))
            {
                return;
            }

            string pattern = @"^([A-Za-zÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Za-z\d]{2})([A\d])$";
            if (!Regex.IsMatch(rfc, pattern))
            {
                throw new CdisException(mensaje);
            }
        }

        public static void ValidarAlfanumerico(String texto, String mensaje)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return;
            }

            string pattern = @"^[(),A-Za-zÑñÁáÉéÍíÓóÚúÜü '0-9.]+$";
            if (!Regex.IsMatch(texto, pattern))
            {
                throw new CdisException(mensaje);
            }
        }

        public static void ValidarNombre(String texto, String mensaje)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return;
            }

            string pattern = @"^[a-zA-ZÀ-ÿ0-9\u00f1\u00d1\u0027 ]*$";
            if (!Regex.IsMatch(texto, pattern))
            {
                throw new CdisException(mensaje);
            }
        }

        public static void ValidarNombreSinNumeros(String texto, String mensaje)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return;
            }

            string pattern = @"^[a-zA-ZÀ-ÿ\u00f1\u00d1\u0027 ]*$";
            if (!Regex.IsMatch(texto, pattern))
            {
                throw new CdisException(mensaje);
            }
        }

        public static void ValidarNumerosEnterosConComas(String texto,int digitosMaximosPorNumero ,String mensaje)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return;
            }

            string pattern = @"^[0-9]{1,"+ digitosMaximosPorNumero + "}?(,[0-9]{1,"+ digitosMaximosPorNumero + "})*$";
            if (!Regex.IsMatch(texto, pattern))
            {
                throw new CdisException(mensaje);
            }
        }
        
        public static void ValidarNumeroEntero(String numero, String mensaje)
        {
             if (string.IsNullOrWhiteSpace(numero))
            {
                return;
            }

            string pattern = @"^[0-9]*$";
            if (!Regex.IsMatch(numero, pattern))
            {
                throw new CdisException(mensaje);
            }
        }


        /**
         * Valida que una cadena sea válida a base de una expresión regular
         * 
         * @param regex a validar
         * @param mensaje a mostar
         * @param cadena cadena a validar
         */
        public static void ValidarGeneralRegex(String cadena, String regex, String mensaje)
        {
            /*Pattern patron = Pattern.compile(regex);
            Matcher validador = patron.matcher(cadena);

            if (!validador.find()) {
                throw new CdisException(mensaje);
            }*/
        }

        /**
         * Valida que un número de tipo int no sea menor a 0
         * 
         * @param valor a validar
         * @param mensaje a mostrar
         */
        public static void ValidarEnteroPositivo(int valor, String mensaje)
        {
            if (valor < 0)
            {
                throw new CdisException(mensaje);
            }
        }

        public static void ValidarRequeridoLista(int[] lista, String mensaje)
        {
            if (lista == null || lista.Length == 0)
            {
                throw new CdisException(mensaje);
            }
        }

        public static void ValidarRequeridoLista(List<object> lista, String mensaje)
        {
            if (lista == null || lista.Count == 0)
            {
                throw new CdisException(mensaje);
            }
        }

        /**
         * Valida que la fecha de inicio sea menor o igual a la fecha fin
         * 
         * @param fechaInicio Fecha de Inicio
         * @param fechaFin Fecha de Fin
         * @param mensaje Mensaje que muestra si no cumple la validacion
         */
        public static void validarRangoFechaInclusive(DateTime fechaInicio, DateTime fechaFin, String mensaje)
        {
            if (fechaInicio > fechaFin)
            {
                throw new CdisException(mensaje);
            }
        }

        public static void validarRangoFechaExclusive(DateTime fechaInicio, DateTime fechaFin, String mensaje)
        {

            if (fechaInicio >= fechaFin)
            {
                throw new CdisException(mensaje);
            }
        }

        /**
         * Valida que el numero exterior tenga un formato valido. Acepta: Letras mayusculas y
         * minusculas, numeros, puntos, comas, guiones simples.
         * 
         * @param numero string a validar
         * @param mensaje de error a mostrar
         */
        public static void ValidarNumeroExterior(String numero, String mensaje)
        {
            /*Pattern nombrePattern = Pattern.compile(
                    "^[a-zA-ZÀ-ÿ0-9., -]*$",
                    Pattern.CASE_INSENSITIVE);
            Matcher matcher = nombrePattern.matcher(numero);

            if (!matcher.find()) {
                throw new FormatoInvalidoException(mensaje);
            }*/
        }

        /**
         * Valida que el valor recibido sea un número positivo
         * 
         * @param valor Valor a validar
         * @param mensaje Mensaje que muestra si no cumple la validacion
         */
        public static void ValidarDecimalPositivo(decimal valor, String mensaje)
        {
            if (valor < 0)
            {
                throw new CdisException(mensaje);
            }
        }

        public static void ValidarTelefono(String telefono, String mensaje)
        {

            if (string.IsNullOrWhiteSpace(telefono))
            {
                return;
            }

            string pattern = @"^([\\+][0-9])?(-?[0-9]+)*$";
            if (!Regex.IsMatch(telefono, pattern))
            {
                throw new CdisException(mensaje);
            }
        }
    }
}
