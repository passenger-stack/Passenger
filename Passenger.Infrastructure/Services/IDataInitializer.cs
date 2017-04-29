using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IDataInitializer : IService
    {
         Task SeedAsync();
    }
}