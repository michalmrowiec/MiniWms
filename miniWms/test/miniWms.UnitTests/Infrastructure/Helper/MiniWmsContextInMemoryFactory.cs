using Microsoft.EntityFrameworkCore;
using miniWms.Infrastructure;

namespace miniWms.UnitTests.Infrastructure.Helper
{
    public class MiniWmsContextInMemoryFactory
    {
        public static TestMiniWmsDbContext Create()
        {
            var options = new DbContextOptionsBuilder<MiniWmsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new TestMiniWmsDbContext(options);
        }
    }
}
