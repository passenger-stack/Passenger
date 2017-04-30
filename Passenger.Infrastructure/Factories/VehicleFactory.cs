using System;
using System.Collections.Generic;
using System.Linq;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.Factories
{
    public class VehicleFactory : IVehicleFactory
    {
        private static readonly IDictionary<string, IEnumerable<VehicleDetails>> availableVehicles = 
            new Dictionary<string, IEnumerable<VehicleDetails>>
        {
            ["Audi"] = new List<VehicleDetails>(),
            ["BMW"] = new List<VehicleDetails>
            {
                new VehicleDetails("i8", 5)
            },
            ["Ford"] = new List<VehicleDetails>(),
            ["Skoda"] = new List<VehicleDetails>(),
            ["Volkswagen"] = new List<VehicleDetails>()
        };

        public Vehicle Create(string brand, string name)
        {
            if(!availableVehicles.ContainsKey(brand))
            {
                throw new Exception($"Vehicle brand: '{brand}' is not available.");
            }
            var vehicles = availableVehicles[brand];
            var vehicle = vehicles.SingleOrDefault(x => x.Name == name);
            if(vehicle == null)
            {
                throw new Exception($"Vehicle: '{name}' for brand: '{brand}' is not available.");
            }

            return Vehicle.Create(brand, name, vehicle.Seats);
        }

        private class VehicleDetails
        {
            public string Name { get; }
            public int Seats { get; }

            public VehicleDetails(string name, int seats)
            {
                Name = name;
                Seats = seats;
            }
        }
    }
}