using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly IDriverRouteService _driverRouteService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public DataInitializer(IUserService userService, IDriverService driverService,
            IDriverRouteService driverRouteService)
        {
            _userService = userService;
            _driverService = driverService;
            _driverRouteService = driverRouteService;
        }

        public async Task SeedAsync()
        {
            var users = await _userService.BrowseAsync();
            if(users.Any())
            {
                Logger.Trace("Data was already initialized.");

                return; 
            }
            Logger.Trace("Initializing data...");    
            var tasks = new List<Task>();
            for(var i=1; i<=10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";
                await _userService.RegisterAsync(userId, $"user{i}@test.com",
                                                 username, "secret", "user");
                Logger.Trace($"Adding user: '{username}'.");
                await _driverService.CreateAsync(userId);
                await _driverService.SetVehicle(userId, "BMW", "i8");
                await _driverRouteService.AddAsync(userId, "Default route",
                    1,1,2,2);
                await _driverRouteService.AddAsync(userId, "Job route",
                    3,3,5,5);
                Logger.Trace($"Adding driver for: '{username}'.");
            }
            for(var i=1; i<=3; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"admin{i}";
                 Logger.Trace($"Adding admin: '{username}'.");
                tasks.Add(_userService.RegisterAsync(userId, $"admin{i}@test.com", 
                    username, "secret", "admin"));
            }
            await Task.WhenAll(tasks);
            Logger.Trace("Data was initialized.");  
        }
    }
}