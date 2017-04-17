using System;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class CreateDriver : ICommand
    {
        public Guid UserId { get; set; }
        public DriverVehicle Vehicle { get; set; }
        public class DriverVehicle 
        {
            public string Brand { get; set; }
            public string Name { get; set; }
            public int Seats { get;  set; }
        }
    }
}