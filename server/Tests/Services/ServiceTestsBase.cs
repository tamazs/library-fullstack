using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class ServiceTestsBase : IDisposable
{
    protected readonly LibraryDbContext Context;

    protected ServiceTestsBase()
    {
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        Context = new LibraryDbContext(options);
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
