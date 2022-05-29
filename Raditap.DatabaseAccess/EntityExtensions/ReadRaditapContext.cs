using Microsoft.EntityFrameworkCore;

namespace Raditap.DatabaseAccess.Entities
{
    public class ReadRaditapContext : RaditapContext
    {
        public ReadRaditapContext(DbContextOptions<ReadRaditapContext> options) : base(null, options) { }
    }
}
