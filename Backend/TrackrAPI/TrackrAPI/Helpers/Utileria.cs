using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace TrackrAPI.Helpers
{
    public static class Utileria
    {
        public static int ObtenerIdUsuarioSesion(ControllerBase controller)
        {
            if (controller.User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                throw new UnathorizedException("Acceso no autorizado");
            }

            return int.Parse(controller.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public static DateTime ObtenerFechaActual()
        {
            return DateTime.Now.AddHours(-1);
        }

        public static int DiferenciaEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return (int)(fechaFin - fechaInicio).TotalDays;
        }

        public static string CalcularEdad(DateTime dateTime) {

            string cadena = (DateTime.Now.Year - dateTime.Year) + " Edad";

            return cadena;
        }

        public static string CalcularAniosDeEdad(DateTime dateTime)
        {

            string cadena = (DateTime.Now.Year - dateTime.Year).ToString();

            return cadena;
        }

        /// <summary>
        /// Se verifica si el rango de fechas proporcionadas se superponen entre si, en caso de 
        /// un rango de fechas cubra a la otra
        /// </summary>
        /// <param name="inicialA">Fecha inicial del conjunto de rango A</param>
        /// <param name="inicialB">Fecha inicial del conjunto de rango B</param>
        /// <param name="finalA">Fecha final del conjunto de rango A</param>
        /// <param name="finalB">Fecha final del conjunto de rango B</param>
        /// <returns>Si se encuentra solapadas las fechas se retorna true, si no false</returns>
        public static Boolean FechasOverlap(DateTime inicialA, DateTime inicialB, DateTime finalA, DateTime finalB)
        {
            return (inicialA.Date <= finalB.Date) && (inicialB.Date <= finalA.Date) && (inicialA.Date <= finalA.Date) && (inicialB.Date <= finalB.Date);
        }

        public static string FormatoNumero(int numero)
        {
            if (numero.ToString().Length == 1)
            {
                return "0" + numero;
            }
            else
            {
                return numero.ToString();
            }
              
        }
    }
}
