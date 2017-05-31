using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    public class VehiclesController : ApiControllerBase
    {
        private readonly IVehicleProvider _vehicleProvider;
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public VehiclesController(IVehicleProvider vehicleProvider, 
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _vehicleProvider = vehicleProvider;
        }

        public async Task<IActionResult> Get()
        {
            Logger.Info("AUTKO");
            var vehicles = await _vehicleProvider.BrowseAsync();

            return Json(vehicles);
        }
    }
}