using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System;

#region TestHeader
#if TESTS
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsMSUnit")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsNUnit")]
#endif
#endregion

namespace Eventer
{
    /// <summary>
    /// Class containing Support Methods, useful for more classes.
    /// </summary>
    public static class SupportMethods
    {
        /// <summary>
        /// Generate MD5 hash as string.
        /// </summary>
        /// <param name="str">String from which should be hash generated.</param>
        /// <returns>MD5 Hash as string.</returns>
        public static string GetMd5Hash(string str)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Enque Interval into Queue. 
        /// </summary>
        /// <param name="dest">Target queue where souhld be interval enqued.</param>
        /// <param name="from">Begining of interval.</param>
        /// <param name="to">End of interval</param>
        public static void EnqueInterval(Queue<DateTime> dest, DateTime from, DateTime to)
        {
            if ((to - from).Days > 365)
            {
                dest.Enqueue(from);
                dest.Enqueue(to);
                return;
            }

            DateTime day = from.Date;
            while (day <= to)
            {
                dest.Enqueue(day);
                day = day.AddDays(1);
            }
        }

        /// <summary>
        /// Check if given string contain Illegal chars in array <see cref="Constants.IllegalChars"/>.
        /// </summary>
        /// <param name="str">Strin that should be checked.</param>
        /// <returns>True if string contain Illegal Chars, false otherwise.</returns>
        public static bool ContainIllegalChars(string str)
        {
            for (int i = 0; i < Constants.IllegalChars.Length; i++)
            {
                if (str.Contains(Constants.IllegalChars[i]))
                    return true;
            }
            return false;
        }
    }
}
