using System.Threading.Tasks;

namespace Hackaton.DataSeed
{
    public interface IDataSeedingService
    {
        Task SeedInitialData();
    }
}
