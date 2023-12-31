using Microsoft.EntityFrameworkCore;
using miniWms.Infrastructure;

namespace miniWms.UnitTests.Infrastructure.Helper
{
    public class TestMiniWmsDbContext(DbContextOptions<MiniWmsDbContext> dbContextOptions) : MiniWmsDbContext(dbContextOptions)
    {
        public override void Dispose()
        {
            Database.EnsureDeleted();
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            Database.EnsureDeletedAsync();
            return base.DisposeAsync();
        }
    }
}
