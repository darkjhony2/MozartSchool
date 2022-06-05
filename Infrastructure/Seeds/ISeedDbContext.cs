using ColegioMozart.Infrastructure.Persistence;

namespace ColegioMozart.Infrastructure.Seeds
{
    internal interface ISeedDbContext
    {
        Task SeedSampleDataAsync(ApplicationDbContext context);
    }
}
