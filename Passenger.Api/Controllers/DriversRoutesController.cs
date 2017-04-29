using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    [Route("drivers/routes")]
    public class DriversRoutesController : ApiControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversRoutesController(ICommandDispatcher commandDispatcher,
            IDriverService driverService) 
            : base(commandDispatcher)
        {
            _driverService = driverService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDriverRoute command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }        
    }
}