using System;

namespace Passenger.Infrastructure.Services
{
    public class RouteManager : IRouteManager
    {
        private static readonly Random Random = new Random();

        public double CalculateLength(double startLatitude, double startLongitude, double endLatitude, double endLongitude)
            => Random.Next(500,10000);
    }
}