using Microsoft.EntityFrameworkCore;

namespace Raditap.DatabaseAccess.Entities
{
    public partial class RaditapContext
    {
        public RaditapContext(DbContextOptions<RaditapContext> options, DbContextOptions<ReadRaditapContext> readOnlyOptions) :
               base(options ?? (DbContextOptions)readOnlyOptions)
        { }
    }
}
