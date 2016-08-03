using System.Text;

#region TestHeader
#if TESTS
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsMSUnit")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsNUnit")]
#endif
#endregion

namespace Eventer
{
    /// <summary>
    /// Class for common used Constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Format used for storing in Database or Sending.
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// User Friendly format, that is easy readable.
        /// </summary>
        public const string UserFriendlyDateTimeFormat = "d. MMMM yyyy HH:mm:ss";

        /// <summary>
        /// Array of diallowed chars. Disallowed due to communication delimiter, storing separator, etc.
        /// </summary>
        public static string[] IllegalChars = { "'", "[", "]", "*", "?", ";", "^", "\""};

        /// <summary>
        /// Array inserted into one string, one by one char
        /// </summary>
        public static string IllegalCharsInString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (string st in IllegalChars)
                {
                    sb.Append(st);
                }
                return sb.ToString();

            }
        }

        /// <summary>
        /// Port used for UDP broadcast communication
        /// </summary>
        public const int UDPPort = 8888;

        /// <summary>
        /// Port used for TCP data sending communication
        /// </summary>
        public const int TCPPort = 8889;
    }
}
