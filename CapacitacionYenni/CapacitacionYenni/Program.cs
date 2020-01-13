using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CapacitacionYenni
{
    /// <summary>
    /// Clase Program.
    /// </summary>
    public class Program
    {
        static readonly string cRutaArchivo = @"C:\Users\yenni.canul\Documents\BOT-YENNI\Requerimientos\SE-API\RQM 176814\CursosUML\Eventos.txt";

        /// <summary>
        /// Método principal del programa.
        /// </summary>
        /// <param name="args">Arreglo de argumentos.</param>
        public static void Main(string[] args)
        {
            List<string> lstEventos = new List<string>();
            lstEventos = ObtenerListaEventosConFechas();

            if (lstEventos.Any())
            {
                foreach (string cEvento in lstEventos)
                {
                    Console.WriteLine(cEvento);
                }
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Método que obtiene una lista de eventos con fechas.
        /// </summary>
        /// <returns>Lista de eventos con fechas.</returns>
        public static List<string> ObtenerListaEventosConFechas()
        {
            int iMinuto = 0;
            int iHora = 0;
            int iDia = 0;
            int iFecha = 0;
            int iMes = 0;
            string cTextoArchivo = string.Empty;
            string cFormatoFecha = string.Empty;
            string cNombreEvento = string.Empty;
            string cMensajeEvento = string.Empty;
            DateTime dtFechaActual = new DateTime();
            DateTime dtFechaEvento = new DateTime();
            List<string> lstEventos = new List<string>();

            if (File.Exists(cRutaArchivo))
            {
                using (StreamReader cArchivo = new StreamReader(cRutaArchivo))
                {
                    while ((cTextoArchivo = cArchivo.ReadLine()) != null)
                    {
                        cNombreEvento = cTextoArchivo.Split(',')[0]; //Navidad, Reyes Magos, Primavera
                        dtFechaActual = DateTime.Now; // 13/01/2020
                        dtFechaEvento = Convert.ToDateTime(cTextoArchivo.Split(',')[1]); // 24/12/2019, 06/01/2020, 21/03/2020

                        iMinuto = (dtFechaActual - dtFechaEvento).Minutes;
                        iHora = (dtFechaActual - dtFechaEvento).Hours;
                        iDia = (dtFechaActual - dtFechaEvento).Days;
                        iMes = ((((dtFechaActual.Year - dtFechaEvento.Year)) * 12) + dtFechaActual.Month - dtFechaEvento.Month);

                        if (iMinuto > 0 && iMinuto <= 59)
                        {
                            cFormatoFecha = "minuto(s)";
                            iFecha = iMinuto;
                        }
                        if (iHora > 0 && iHora < 1)
                        {
                            cFormatoFecha = "hora(s)";
                            iFecha = iHora;
                        }
                        if (iDia > 0 && iDia <= 23)
                        {
                            cFormatoFecha = "día(s)";
                            iFecha = iDia;
                        }
                        if (iMes > 0 && iMes <= 11)
                        {
                            cFormatoFecha = "mes(es)";
                            iFecha = iMes;
                        }

                        if (dtFechaEvento < dtFechaActual)
                        {
                            cMensajeEvento = $"{cNombreEvento} ocurrió hace {Math.Abs(iFecha)} {cFormatoFecha}";
                        }
                        else
                        {
                            cMensajeEvento = $"{cNombreEvento} ocurrirá dentro de {Math.Abs(iFecha)} {cFormatoFecha}";
                        }
                        lstEventos.Add(cMensajeEvento);
                    }
                    cArchivo.Close();
                }
            }

            return lstEventos;
        }
    }
}
