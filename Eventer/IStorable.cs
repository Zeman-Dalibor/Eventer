
#region TestHeader
#if TESTS
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsMSUnit")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsNUnit")]
#endif
#endregion

namespace Eventer
{
    /// <summary>
    /// Interface for Storing class into Database.
    /// </summary>
    interface IStorable
    {
        /// <summary>
        /// String used for inserting values into database. It should contain every data, that shuld be stored in Database. 
        /// </summary>
        /// <returns>String in format: "value1, value2, value3", by the exact and all collumns.</returns>
        string GetSQLiteInsertString();

        /// <summary>
        /// String used for updating values into database. It should contain every data, that shuld be stored in Database.
        /// </summary>
        /// <returns>String in format: "collumn1=value1, collumn2=value2, collumn3=value3", by collumns.</returns>
        string GetSQLiteUpdateString();
    }
}
