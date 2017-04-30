namespace Passenger.Infrastructure.Services
{
    public interface IRouteManager : IService
    {
         double CalculateLength(double startLatitude, double startLongitude,
            double endLatitude, double endLongitude);
    }
}