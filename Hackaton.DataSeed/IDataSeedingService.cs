using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.DataSeed
{
    public interface IDataSeedingService
    {
        Task SeedInitialData();
    }
}
