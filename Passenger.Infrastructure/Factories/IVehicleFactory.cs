using Passenger.Core.Domain;

namespace Passenger.Infrastructure.Factories
{
    public interface IVehicleFactory
    {
         Vehicle Create(string brand, string name);
    }
}