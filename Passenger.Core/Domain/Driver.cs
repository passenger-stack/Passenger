using System;
using System.Collections.Generic;
using System.Linq;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        private ISet<Route> _routes = new HashSet<Route>();
        private ISet<DailyRoute> _dailyRoutes = new HashSet<DailyRoute>();
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public DateTime UpdatedAt { get; private set; }
        public IEnumerable<Route> Routes
        {
            get { return _routes; }
            set { _routes = new HashSet<Route>(value); }
        }
        public IEnumerable<DailyRoute> DailyRoutes
        {
            get { return _dailyRoutes; }
            set { _dailyRoutes = new HashSet<DailyRoute>(value); }
        }

        protected Driver() 
        {
        }

        public Driver (User user)
        {
            UserId = user.Id;
            Name = user.Username;
        }

        public void SetVehicle(string brand, string name, int seats)
        {
            Vehicle = Vehicle.Create(brand, name, seats);
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddRoute(string name, Node start, Node end)
        {
            var route = Routes.SingleOrDefault(x => x.Name == name);
            if(route != null)
            {
                throw new Exception($"Route with name: '{name}' already exists for driver: {name}.");
            }
            _routes.Add(Route.Create(name, start, end));
            UpdatedAt = DateTime.UtcNow;
        }
    }
}