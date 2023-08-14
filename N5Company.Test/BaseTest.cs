using Microsoft.EntityFrameworkCore;
using N5Company.Persistence;

namespace N5Company.Test
{
    public class BaseTest
    {
        protected DataContext BuildContext(string dbName)
        {

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(dbName).Options;

            DataContext dbContext = new DataContext(options);
            return dbContext;
        }
    }
}