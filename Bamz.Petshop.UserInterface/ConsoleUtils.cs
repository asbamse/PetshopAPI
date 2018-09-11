using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Bamz.Petshop.UserInterface
{
    /**
     * <summary>
     * Handles console parsing.
     * </summary>
     **/
    static class ConsoleUtils
    {
        /**
         * <summary>
         * Read a string which is not empty from console ReadLine();
         * Returns given string.
         * </summary>
         **/
        public static string ReadNotEmpty()
        {
            return ReadNotEmpty("");
        }

        public static string ReadNotEmpty(string message)
        {
            Console.Write(message);
            
            string str = Console.ReadLine();
            if(str.Length > 0)
            {
                return str;
            }
            else
            {
                Console.WriteLine("It cannot be empty!");
                return ReadNotEmpty(message);
            }
        }

        /**
         * <summary>
         * Read an integer from console ReadLine();
         * Returns given integer.
         * </summary>
         **/
        public static int ReadInt()
        {
            return ReadInt("");
        }
        public static int ReadInt(string message)
        {
            Console.Write(message);

            int j;
            if (Int32.TryParse(Console.ReadLine(), out j))
            {
                return j;
            }
            else
            {
                Console.WriteLine("It can be an integer only!");
                return ReadInt(message);
            }
        }

        /// <summary>
        /// Reads a double from console input.
        /// </summary>
        /// <returns>Double from input.</returns>
        public static double ReadDouble()
        {
            return ReadDouble("");
        }
        public static double ReadDouble(string message)
        {
            Console.Write(message);

            double j;
            if (double.TryParse(Console.ReadLine(), out j))
            {
                return j;
            }
            else
            {
                Console.WriteLine("It can be an double only!");
                return ReadDouble(message);
            }
        }

        /// <summary>
        /// Gets date from console input.
        /// </summary>
        /// <returns>DateTime</returns>
        public static DateTime ReadDate()
        {
            return ReadDate("");
        }
        public static DateTime ReadDate(string message)
        {
            Console.Write(message);

            CultureInfo MyCultureInfo = new CultureInfo("de-DE");
            try
            {
                return DateTime.Parse(Console.ReadLine(), MyCultureInfo);
            }
            catch(Exception)
            {
                Console.WriteLine("It can be an date only! Try writing in format 'dd/mm/yyyy hh:mm:ss'.");
                return ReadDate(message);
            }
        }
    }
}
