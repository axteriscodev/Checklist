using Microsoft.EntityFrameworkCore;
using TDatabase.Database;

namespace TDatabase.Utilities
{

    /// <summary>
    /// Classe utilizzata per creare il contesto del db
    /// </summary>
    public class DatabaseContextFactory
    {
        /// <summary>
        /// Metodo per creare un contesto del database
        /// </summary>
        /// <returns></returns>
        public static ChecklistContext Create(string connectionString)
        {
            var OptionsBuilder = new DbContextOptionsBuilder<ChecklistContext>();
            OptionsBuilder.UseSqlServer(connectionString);
            return new ChecklistContext(OptionsBuilder.Options);
        }
    }
}
