using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRouteManager _routeManager;
        private readonly IMapper _mapper;

        public DriverRouteService(IDriverRepository driverRepository,
            IUserRepository userRepository,
            IRouteManager routeManager, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _routeManager = routeManager;
            _mapper = mapper;
        }

        public async Task AddAsync(Guid userId, string name, 
            double startLatitude, double startLongitude,
            double endLatitude, double endLongitude)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if(driver == null)
            {
                throw new Exception($"Driver with user id: '{userId}' was not found.");
            }
            var start = Node.Create("Start address", startLatitude, startLongitude);
            var end = Node.Create("End address", endLatitude, endLongitude);
            var length = _routeManager.CalculateLength(startLatitude, startLongitude,
                endLatitude, endLongitude);
            driver.AddRoute(name, start, end, length);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if(driver == null)
            {
                throw new Exception($"Driver with user id: '{userId}' was not found.");
            }
            driver.DeleteRoute(name);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}