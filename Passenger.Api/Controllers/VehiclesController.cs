using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    public class VehiclesController : ApiControllerBase
    {
        private readonly IVehicleProvider _vehicleProvider;
        
        public VehiclesController(IVehicleProvider vehicleProvider, 
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _vehicleProvider = vehicleProvider;
        }

        public async Task<IActionResult> Get()
        {
            var users = await _vehicleProvider.BrowseAsync();

            return Json(users);
        }
    }
}